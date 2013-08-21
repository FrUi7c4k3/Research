using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
	public class QueryableTerraServerData<TDate> : IOrderedQueryable<TDate>
	{
		public QueryableTerraServerData()
		{
			Provider = new TerraServerQueryProvider();
		}

		#region IQueriable Members

		public Type ElementType
		{
			get { throw new NotImplementedException(); }
		}

		public Expression Expression { get; private set; }

		public IQueryProvider Provider { get; private set; }
		
		public IEnumerator<TDate> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion //IQueriable Members
	}
}
