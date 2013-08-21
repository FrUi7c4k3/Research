using System.Linq;
using Extensibilty;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Research.Reflection.ExtesibilityExample.ExtensiCalc
{
	class Program 
	{
		static void Main(string[] args)
		{
			Console.WriteLine("ExtensiCalc - The world's best calculator");
			Console.WriteLine();
			var operations = GetOperations().ToList();

			while (true)
			{
				int i = 1;

				foreach (var op in operations)
				{
					Console.WriteLine(i++ + ". " + op.GetType().Name);
				}
				Console.WriteLine();

				int selectedOperation = -1;

				while (selectedOperation < 0 || selectedOperation > operations.Count)
				{
					selectedOperation = ReadInt("Enter selection: ");
				}
				Console.WriteLine();

				if (selectedOperation ==0)
					break;

				ICalculation operation = operations[selectedOperation - 1];
				int a = ReadInt("a: ");
				int b = ReadInt("b: ");
				Console.WriteLine(String.Format("Result = {0}", operation.Calculate(a,b)));

				Console.WriteLine();
				Console.WriteLine();
			}
		}

		private static int ReadInt(string prompt)
		{
			int result = 0;
			bool valid = false;
			while (!valid)
			{
				Console.Write(prompt);
				valid = int.TryParse(Console.ReadLine(), out result);
			}
			return result;
		}

		private static IEnumerable<ICalculation> GetOperations()
		{
			return GetBuiltInOperations().Concat(GetExtensions());
		}

		private static IEnumerable<ICalculation> GetBuiltInOperations()
		{
			yield return new Addition();
			yield return new Subtraction();
			yield return new Multiplication();
			yield return new Division();
		}

		private static IEnumerable<ICalculation> GetExtensions()
		{
			if (Directory.Exists("Extensions"))
			{
				Type icalc = typeof (ICalculation);

				foreach (string file in Directory.EnumerateFiles("Extensions", "*.dll"))
				{
					Assembly asm = Assembly.LoadFrom(file);

					var extTypes = from t in asm.GetTypes()
					               where t.GetInterfaces().Contains(icalc)
					               select t;

					foreach (var ext in extTypes)
						yield return (ICalculation) Activator.CreateInstance(ext);
				}
			}
		}
	}
}
