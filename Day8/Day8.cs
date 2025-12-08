

namespace AdventOfCode {
	public class Day8 {


		private List<(int, int, int)> _coords = new();

		private int _multiplicatonResult = 1;
		public Day8(string path)
		{
			var lines = File.ReadAllLines(path);

			for (int i = 0; i < lines.Length; i++)
			{
				var splitCoords = lines[i].Split(",");
				_coords.Add(new(int.Parse(splitCoords[0]), int.Parse(splitCoords[1]), int.Parse(splitCoords[2])));

			}
		}

		public int Solve(int numberOfConnections, int numberOfMultiplications)
		{
			_multiplicatonResult = 1;

			List<(double, (int, int))> distances = new();
			List<(int, int)> connections = new();
			List<List<int>> circuits = new();

			for (int i = 0; i < _coords.Count; i++)
			{
				for (int j = i + 1; j < _coords.Count; j++)
				{
					var box1 = _coords[i];
					var box2 = _coords[j];

					var distance = Math.Sqrt(Math.Pow(box2.Item1 - box1.Item1, 2) + Math.Pow(box2.Item2 - box1.Item2, 2) + Math.Pow(box2.Item3 - box1.Item3, 2));
					distances.Add((distance, (i, j)));

				}
			}

			distances = distances.OrderBy((d) => d.Item1).ToList();
			int m = 0;
			(int, int) lastPair = new();
			bool isFirstLap = true;
			circuits.Add(new());
			connections.Add(distances[m].Item2);
			circuits[m].Add(connections[m].Item1);
			circuits[m].Add(connections[m].Item2);
			while (circuits.Count != 1 || circuits[0].Count != _coords.Count)
			{


				if (m != 0)
				{
					connections.Add(distances[m].Item2);
					bool newEntry = true;
					for (int j = 0; j < circuits.Count; j++)
					{
						for (int k = 0; k < circuits[j].Count; k++)
						{
							if (connections[m].Item1 == circuits[j][k] || connections[m].Item2 == circuits[j][k])
							{
								newEntry = false;
								bool item1In = connections[m].Item1 == circuits[j][k];
								bool alreadyIn = false;
								for (int l = k + 1; l < circuits[j].Count; l++)
								{
									if ((item1In ? connections[m].Item2 : connections[m].Item1) == circuits[j][l])
									{
										alreadyIn = true;
										break;
									}

								}


								if (!alreadyIn)
								{
									bool connectRows = false;
									for (int l = 0; l < circuits.Count; l++)
									{
										if (circuits[l].Contains(item1In ? connections[m].Item2 : connections[m].Item1))
										{
											connectRows = true;
											circuits[j].AddRange(circuits[l]);
											circuits.RemoveAt(l);
											lastPair = connections[m];
											break;
										}
									}
									if (!connectRows)
									{

										circuits[j].Add(item1In ? connections[m].Item2 : connections[m].Item1);
										lastPair = connections[m];
									}

								}
								break;
							}
						}
						if (!newEntry)
							break;
					}
					if (newEntry)
					{

						circuits.Add(new());
						circuits[circuits.Count - 1].Add(connections[m].Item1);
						circuits[circuits.Count - 1].Add(connections[m].Item2);
					}
				}
				m++;
			}

			_multiplicatonResult = _coords[lastPair.Item1].Item1 * _coords[lastPair.Item2].Item1;
			//circuits.Sort((a, b) => b.Count - a.Count);



			//for (int i = 0; i < numberOfMultiplications; i++)
			//{
			//	_multiplicatonResult *= circuits[i].Count;
			//}

			return _multiplicatonResult;

		}

	}
}