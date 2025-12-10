

namespace AdventOfCode
{
  public class Day10
  {

    private List<List<List<int>>> _buttonWirings = new();
    private List<List<bool>> _lights = new();
    private List<List<int>> _joltages = new();

    private int _sumOfFewestButtonPresses;

    public Day10(string path)
    {
      var lines = File.ReadAllLines(path);

      for (int i = 0; i < lines.Length; i++)
      {
        var splitLinesBySpace = lines[i].Split(" ");

        _buttonWirings.Add(new());
        _lights.Add(new());
        _joltages.Add(new());


        for (int j = 0; j < splitLinesBySpace.Length; j++)
        {
          if (j == 0)
          {
            for (int k = 1; k < splitLinesBySpace[j].Length - 1; k++)
            {
              _lights[i].Add(splitLinesBySpace[j][k] == '.');
            }
          }
          else if (j == splitLinesBySpace.Length - 1)
          {
            for (int k = 1; k < splitLinesBySpace[j].Length - 1; k++)
            {
              if (splitLinesBySpace[j][k] != ',')
                _joltages[i].Add(splitLinesBySpace[j][k] - '0');
            }


          }
          else
          {
            _buttonWirings[i].Add(new());
            for (int k = 1; k < splitLinesBySpace[j].Length - 1; k++)
            {
              if (splitLinesBySpace[j][k] != ',')
                _buttonWirings[i][j - 1].Add(splitLinesBySpace[j][k] - '0');

            }
          }


        }


      }

    }

    public int Solve()
    {
      _sumOfFewestButtonPresses = 0;

      for (int i = 0; i < _lights.Count; i++)
      {

        while (true)
        {


          int[] timesJShowingUpTable = new int[_lights[i].Count];


          for (int j = 0; j < _lights[i].Count; j++)
          {

            for (int k = 0; k < _buttonWirings[i].Count; k++)
            {
              if (_buttonWirings[i][k].Contains(j))
              {
                timesJShowingUpTable[j]++;

              }

            }
          }
        }
      }



      return _sumOfFewestButtonPresses;

    }



  }
}
