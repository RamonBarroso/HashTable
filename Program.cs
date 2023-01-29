using System;

namespace HashTable
{
    class Program
    {
        public static void Main(string[] args)
        {
            AssociatArray<string, int> ar = new(20);
            ar.Add("Teste", 1);
            ar.Add("Ramon", 2);
            ar.Add("Oliveira", 3);
            ar.Add("Dias", 4);
            ar.Add("Barroso", 5);
            Console.WriteLine(ar.Get("Barroso"));
        }
    }

    class AssociatArray<Tkey, Tvalue> where Tkey : IComparable<Tkey>
    {

        public int Count { get; private set; }
        KeyValuePair<Tkey, Tvalue>[] _data;
        public AssociatArray(int capacity)
        {
            _data = new KeyValuePair<Tkey, Tvalue>[capacity];
        }
        public void Add(Tkey key, Tvalue value)
        {
            if (Count == _data.Length)
                throw new InvalidOperationException();


            var hash = Math.Abs(key.GetHashCode());
            var position = hash % _data.Length;
            while (_data[position].Value != null)
            {
                if (_data[position].Key.CompareTo(key) == 0)
                    throw new Exception();
                position = (position + 1) % _data.Length;

            }
            _data[position] = new KeyValuePair<Tkey, Tvalue>(key, value);
            Count++;

        }

        public Tvalue Get(Tkey key)
        {
            var hash = Math.Abs(key.GetHashCode());
            var position = hash % _data.Length;
            var breakPosition = position;
            while (_data[position].Value != null)
            {
                if (_data[position].Key.CompareTo(key) == 0)
                    return _data[position].Value;

                position = (position + 1) % _data.Length;

                if (position == breakPosition)
                    throw new KeyNotFoundException();
            }
            return _data[position].Value;
        }
    }

}