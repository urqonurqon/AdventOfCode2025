

namespace AdventOfCode {
	public class Day7 {


		private List<List<char>> _takyonGraph;
		private List<List<char>> _takyonGraph2;


		private int _sum;
		public int PartTwoSum;
		public Day7(string path)
		{
			var lines = File.ReadAllLines(path);
			PartTwoSum = 0;
			_takyonGraph = new();
			_takyonGraph2 = new();
			for (int i = 0; i < lines.Length; i++)
			{
				_takyonGraph.Add([.. lines[i]]);
				_takyonGraph2.Add([.. lines[i]]);
			}
			SolvePartTwo(_takyonGraph, true);
			SolvePartTwo(_takyonGraph2, false);

			PartTwoSum /= 2;
		}


		public int Solve()
		{
			_sum = 0;


			for (int i = 1; i < _takyonGraph.Count; i++)
			{
				for (int j = 0; j < _takyonGraph[i].Count; j++)
				{

					if (_takyonGraph[i - 1][j] == 'S' || (_takyonGraph[i - 1][j] == '|' && _takyonGraph[i][j] != '^'))
					{
						_takyonGraph[i][j] = '|';
					}
					if (j < _takyonGraph[i].Count - 1)
						if (_takyonGraph[i][j + 1] == '^' && _takyonGraph[i - 1][j + 1] == '|')
						{
							_sum++;
							_takyonGraph[i][j] = '|';
							if (j + 2 < _takyonGraph[i].Count)
								_takyonGraph[i][j + 2] = '|';


						}

				}
			}

			return _sum;
		}

		public void SolvePartTwo(List<List<char>> takyonGraph, bool left)
		{




			if (takyonGraph.Count > 2)
			{



				for (int i = 1; i < takyonGraph.Count; i++)
				{
					for (int j = 0; j < takyonGraph[i].Count; j++)
					{
						if (takyonGraph[i - 1][j] == 'S' || (takyonGraph[i - 1][j] == '|' && takyonGraph[i][j] != '^'))
						{
							takyonGraph[i][j] = '|';
						}
						if (j < takyonGraph[i].Count - 1)
							if (takyonGraph[i][j + 1] == '^' && takyonGraph[i - 1][j + 1] == '|')
							{


								if (left)
								{
									takyonGraph[i][j] = '|';
								}
								else
								{

									if (j + 2 < takyonGraph[i].Count)
										takyonGraph[i][j + 2] = '|';
								}

								List<List<char>> newTakyonL = new();
								List<List<char>> newTakyonR = new();
								for (int k = i; k < takyonGraph.Count; k++)
								{
									newTakyonL.Add(new(takyonGraph[k]));
									newTakyonR.Add(new(takyonGraph[k]));
								}


								if (left)
								{
									newTakyonL[0][j] = 'S';
									newTakyonR[0][j] = 'S';
								}
								else
								{

									if (j + 2 < takyonGraph[i].Count)
									{
										newTakyonL[0][j + 2] = 'S';
										newTakyonR[0][j + 2] = 'S';

									}
								}
								//Console.WriteLine("\n\nTakyon dole:\n");
								//for (int o = 0; o < takyonGraph.Count; o++)
								//{
								//	Console.WriteLine("\n");
								//	for (int u = 0; u < takyonGraph[o].Count; u++)
								//	{
								//		Console.Write(takyonGraph[o][u]);
								//	}
								//}

								SolvePartTwo(newTakyonL, true);
								SolvePartTwo(newTakyonR, false);
								//PartTwoSum++;
								return;

							}

					}
				}
			}

			PartTwoSum++;
			//Console.WriteLine(PartTwoSum);



		}


	}
}