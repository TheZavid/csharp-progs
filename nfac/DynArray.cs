using System;

class DynArray
{
        int[] _data = new int[100];
        int _cur_length = 0;

        public void set(int index, int val) {
                if (index >= _cur_length)
                        append(val);
                else
                        _data[index] = val;
        }

        public int get(int index) => _data[index];

        public void append(int val) {
                if (_cur_length == _data.Length) {
                int[] old = _data;
                _data = new int[old.Length * 2];
                for (int i = 0; i < old.Length; i++) {
                        _data[i] = old[i];
                }
                }
                _data[_cur_length++] = val;
        }

        public int getLength() => _cur_length;

        public void setLength(int length) {
                if (length > _cur_length) {
                int to_add = length - _cur_length;
                for (int i = 0; i < to_add; i++) {
                        append(0);
                }
                } else
                _cur_length = length;
        }
}