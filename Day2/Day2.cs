

namespace AdventOfCode
{
    public class Day2
    {

        private List<(long, long)> _rangePairs;

        private long _sum;

        public Day2(string path)
        {
            var ranges = File.ReadAllText(path).Split(',');
            //ranges = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\r\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\r\n824824821-824824827,2121212118-2121212124".Split(',');
            _rangePairs = new List<(long, long)>();
            for (int i = 0; i < ranges.Length; i++)
            {
                var pair = ranges[i];
                var splitPair = pair.Split('-');
                _rangePairs.Add((long.Parse(splitPair[0]), long.Parse(splitPair[1])));
            }



        }

        public long Solve()
        {
            for (int i = 0; i < _rangePairs.Count; i++)
            {
                long firstID = _rangePairs[i].Item1;
                long lastID = _rangePairs[i].Item2;

                for (long j = firstID; j <= lastID; j++)
                {

                    var stringID = j.ToString();
                    var numberOfDigits = stringID.Length;

                    for (int k = 1; k < numberOfDigits; k++)
                    {
                        bool breakFlag = false;
                        if (numberOfDigits % k == 0)
                        {
                            string lastStringPart = "";
                            for (int l = 0; l < numberOfDigits; l += k)
                            {
                                var stringPart = stringID.Substring(l, k);
                                if (lastStringPart != "")
                                {
                                    if (lastStringPart != stringPart)
                                    {
                                        breakFlag = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    lastStringPart = stringPart;
                                }
                            }
                            if (!breakFlag)
                            {
                                _sum += j;

                                break;
                            }

                        }
                    }
                }
            }
            return _sum;
        }

    }
}