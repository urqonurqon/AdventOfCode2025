using System;
using System.Diagnostics;
using System.Reflection;
using AdventOfCode;

namespace AdventOfCode2024
{
  public class Program
  {

    static string? _dayDataPath;
    static string? _methodName;

    static void Main(string[] args)
    {
      string currentDirectory = Directory.GetCurrentDirectory();
      string? dataPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));

      int dayInNumbers;
      string? day;
      bool correctInput = false;
      do
      {

        Console.WriteLine("Choose day or create a new one: \n");
        day = Console.ReadLine();

        correctInput = int.TryParse(day, out dayInNumbers) && dayInNumbers > 0;

        if (!correctInput)
        {
          Console.WriteLine("Wrong input. Type in the day number");
        }

      } while (!correctInput);

      string methodName = "Day" + dayInNumbers;

      var methodToInvoke = typeof(Program).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

      if (methodToInvoke == null)
      {
        if (!Directory.Exists(methodName))
        {
          Directory.CreateDirectory(dataPath + methodName);
          var fileStream = File.Create(dataPath + methodName + "\\" + methodName + ".cs");
          fileStream.Close();
          fileStream = File.Create(dataPath + methodName + "\\" + "Input" + methodName + ".txt");
          fileStream.Close();
          File.WriteAllText(dataPath + methodName + "\\" + methodName + ".cs", "\r\n\r\nnamespace AdventOfCode " +
              "{\r\n\tpublic class " + methodName + " {\r\n\r\n\r\n\r\n\t\tpublic " + methodName + "()\r\n\t\t" +
              "{\r\n\t\r\n\t\t}\r\n\r\n\t\r\n\r\n\t}" +
              "\r\n}");

          var programCode = File.ReadAllText(dataPath + "Program.cs");
          programCode = programCode.Remove(programCode.LastIndexOf("}"));
          programCode = programCode.Remove(programCode.LastIndexOf("}"));

          programCode += "\n\t\tstatic void " + methodName + "()\r\n\t\t{\r\n\r\n\t\t}\r\n\t}\r\n}";

          File.WriteAllText(dataPath + "Program.cs", programCode);
        }
      }
      else
      {
        _dayDataPath = dataPath + methodName + "\\";
        _methodName = methodName;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        methodToInvoke.Invoke(null, null);
        sw.Stop();
        Console.WriteLine("Runtime: " + sw.Elapsed.TotalSeconds + "s.\n");
      }

      Console.WriteLine("Press any key..");
      Console.ReadKey();
    }

    static void Day1()
    {
      Day1 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day1(filePath);

        Console.WriteLine("Password is: " + day.Solver());
      }
    }


    static void Day2()
    {
      Day2 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day2(filePath);
        Console.WriteLine("Sum of invalid ID's: " + day.Solve());
      }
    }

    static void Day3()
    {
      Day3 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day3(filePath);
        Console.WriteLine("Sum of batteries is: " + day.Solve(12));
      }
    }

    static void Day4()
    {
      Day4 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day4(filePath);
        Console.WriteLine("Movable papiruses: " + day.Sum);
      }
    }

    static void Day5()
    {
      Day5 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day5(filePath);
        Console.WriteLine("Fresh Ingredients: " + day.Solve());
        Console.WriteLine("All unique Ingredients: " + day.SolvePartTwo());
      }
    }


    static void Day6()
    {
      Day6 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day6(filePath);
        //Console.WriteLine("Sum of columns: " + day.Solve());
        Console.WriteLine("Sum of columns but in columns: " + day.SolvePartTwo());
      }
    }

    static void Day7()
    {
      Day7 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day7(filePath);
        //Console.WriteLine("Number of Splits: " + day.Solve());
        Console.WriteLine("Number of Splits in Timelines: " + day.PartTwoSum);
      }
    }


    static void Day8()
    {
      Day8 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day8(filePath);
        Console.WriteLine("Distance Multiplication: " + day.Solve(1000, 3));
        //Console.WriteLine("Number of Splits in Timelines: " + day.PartTwoSum);
      }
    }


    static void Day9()
    {
      Day9 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day9(filePath);
        Console.WriteLine("Biggest Rect: " + day.Solve());
      }
    }

  
		static void Day10()
		{
      Day10 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day10(filePath);
        //Console.WriteLine("Smallest number of presses: " + day.Solve());
        Console.WriteLine("Smallest number of presses P2: " + day.SolvePartTwo());
      }
    }
	
		static void Day11()
		{
      Day11 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day11(filePath);
        Console.WriteLine("Smallest number of presses: " + day.Solve());
        //Console.WriteLine("Smallest number of presses P2: " + day.SolvePartTwo());
      }
    }
	
		static void Day12()
		{
      Day11 day;

      string? filePath = _dayDataPath + "Input" + _methodName + ".txt";

      if (filePath != null)
      {
        day = new Day11(filePath);
        Console.WriteLine("Smallest number of presses: " + day.Solve());
        //Console.WriteLine("Smallest number of presses P2: " + day.SolvePartTwo());
      }
    }
	}
}