using Extensibilty;

namespace Research.Reflection.ExtesibilityExample.ExtensiCalc
{
	class Addition : ICalculation
	{
		public int Calculate(int a, int b)
		{
			return a + b;
		}
	}

	class Subtraction : ICalculation
	{
		public int Calculate(int a, int b)
		{
			return a - b;
		}
	}

	class Multiplication : ICalculation
	{
		public int Calculate(int a, int b)
		{
			return a * b;
		}
	}

	class Division : ICalculation
	{
		public int Calculate(int a, int b)
		{
			return a / b;
		}
	}
}
