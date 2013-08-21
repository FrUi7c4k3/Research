using System;
using Extensibilty;

namespace Geometry
{
	public class Pythagoras : ICalculation
	{
		public int Calculate(int a, int b)
		{
			return (int) Math.Sqrt(a * a + b * b);
		}
	}
}
