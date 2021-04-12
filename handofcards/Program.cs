using System;


class Card {
    // 1 = Ace, 2 .. 10, 11 = Jack, 12 = Queen, 13 = King 
    public int rank;    
    
    public char suit;   // 'C', 'D', 'H' or 'S'

    int determineRank(string rank) {
        switch (rank)
        {
            case "J":
                return 11;
            case "Q":
                return 12;
            case "K":
                return 13;
            case "A":
                return 1;
            default:
                return Int32.Parse(rank);
            
        }
    }
    public Card(string s) {
        if (s.Length == 2)
            rank = determineRank(s.Substring(0,1));
        else
            rank = Int32.Parse(s.Substring(0,2));

        suit = char.Parse(s.Substring(s.Length - 1));
    }
}

class Hand
{
    public Card[] inhand = new Card[5];

    public Hand(string s) {
        string[] handout = s.Split();
        for (int i = 0; i < 5; i++) {
            inhand[i] = new Card(handout[i]);
        }
    }


    bool isBetter(int my_n, int my_rank, int op_n, int op_rank) {
        return (my_n > op_n || (my_n == op_n && (my_rank == 1 || ((my_rank > op_rank) && (op_rank != 1)))));
    }
    string findCombo(Card[] cards) {
       int hi_number = 0;
       int hi_rank = 0;
       for (int i = 0; i < 5; i++) {
            int cur_rank = cards[i].rank;
            int cur_number = 1;
            if (cur_rank != 0) {
                    for (int j = i+1; j < 5; j++) {
                        if (cards[j].rank == cur_rank) {
                            cards[j].rank = 0;
                            cur_number++;
                        }
                    }
            }
            if (cur_number > hi_number) {
                hi_number = cur_number;
                hi_rank = cur_rank;
            } else if (cur_number == hi_number && (cur_rank == 1 || (cur_rank > hi_rank && hi_rank != 1)))
                hi_rank = cur_rank;
       }
       return hi_number.ToString() + hi_rank.ToString();
    } 
    public int compare(Hand g) {
        string my_combo = findCombo(inhand);
        string op_combo = g.findCombo(g.inhand);
        int my_n = Int32.Parse(my_combo.Substring(0,1));
        int my_rank = Int32.Parse(my_combo.Substring(1));
        int op_n = Int32.Parse(op_combo.Substring(0,1));
        int op_rank = Int32.Parse(op_combo.Substring(1));
        bool my_better = isBetter(my_n, my_rank, op_n, op_rank);
        bool op_better = isBetter(op_n, op_rank, my_n, my_rank);
        if (my_better && !op_better)
            return 1;
        else if (op_better && !my_better)
            return -1;
        else
            return 0;
    }
}

