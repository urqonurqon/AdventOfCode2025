

namespace AdventOfCode {
	public class Day6 {


		private List<List<char>> _inputNumbers;
		private List<char> _operations;

		private double _sum;
		public Day6(string path)
		{
			var lines = File.ReadAllLines(path);
			//_numOfOperands = lines.Length - 1;

			_inputNumbers = new();
			_operations = new();
			for (int i = 0; i < lines.Length; i++)
			{
				if (i == lines.Length - 1)
				{
					var splitString = lines[i].Split(" ");
					for (int j = 0; j < splitString.Length; j++)
					{
						if (splitString[j] == "" || splitString[j] == " ")
						{
							_operations.Add('n');
						}
						else
						{

							_operations.Add(char.Parse(splitString[j]));
						}
					}
				}
				else
				{
					_inputNumbers.Add(new());
					var splitString = lines[i].Split(" ");
					for (int j = 0; j < splitString.Length; j++)
					{
						if (splitString[j] == "" || splitString[j] == " ")
						{
							_inputNumbers[i].Add('n');
						}


						for (int k = 0; k < splitString[j].Length; k++)
						{

							_inputNumbers[i].Add(splitString[j][k]);
						}

					}

				}
			}


		}


		public double Solve()
		{
			_sum = 0;
			for (int i = 0; i < _inputNumbers[0].Count; i++)
			{
				bool isSum = _operations[i] == '+';
				double columnSolution = isSum ? 0 : 1;
				for (int j = 0; j < _inputNumbers.Count; j++)
				{
					if (isSum)
					{
						columnSolution += _inputNumbers[j][i];
					}
					else
					{

						columnSolution *= _inputNumbers[j][i];
					}
				}
				_sum += columnSolution;
			}
			return _sum;
		}

		public double SolvePartTwo()
		{
			_sum = 0;


			char rememberedOperation = 'n';
			double columnSolution = 0;
			List<double> numbersToOperateWith = new();
			for (int i = 0; i < _operations.Count; i++)
			{
				if (_operations[i] != 'n')
				{
					numbersToOperateWith = new();
					rememberedOperation = _operations[i];
					for (int j = i + 1; j < _operations.Count; j++)
					{
						if (_operations[j] == '+' || _operations[j] == '*') break;
					}
					columnSolution = rememberedOperation == '+' ? 0 : 1;
				}

				double number = 0;
				int numberOfEmptyDigits = 0;
				int numberOfExpectedDigits = _inputNumbers.Count;
				for (int j = 0; j < _inputNumbers.Count; j++)
				{
					var digit = _inputNumbers[j][i];


					if (digit == 'n')
					{
						numberOfEmptyDigits++;
					}
					else
					{
						number += (digit - '0') * Math.Pow(10, numberOfExpectedDigits - 1);
						numberOfExpectedDigits--;
					}

				}
				number /= Math.Pow(10, numberOfEmptyDigits);
				numbersToOperateWith.Add(number);

				if (i == _operations.Count - 1 || _operations[i + 1] != 'n')
				{
					for (int j = 0; j < numbersToOperateWith.Count; j++)
					{

						if (rememberedOperation == '+')
						{
							columnSolution += numbersToOperateWith[j];
						}
						else
						{
							columnSolution *= numbersToOperateWith[j];

						}
					}
					_sum += columnSolution;
				}
			}



			return _sum;
		}

	}
}