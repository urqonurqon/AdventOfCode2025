

namespace AdventOfCode
{
  public class Day9
  {


    private List<(int, int)> _tileCoords = new List<(int, int)>();

    private double _area;

    public Day9(string path)
    {
      var lines = File.ReadAllLines(path);
      for (int i = 0; i < lines.Length; i++)
      {
        var splitLines = lines[i].Split(',');

        _tileCoords.Add((int.Parse(splitLines[1]), int.Parse(splitLines[0])));
      }
    }

    public double Solve()
    {
      _area = 0;

      List<(int, int)> edgeDotsBig = new();
      List<(int, int)> cornerDotsBig = new();
      for (int i = 0; i < _tileCoords.Count; i++)
      {


        if (_tileCoords[i].Item1 == _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1)
        {


          if (_tileCoords[i].Item2 > _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item1 > _tileCoords[i].Item1)
          {
            //for (int j = _tileCoords[i].Item2; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2; j++)
            //{
            //  edgeDotsBig.Add((_tileCoords[i].Item1 - 1, j - 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 + 1, _tileCoords[i].Item2 - 1));
            //}
          }
          else if (_tileCoords[i].Item2 > _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item1 < _tileCoords[i].Item1)
          {
            //for (int j = _tileCoords[i].Item2; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2; j++)
            //{
            //  edgeDotsBig.Add((_tileCoords[i].Item1 - 1, j + 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 + 1, _tileCoords[i].Item2 + 1));
            //}

          }
          if (_tileCoords[i].Item2 < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item1 > _tileCoords[i].Item1)
          {
            //for (int j = _tileCoords[i].Item2; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2; j++)
            //{
            //  edgeDotsBig.Add((_tileCoords[i].Item1 + 1, j - 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 - 1, _tileCoords[i].Item2 - 1));
            //}
          }
          else if (_tileCoords[i].Item2 < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item1 < _tileCoords[i].Item1)
          {
            //for (int j = _tileCoords[i].Item2; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item2; j++)
            //{
            //  edgeDotsBig.Add((_tileCoords[i].Item1 + 1, j + 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 - 1, _tileCoords[i].Item2 + 1));
            //}

          }


        }
        else
        {


          if (_tileCoords[i].Item1 > _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item2 > _tileCoords[i].Item2)
          {
            //for (int j = _tileCoords[i].Item1; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1; j++)
            //{
            //  edgeDotsBig.Add((j - 1, _tileCoords[i].Item2 - 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 + 1, _tileCoords[i].Item2 - 1));
            //}
          }
          else if (_tileCoords[i].Item1 > _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item2 < _tileCoords[i].Item2)
          {
            //for (int j = _tileCoords[i].Item1; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1; j++)
            //{
            cornerDotsBig.Add((_tileCoords[i].Item1 - 1, _tileCoords[i].Item2 - 1));
            //}
          }
          else if (_tileCoords[i].Item1 < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item2 > _tileCoords[i].Item2)
          {
            //for (int j = _tileCoords[i].Item1; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1; j++)
            //{
            //  edgeDotsBig.Add((j + 1, _tileCoords[i].Item2 - 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 + 1, _tileCoords[i].Item2 + 1));
            //}
          }
          else if (_tileCoords[i].Item1 < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1 && _tileCoords[i == 0 ? _tileCoords.Count - 1 : i - 1].Item2 < _tileCoords[i].Item2)
          {
            //for (int j = _tileCoords[i].Item1; j < _tileCoords[i == _tileCoords.Count - 1 ? 0 : i + 1].Item1; j++)
            //{
            //  edgeDotsBig.Add((j + 1, _tileCoords[i].Item2 + 1));
            cornerDotsBig.Add((_tileCoords[i].Item1 - 1, _tileCoords[i].Item2 + 1));
            //}
          }




        }
      }


      for (int i = 0; i < _tileCoords.Count; i++)
      {
        Console.WriteLine(i);


        for (int j = i + 2; j < _tileCoords.Count; j++)
        {
        Console.WriteLine(j);

          (int, int) bootomLeft = new();
          (int, int) topRight = new();

          if (_tileCoords[i].Item1 > _tileCoords[j].Item1)
          {
            bootomLeft = _tileCoords[j];
            topRight = _tileCoords[i];
          }
          else if (_tileCoords[i].Item1 == _tileCoords[j].Item1 && _tileCoords[i].Item2 > _tileCoords[j].Item2)
          {
            bootomLeft = _tileCoords[j];
            topRight = _tileCoords[i];
          }
          else
          {
            topRight = _tileCoords[j];
            bootomLeft = _tileCoords[i];
          }


          List<(int, int)> edgeDots = new();


          for (int k = bootomLeft.Item2; k <= topRight.Item2; k++)
          {
            edgeDots.Add((bootomLeft.Item1, k));
          }
          for (int k = bootomLeft.Item1; k <= topRight.Item1; k++)
          {
            edgeDots.Add((k, topRight.Item2));
          }
          for (int k = bootomLeft.Item2; k <= topRight.Item2; k++)
          {
            edgeDots.Add((topRight.Item1, k));
          }
          for (int k = bootomLeft.Item1; k <= topRight.Item1; k++)
          {
            edgeDots.Add((k, bootomLeft.Item2));
          }

          edgeDots = edgeDots.Distinct().ToList();

          bool possible = true;
          for (int k = 0; k < edgeDots.Count; k++)
          {
            for (int l = 0; l < cornerDotsBig.Count; l++)
            {
              if ((edgeDots[k].Item1 - cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item1) * (cornerDotsBig[l].Item2 - cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2) == (edgeDots[k].Item2 - cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2) * (cornerDotsBig[l].Item1 - cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item1))
              {
                if (cornerDotsBig[l].Item2 == cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2)
                {
                  if (cornerDotsBig[l].Item1 > cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item1)
                  {
                    if (edgeDots[k].Item1 < cornerDotsBig[l].Item1 && edgeDots[k].Item1 > cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item1)
                    {
                      possible = false;
                      break;
                    }

                  }
                  else
                  {
                    if (edgeDots[k].Item1 > cornerDotsBig[l].Item1 && edgeDots[k].Item1 < cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item1)
                    {
                      possible = false;
                      break;
                    }
                  }
                }
                else
                {
                  if (cornerDotsBig[l].Item2 > cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2)
                  {
                    if (edgeDots[k].Item2 < cornerDotsBig[l].Item2 && edgeDots[k].Item2 > cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2)
                    {
                      possible = false;
                      break;
                    }

                  }
                  else
                  {
                    if (edgeDots[k].Item2 > cornerDotsBig[l].Item2 && edgeDots[k].Item2 < cornerDotsBig[l == 0 ? cornerDotsBig.Count - 1 : l - 1].Item2)
                    {
                      possible = false;
                      break;
                    }
                  }
                }
               
              }
            }
            if (!possible) break;

          }


          if (possible)
          {


            double a = Math.Abs(_tileCoords[i].Item1 - _tileCoords[j].Item1) + 1;
            double b = Math.Abs(_tileCoords[i].Item2 - _tileCoords[j].Item2) + 1;



            double area = a * b;
            if (area > _area)
            {
              _area = area;
            }
          }
        }
      }




      return _area;
    }

  }
}