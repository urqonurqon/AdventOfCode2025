

using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day5
    {

        private List<double> _freshIngredients = new List<double>();
        private List<(double, double)> _ranges = new();

        private double _sum;
        public Day5(string path)
        {
            _sum = 0;
            var fileString = File.ReadAllText(path);
            List<string> splitString = fileString.Split("\r\n\r\n").ToList();

            var rangeList = splitString[0].Split("\r\n");

            _ranges.Clear();
            for (int i = 0; i < rangeList.Length; i++)
            {
                var numbers = rangeList[i].Split('-');
                _ranges.Add(new(double.Parse(numbers[0].ToString()), double.Parse(numbers[1].ToString())));
            }

            var freshIngredientsList = splitString[1].Split("\r\n");

            _freshIngredients.Clear();
            for (int i = 0; i < freshIngredientsList.Length; i++)
            {
                _freshIngredients.Add(double.Parse(freshIngredientsList[i].ToString()));
            }
        }

        public double Solve()
        {
            _sum = 0;

            for (int i = 0; i < _freshIngredients.Count; i++)
            {
                var freshIngredient = _freshIngredients[i];

                for (int j = 0; j < _ranges.Count; j++)
                {
                    if (freshIngredient >= _ranges[j].Item1 && freshIngredient <= _ranges[j].Item2)
                    {
                        _sum++;
                        break;
                    }
                }


            }
            return _sum;
        }

        public double SolvePartTwo()
        {
            _sum = 0;

            _ranges = _ranges.OrderBy((s) => s.Item1).ToList();

            for (int i = 0; i < _ranges.Count; i++)
            {
                for (int j = i + 1; j < _ranges.Count; j++)
                {

                    if (CheckIfCompletlyInsideOtherRange(_ranges[i], _ranges[j])) _ranges.Remove(_ranges[j]);
                }
            }

            double minNumber = _ranges[0].Item1;
            double maxNumber = _ranges[0].Item2;
            for (int i = 0; i < _ranges.Count; i++)
            {

                if (_ranges[i].Item2 > maxNumber) maxNumber = _ranges[i].Item2;


                if (i == _ranges.Count - 1) break;
                if (CheckIfRangesDontOverlap(_ranges[i], _ranges[i + 1]))
                {
                    _sum -= _ranges[i + 1].Item1 - _ranges[i].Item2 - 1;
                }
            }

            _sum += maxNumber - minNumber + 1;
            return _sum;

        }







        private bool CheckIfRangesDontOverlap((double, double) value1, (double, double) value2)
        {
            return value2.Item1 > value1.Item2;
        }

        private bool CheckIfCompletlyInsideOtherRange((double, double) value1, (double, double) value2)
        {
            return value2.Item1 >= value1.Item1 && value2.Item2 <= value1.Item2;
        }
    }
}