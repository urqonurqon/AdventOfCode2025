

namespace AdventOfCode
{
  public class Day12
  {

    private Dictionary<int, int> _gifts = new Dictionary<int, int>();
    private List<(string, List<int>)> _areas = new();

    private int _canFit;
    public Day12(string path)
    {
      var lines = File.ReadAllLines(path);
      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].Length == 2)
        {
          var number = lines[i].Replace(":", "");
          string gift = lines[i + 1] + lines[i + 2] + lines[i + 3];
          int giftArea = 0;
          for (int j = 0; j < gift.Length; j++)
          {
            if (gift[j] == '#') giftArea++;
          }
          _gifts.Add(int.Parse(number), giftArea);


        }
        if (lines[i].Length > 4)
        {

          var splitLine = lines[i].Split(": ");
          _areas.Add((splitLine[0], new()));
          var afterColon = splitLine[1].Split(" ");
          for (int j = 0; j < afterColon.Length; j++)
          {
            _areas[i].Item2.Add(int.Parse(afterColon[j]));
          }

        }

      }
    }


    public int Solve()
    {
      _canFit = 0;
      for (int i = 0; i < _areas.Count; i++)
      {
        var areaStringSplit = _areas[i].Item1.Split("x");
        long area = 1;
        for (int j = 0; j < areaStringSplit.Length; j++)
        {
          area *= long.Parse(areaStringSplit[j]);
        }

        long areaOfRequiredGifts = 0;
        for (int j = 0; j < _areas[i].Item2.Count; j++)
        {
          var boxTypeAmount = _areas[i].Item2[j];
          areaOfRequiredGifts += boxTypeAmount * _gifts[j];
        }

        if (area >= areaOfRequiredGifts) _canFit++;
      }
      return _canFit;
    }
  }
}