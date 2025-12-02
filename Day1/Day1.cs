using System;
namespace AdventOfCode {
	public class Day1 {

		private int _currentPosition;

		private List<string> _instructions = new List<string>();

		private int _sum;

		public Day1(string path)
		{
			_currentPosition = 50;
			_sum = 0;
			_instructions = File.ReadAllLines(path).ToList();

		}

		public int Solver()
		{
			for (int i = 0; i < _instructions.Count; i++)
			{
				bool add = _instructions[i][0] == 'R' ? true : false;
				int amount = int.Parse(_instructions[i].Remove(0, 1));

				int oldPos = _currentPosition;
				_currentPosition = _currentPosition + (add ? amount : -amount);

				if (Math.Sign(oldPos) != Math.Sign(_currentPosition) && oldPos != 0) _sum++;

				_sum += Math.Abs(_currentPosition / 100);


				_currentPosition %= 100;
			}
			return _sum;
		}
	}
}