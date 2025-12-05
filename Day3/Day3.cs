

namespace AdventOfCode {
	public class Day3 {


		private List<List<int>> _batteries;
		private double _sum;

		public Day3(string path)
		{
			var banks = File.ReadAllLines(path);
			_batteries = new List<List<int>>();
			_sum = 0;


			for (int i = 0; i < banks.Length; i++)
			{
				var bank = banks[i];
				_batteries.Add(new List<int>());
				for (int j = 0; j < bank.Length; j++)
				{
					int battery = int.Parse(bank[j].ToString());
					_batteries[i].Add(battery);
				}
			}
		}

		public double Solve(int digits)
		{
			for (int i = 0; i < _batteries.Count; i++)
			{
				//List<int> max = new List<int>();
				int indexToStartFrom = -1;
				double sum = 0;
				for (int k = 0; k < digits; k++)
				{
					int max = 0;
					int limit = _batteries[i].Count - (digits-k);
					for (int j = indexToStartFrom + 1; j <= limit; j++)
					{
						if (_batteries[i][j] > max)
						{
							max = _batteries[i][j];
							indexToStartFrom = j;
							if (max == 9) break;
						}

					}
					sum += max * Math.Pow(10, digits - (k + 1));
				}
				_sum += sum;
			}
			return _sum;
		}

	}
}