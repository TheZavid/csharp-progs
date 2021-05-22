using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
using System.IO;
using System.Text.RegularExpressions;

class Geo {
    const double RADIUS = 6378.16;

    public static double radians(double x) => x * PI / 180;

    public static double distance(
        double lat1, double long1, double lat2, double long2)
    {
        double dlat = radians(lat2 - lat1);
        double dlong = radians(long2 - long1);

        double a = Sin(dlat / 2) * Sin(dlat / 2) +
                   Cos(radians(lat1)) * Cos(radians(lat2)) * (Sin(dlong / 2) * Sin(dlong / 2));
        double angle = 2 * Atan2(Sqrt(a), Sqrt(1 - a));
        return angle * RADIUS;
    }
}


class Program {
    public struct Data {
        public string name;
        public double lat;
        public double lng;
        public int pop;
        public double dist; 
    }
    public class popComparer : IComparer<Data> {
        public int Compare(Data x, Data y) {
            if (x.pop > y.pop)
                return 1;
            else if (x.pop < y.pop)
                return -1;
            return 0;
        }
    }

    public class distComparer : IComparer<Data> {
        public int Compare(Data x, Data y) {
            if (x.dist > y.dist)
                return 1;
            else if (x.dist < y.dist)
                return -1;
            return 0;
        }
    }
    static Data extractDataUtil (string entry) {
        Data data;
        string[] tmp = entry.Split(',');
        string[] rawData = new string[11];
        if (tmp.Length > rawData.Length) {
            int ti = 0;
            for(int i = 0; i < tmp.Length; i++) {
                if (tmp[i].StartsWith('"') && !tmp[i].EndsWith('"')) {
                    rawData[ti] += tmp[i++];
                    while(!tmp[i].EndsWith('"'))
                        rawData[ti] += tmp[i++];
                }
                rawData[ti++] += tmp[i];
            }
        } else rawData = tmp;
        data.name = rawData[1].Trim('"');
        data.lat = double.Parse(rawData[2]);
        data.lng = double.Parse(rawData[3]);
        if (rawData[9] == "")
            data.pop = 0;
        else data.pop = int.Parse(rawData[9].Split('.')[0]);
        data.dist = 0;
        return data;
    }
    static Data extractData(string entry, double b_lat, double b_lng) {
        Data data = extractDataUtil(entry);
        data.dist = Geo.distance(b_lat, b_lng, data.lat, data.lng);
        return data;
    }
    static string findCity(string city) {
        Regex regex = new Regex(city, RegexOptions.IgnoreCase);
        using (StreamReader reader = new StreamReader("worldcities.csv")) {
            string line;
            while ((line = reader.ReadLine()) != null) {
                Match match = regex.Match(line);
                if (match.Success)
                    return line;
            }
        }
        return null;
    }

    static void execQuery(Data base_data, char cmd, int cmd_var) {
        List<Data> results = new List<Data>();
        using (StreamReader reader = new StreamReader("worldcities.csv")) {
            string line = reader.ReadLine();
            if (cmd == 'd') {
                while ((line = reader.ReadLine()) != null) {
                    Data cur_data = extractData(line, base_data.lat, base_data.lng);
                    if (cur_data.name != base_data.name && cur_data.pop > 0 && cur_data.dist <= cmd_var) {
                        if (results.Count == 10) {
                            results.Add(cur_data);
                            results.Sort(new popComparer());
                            results.RemoveAt(0);
                        }  else results.Add(cur_data);
                    }
                }
            } else {
                while ((line = reader.ReadLine()) != null) {
                    Data cur_data = extractData(line, base_data.lat, base_data.lng);
                    if (cur_data.name != base_data.name && cur_data.pop >= cmd_var) {
                        if (results.Count == 10) {
                            results.Add(cur_data);
                            results.Sort(new distComparer());
                            results.RemoveAt(10);
                        }  else results.Add(cur_data);
                    }
                }
            }
        }
        if (cmd == 'd') {
            results.Sort(new popComparer());
            results.Reverse();
        } else
            results.Sort(new distComparer());

        foreach(Data data in results) {
            WriteLine($"{data.name}: pop = {data.pop:n0} / {Round(data.dist)} km");
        }
    }
    static void Main(string[] args) {
        if (args.Length != 3) {
            WriteLine("Wrong number of arguments!");
            return;
        }
        string base_city = args[0];
        string base_entry = findCity(base_city);
        if (base_entry == null) {
            WriteLine($"No city {base_city}");
            return;
        }
        Data base_data = extractDataUtil(base_entry);
        string command = args[1];
        if (command != "pop" && command != "dist") {
            WriteLine($"I dont know command {command}!");
            return;
        }
        int cmd_var = 0;
        if(!int.TryParse(args[2], out cmd_var)) {
            WriteLine($"{args[2]} is not an integer!");
            return;
        }
        execQuery(base_data, command[0], cmd_var);
    }
}
