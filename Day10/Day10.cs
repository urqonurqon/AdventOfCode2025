


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

            var numbers = splitLinesBySpace[j].Split(",");

            for (int l = 0; l < numbers.Length; l++)
            {
              numbers[l] = numbers[l].Replace("{", "");
              numbers[l] = numbers[l].Replace("}", "");
              _joltages[i].Add(int.Parse(numbers[l]));
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


        _sumOfFewestButtonPresses += GenerateAllCombinations(_buttonWirings[i], i, true);



      }

      return _sumOfFewestButtonPresses;

    }

    private int GenerateAllCombinations(List<List<int>> S, int rowIndex, bool partOne)
    {
      for (int size = 1; size <= S.Count; size++)
      {
        if (partOne)
        {

          if (GenerateCombinationsOfSize(S, size, 0, new List<List<int>>(), rowIndex, _lights)) return size;
        }
        else
        {

          if (GenerateCombinationsOfSize(S, size, 0, new List<List<int>>(), rowIndex, _joltages)) return size;
        }

      }
      return 0;
    }

    private bool GenerateCombinationsOfSize(List<List<int>> S, int targetSize, int startIndex, List<List<int>> current, int rowIndex, List<List<bool>> lights)
    {
      if (current.Count == targetSize)
      {
        int[] numberOfIndices = new int[lights[rowIndex].Count];
        for (int i = 0; i < current.Count; i++)
        {
          for (int j = 0; j < current[i].Count; j++)
          {
            numberOfIndices[current[i][j]]++;


          }
        }
        bool valid = true;
        for (int i = 0; i < lights[rowIndex].Count; i++)
        {
          if (lights[rowIndex][i] && numberOfIndices[i] % 2 != 0)
          {
            valid = false;
            break;
          }
          if (!lights[rowIndex][i] && numberOfIndices[i] % 2 == 0)
          {
            valid = false;
            break;
          }
        }

        return valid;
      }

      for (int i = startIndex; i < S.Count; i++)
      {
        current.Add(S[i]);
        if (GenerateCombinationsOfSize(S, targetSize, i, current, rowIndex, lights)) return true;
        current.RemoveAt(current.Count - 1);
      }

      return false;
    }

    private bool GenerateCombinationsOfSize(List<List<int>> S, int targetSize, int startIndex, List<List<int>> current, int rowIndex, List<List<int>> joltages)
    {
      if (current.Count == targetSize)
      {
        int[] numberOfIndices = new int[joltages[rowIndex].Count];
        for (int i = 0; i < current.Count; i++)
        {
          for (int j = 0; j < current[i].Count; j++)
          {
            numberOfIndices[current[i][j]]++;


          }
        }
        bool valid = true;
        for (int i = 0; i < _lights[rowIndex].Count; i++)
        {
          if (joltages[rowIndex][i] != numberOfIndices[i])
          {
            valid = false;
            break;
          }
        }

        return valid;
      }

      for (int i = startIndex; i < S.Count; i++)
      {
        current.Add(S[i]);
        if (GenerateCombinationsOfSize(S, targetSize, i, current, rowIndex, joltages)) return true;
        current.RemoveAt(current.Count - 1);
      }

      return false;
    }




    public int SolvePartTwo()
    {
      _sumOfFewestButtonPresses = 0;

      for (int i = 0; i < _joltages.Count; i++)
      {
        int maxButton = 0;
        int maxButtonIndex = 0;
        int rowSum = 0;
        bool allZeros = true;
        int indexOfEqualMaxButtons = 0;
        do
        {
          List<int> mapOfWeights = new();
          for (int j = 0; j < _buttonWirings[i].Count; j++)
          {
            int weightSum = 0;
            for (int k = 0; k < _buttonWirings[i][j].Count; k++)
            {
              weightSum += _joltages[i][_buttonWirings[i][j][k]];
            }
            mapOfWeights.Add(weightSum);
          }

          maxButton = mapOfWeights.Max();
          List<int> maxButtons = new();
          for (int j = 0; j < mapOfWeights.Count; j++)
          {
            if (mapOfWeights[j] == maxButton) maxButtons.Add(mapOfWeights[j]);
          }
          maxButtonIndex = mapOfWeights.FindIndex((i) => i == maxButtons[indexOfEqualMaxButtons]);
          indexOfEqualMaxButtons++;

          if (!PressButton(_buttonWirings[i][maxButtonIndex], _joltages[i]))
          {

          }
          rowSum++;


          for (int j = 0; j < _joltages[i].Count; j++)
          {
            if (_joltages[i][j] != 0)
              allZeros = false;
          }

        } while (!allZeros);
        _sumOfFewestButtonPresses += rowSum;
      }

      return _sumOfFewestButtonPresses;
    }

    private bool PressButton(List<int> buttons, List<int> jolts)
    {
      for (int i = 0; i < buttons.Count; i++)
      {
        jolts[buttons[i]]--;
        if (jolts[buttons[i]] < 0)
          return false;
      }
      return true;
    }
  }
}
