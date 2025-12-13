using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace AdventOfCode
{
  public class Day10
  {
    private List<List<List<int>>> _buttonWirings = new();
    private List<List<bool>> _lights = new();
    private List<List<int>> _joltages = new();

    private Stopwatch _rowStopwatch;
    private long _statesExplored;
    private long _lastStatesCount;
    private DateTime _lastUpdateTime;

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
              numbers[l] = numbers[l].Replace("{", "").Replace("}", "");
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

    public long Solve()
    {
      long total = 0;
      for (int i = 0; i < _lights.Count; i++)
      {
        total += SolvePartOneForRow(i);
      }
      return total;
    }

    private long SolvePartOneForRow(int row)
    {
      var lights = new bool[_lights[row].Count];
      return DFSPartOne(lights, _lights[row], _buttonWirings[row], 0, new Dictionary<string, long>());
    }

    private long DFSPartOne(bool[] lights, List<bool> target, List<List<int>> buttons, int start, Dictionary<string, long> memo)
    {
      if (lights.SequenceEqual(target))
        return 0;

      if (start == buttons.Count)
        return -1;

      string key = $"{string.Join(",", lights.Select(b => b ? "1" : "0"))},{start}";
      if (memo.TryGetValue(key, out var cached))
        return cached;

      long result = long.MaxValue;

      for (int j = start; j < buttons.Count; j++)
      {
        foreach (var k in buttons[j])
          lights[k] = !lights[k];

        long subResult = 1 + DFSPartOne(lights, target, buttons, j + 1, memo);
        if (subResult >= 0 && subResult < result)
          result = subResult;

        foreach (var k in buttons[j])
          lights[k] = !lights[k];
      }

      memo[key] = result;
      return result;
    }

    public int SolvePartTwo()
    {
      int total = 0;

      Console.WriteLine($"Solving {_joltages.Count} rows...\n");

      for (int i = 0; i < _joltages.Count; i++)
      {
        Console.WriteLine($"Row {i + 1}/{_joltages.Count}: [{string.Join(",", _joltages[i])}]");

        _rowStopwatch = Stopwatch.StartNew();
        _statesExplored = 0;
        _lastStatesCount = 0;
        _lastUpdateTime = DateTime.Now;

        int result = SolvePartTwoForRow(i);
        total += result;

        _rowStopwatch.Stop();
        Console.WriteLine($"Result: {result} presses ({_rowStopwatch.ElapsedMilliseconds}ms, {_statesExplored:N0} states)\n");
      }

      return total;
    }

    private int SolvePartTwoForRow(int row)
    {
      var joltage = _joltages[row].ToList();
      var buttons = _buttonWirings[row];

      int allButtonsMask = (1 << buttons.Count) - 1;

      var memo = new Dictionary<string, int>();

      // Estimate initial complexity
      EstimateComplexity(joltage, buttons);

      return DFSPartTwoWithProgress(joltage, allButtonsMask, buttons, memo);
    }

    private void EstimateComplexity(List<int> joltage, List<List<int>> buttons)
    {
      // Very rough estimation based on:
      // 1. Number of non-zero joltages
      // 2. Maximum joltage value
      // 3. Average number of buttons affecting each position

      int nonZeroCount = joltage.Count(j => j > 0);
      int maxJoltage = joltage.Max();
      double avgAffectingButtons = 0;

      for (int i = 0; i < joltage.Count; i++)
      {
        if (joltage[i] > 0)
        {
          int affectingCount = 0;
          for (int btn = 0; btn < buttons.Count; btn++)
          {
            if (buttons[btn].Contains(i))
              affectingCount++;
          }
          avgAffectingButtons += affectingCount;
        }
      }

      if (nonZeroCount > 0)
        avgAffectingButtons /= nonZeroCount;

      // VERY ROUGH ESTIMATE FORMULA (purely heuristic)
      // Complexity grows exponentially with number of constrained positions
      // But our algorithm prunes heavily by targeting most constrained first

      double estimatedComplexity = Math.Pow(maxJoltage + 1, Math.Min(3, nonZeroCount)) * Math.Pow(avgAffectingButtons, 2);

      Console.WriteLine($"  Complexity estimate: ~{estimatedComplexity:N0} states (very rough)");

      if (estimatedComplexity > 1000000)
        Console.WriteLine($"  WARNING: This might take a while...");
    }

    private bool IsButtonAvailable(int buttonIndex, int mask)
    {
      return (mask & (1 << buttonIndex)) != 0;
    }

    private int DFSPartTwoWithProgress(List<int> joltage, int availableButtonsMask, List<List<int>> buttons, Dictionary<string, int> memo)
    {
      _statesExplored++;

      // Show progress with time estimate every 5 seconds
      if ((DateTime.Now - _lastUpdateTime).TotalSeconds >= 5.0)
      {
        _lastUpdateTime = DateTime.Now;
        double elapsedSeconds = _rowStopwatch.Elapsed.TotalSeconds;

        if (elapsedSeconds > 0)
        {
          double statesPerSecond = _statesExplored / elapsedSeconds;

          // Estimate remaining time based on current speed
          // This is VERY approximate since search space isn't uniform
          double estimatedTotalStates = _statesExplored * 10; // Wild guess multiplier
          double estimatedRemainingSeconds = (estimatedTotalStates - _statesExplored) / statesPerSecond;

          Console.Write($"  Progress: {_statesExplored:N0} states");
          Console.Write($", {statesPerSecond:N0}/s");
          Console.Write($", {elapsedSeconds:F0}s elapsed");

          if (estimatedRemainingSeconds > 0)
          {
            if (estimatedRemainingSeconds < 60)
              Console.WriteLine($", est. {estimatedRemainingSeconds:F0}s remaining");
            else if (estimatedRemainingSeconds < 3600)
              Console.WriteLine($", est. {estimatedRemainingSeconds / 60:F1}m remaining");
            else
              Console.WriteLine($", est. {estimatedRemainingSeconds / 3600:F1}h remaining");
          }
          else
          {
            Console.WriteLine();
          }
        }
      }

      if (joltage.All(j => j == 0))
        return 0;

      string memoKey = $"{string.Join(",", joltage)}|{availableButtonsMask}";
      if (memo.TryGetValue(memoKey, out var cached))
        return cached;

      int bestIndex = -1;
      int bestValue = -1;
      int minAffectingButtons = int.MaxValue;

      for (int i = 0; i < joltage.Count; i++)
      {
        if (joltage[i] == 0)
          continue;

        int affectingButtonsCount = 0;
        for (int btn = 0; btn < buttons.Count; btn++)
        {
          if (IsButtonAvailable(btn, availableButtonsMask) && buttons[btn].Contains(i))
            affectingButtonsCount++;
        }

        if (affectingButtonsCount < minAffectingButtons ||
            (affectingButtonsCount == minAffectingButtons && joltage[i] > bestValue))
        {
          minAffectingButtons = affectingButtonsCount;
          bestIndex = i;
          bestValue = joltage[i];
        }
      }

      if (bestIndex == -1)
      {
        memo[memoKey] = int.MaxValue;
        return int.MaxValue;
      }

      var matchingButtons = new List<int>();
      for (int btn = 0; btn < buttons.Count; btn++)
      {
        if (IsButtonAvailable(btn, availableButtonsMask) && buttons[btn].Contains(bestIndex))
          matchingButtons.Add(btn);
      }

      int newMask = availableButtonsMask;
      foreach (var btn in matchingButtons)
        newMask &= ~(1 << btn);

      int result = int.MaxValue;

      if (matchingButtons.Count > 0)
      {
        int m = matchingButtons.Count;
        int n = bestValue;

        List<int> counts = new List<int>(new int[m]);
        counts[m - 1] = n;

        do
        {
          bool valid = true;
          List<int> newJoltage = new List<int>(joltage);

          for (int bi = 0; bi < m && valid; bi++)
          {
            int presses = counts[bi];
            if (presses == 0)
              continue;

            int buttonIdx = matchingButtons[bi];
            foreach (var pos in buttons[buttonIdx])
            {
              if (newJoltage[pos] >= presses)
                newJoltage[pos] -= presses;
              else
              {
                valid = false;
                break;
              }
            }
          }

          if (valid)
          {
            int subResult = DFSPartTwoWithProgress(newJoltage, newMask, buttons, memo);
            if (subResult != int.MaxValue)
            {
              int totalPresses = n + subResult;
              if (totalPresses < result)
                result = totalPresses;
            }
          }

        } while (NextCombination(counts));
      }

      memo[memoKey] = result;
      return result;
    }

    private bool NextCombination(List<int> combination)
    {
      int i = combination.Count - 1;
      while (i >= 0 && combination[i] == 0)
        i--;

      if (i <= 0)
        return false;

      int value = combination[i];
      combination[i - 1]++;
      combination[i] = 0;
      combination[combination.Count - 1] = value - 1;

      return true;
    }
  }
}