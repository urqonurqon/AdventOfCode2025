

namespace AdventOfCode {
	public class Day4 {

		private List<List<string>> _positionGrid = new();
		public int Sum;
		public Day4(string path)
		{
			Sum = 0;
			var row = File.ReadAllLines(path);
			for (int i = 0; i < row.Length; i++)
			{
				_positionGrid.Add(new());
				for (int j = 0; j < row[i].Length; j++)
				{
					var pos = row[i][j];
					_positionGrid[i].Add(pos.ToString());
				}
			}

			RemovePaperRoles();
		}

		public void RemovePaperRoles()
		{
			int sum = 0;
			for (int i = 0; i < _positionGrid.Count; i++)
			{
				for (int j = 0; j < _positionGrid[i].Count; j++)
				{
					if (_positionGrid[i][j] != "@") continue;
					int count = 0;
					for (int k = -1; k <= 1; k++)
					{
						for (int l = -1; l <= 1; l++)
						{
							if (k == 0 && l == 0) continue;
							if (CheckPos(i, j, k, l))
							{
								count++;
							}
						}
					}


					if (count < 4)
					{
						sum++;

						_positionGrid[i][j] = "x";
					}
				}
			}
			if (sum != 0)
			{
				Sum += sum;
				RemovePaperRoles();
			}
		}

		private bool CheckPos(int i, int j, int offsetX, int offsetY)
		{
			if (i == 0 && offsetX == -1 || i == _positionGrid.Count - 1 && offsetX == 1 || j == 0 && offsetY == -1 || j == _positionGrid[i].Count - 1 && offsetY == 1) return false;
			if (_positionGrid[i + offsetX][j + offsetY] == "@") return true;
			return false;

		}

	}
}