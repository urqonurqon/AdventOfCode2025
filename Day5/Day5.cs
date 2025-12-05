

using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day5
    {

        private List<int> _freshIngredients = new List<int>();
        private List<(int, int)> _ranges = new();

        private int _sum;
        public Day5(string path)
        {
            _sum = 0;
            var fileString = File.ReadAllLines(path);
            List<string> splitString = path.Split("\r\n\r\n").ToList();

            _ranges.Clear();
            for (int i = 0; i < splitString[0].Length; i++)
            {
                var numbers = splitString[0][i].ToString().Split('-');
                _ranges.Add(new(int.Parse(numbers[0].ToString()), int.Parse(numbers[1].ToString())));
            }


            _freshIngredients.Clear();
            for (int i = 0; i < splitString[1].Length; i++)
            {
                _freshIngredients.Add(int.Parse(splitString[1][i].ToString()));
            }
        }

        public int Solve()
        {
            for (int i = 0; i < _ranges.Count; i++)
            {
                for (int j = _ranges[i].Item1; j <= _ranges[i].Item2; j++)
                {
                    if (_freshIngredients.Exists((ingredient) => ingredient == j))
                    {
                        _freshIngredients.Remove(j);
                        _sum++;
                    }
                }
            }
            return _sum;
        }

    }
}