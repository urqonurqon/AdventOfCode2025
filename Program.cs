using System;
using System.Diagnostics;
using System.Reflection;
using AdventOfCode;

namespace AdventOfCode2024 {
	public class Program {

		static string? _dayDataPath;
		static string? _methodName;

		static void Main(string[] args)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			string? dataPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));


			Console.WriteLine("Choose day or create a new one: \n");
			string? day = Console.ReadLine();


			if (!int.TryParse(day, out int dayInNumbers))
			{
				Console.WriteLine("Wrong input. Type in the day number");
			}
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
                Console.WriteLine("FreshIngredients: " + day.Solve());
            }
        }
	}
}