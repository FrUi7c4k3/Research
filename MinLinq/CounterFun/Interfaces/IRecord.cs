using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinLinq.Interfaces
{
	public interface IRecord<out T1, out T2>
	{
		T1 First { get; }
		T2 Second { get; }
	}
}
