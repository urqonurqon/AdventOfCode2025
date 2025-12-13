namespace AdventOfCode
{
  public class Day11
  {
    private Dictionary<string, List<string>> _wires = new();
    private List<string> haveToVisit = new() { "dac", "fft" };
    private long _numOfPaths;

    // Memoization dictionary: key = (node + visitedState)
    private Dictionary<string, long> _memo = new();

    public Day11(string path)
    {
      var lines = File.ReadAllLines(path);
      for (int i = 0; i < lines.Length; i++)
      {
        var splitLines = lines[i].Split(" ");
        var key = splitLines[0].Replace(":", "");
        _wires.Add(key, new());
        for (int j = 1; j < splitLines.Length; j++)
        {
          _wires[key].Add(splitLines[j]);
        }
      }
    }

    public long Solve()
    {
      _numOfPaths = 0;
      _memo.Clear();

      Stack<string> stack = new();
      DepthFirstSearch("svr", stack, 0);

      return _numOfPaths;
    }

    private void DepthFirstSearch(string startNode, Stack<string> stack, int visitedState)
    {
      // visitedState: 0=none, 1=dac, 2=fft, 3=both (using bitmask)

      // Create memo key
      string memoKey = $"{startNode}|{visitedState}";

      if (_memo.ContainsKey(memoKey))
      {
        // We've already computed paths from this state, add them
        _numOfPaths += _memo[memoKey];
        return;
      }

      // Remember how many paths we had before exploring this node
      long pathsBefore = _numOfPaths;

      if (!_wires.TryGetValue(startNode, out var values))
      {
        _memo[memoKey] = 0;
        return;
      }

      for (int i = 0; i < values.Count; i++)
      {
        if (values[i] == "out")
        {
          // Check if we visited both required nodes
          if (visitedState == 3) // Binary 11 = both bits set
          {
            _numOfPaths++;
          }
        }
        else
        {
          // Update visited state if this is a required node
          int newVisitedState = visitedState;
          if (values[i] == "dac")
          {
            newVisitedState |= 1; // Set bit 0
          }
          else if (values[i] == "fft")
          {
            newVisitedState |= 2; // Set bit 1
          }

          stack.Push(values[i]);
          DepthFirstSearch(values[i], stack, newVisitedState);
          stack.Pop();
        }
      }

      // Store how many paths were added from this state
      _memo[memoKey] = _numOfPaths - pathsBefore;
    }
  }
}