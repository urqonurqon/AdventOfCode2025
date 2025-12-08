using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
  public class Day7
  {
    private List<List<char>> _takyonGraph;
    private List<List<char>> _takyonGraph2;

    private int _sum;
    public long PartTwoSum;

    private readonly Dictionary<string, long> _memo = new();

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

      long c1 = SolvePartTwo(_takyonGraph, true);
      long c2 = SolvePartTwo(_takyonGraph2, false);

      PartTwoSum = (c1 + c2) / 2;
    }

    private static string MakeKey(List<List<char>> g, bool left)
    {
      var sb = new StringBuilder();
      sb.Append(left ? "L|" : "R|");

      for (int i = 0; i < g.Count; i++)
      {
        sb.Append(g[i].ToArray());
        sb.Append('\n');
      }
      return sb.ToString();
    }

    public int Solve()
    {
      _sum = 0;

      for (int i = 1; i < _takyonGraph.Count; i++)
      {
        for (int j = 0; j < _takyonGraph[i].Count; j++)
        {
          if (_takyonGraph[i - 1][j] == 'S' ||
              (_takyonGraph[i - 1][j] == '|' && _takyonGraph[i][j] != '^'))
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


    public long SolvePartTwo(List<List<char>> takyonGraph, bool left)
    {
      string key = MakeKey(takyonGraph, left);
      if (_memo.TryGetValue(key, out long cached))
      {
        return cached;
      }

      if (takyonGraph.Count <= 2)
      {
        _memo[key] = 1;
        return 1;
      }

      for (int i = 1; i < takyonGraph.Count; i++)
      {
        for (int j = 0; j < takyonGraph[i].Count; j++)
        {
          if (takyonGraph[i - 1][j] == 'S' ||
              (takyonGraph[i - 1][j] == '|' && takyonGraph[i][j] != '^'))
          {
            takyonGraph[i][j] = '|';
          }

          if (j < takyonGraph[i].Count - 1 &&
              takyonGraph[i][j + 1] == '^' &&
              takyonGraph[i - 1][j + 1] == '|')
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

            long leftCount = SolvePartTwo(newTakyonL, true);
            long rightCount = SolvePartTwo(newTakyonR, false);
            long total = leftCount + rightCount;

            // Memoize and return
            _memo[key] = total;
            return total;
          }
        }
      }

      _memo[key] = 1;
      return 1;
    }
  }
}
