using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppTest.Shared
{
	[Serializable]
	public class Pagination<TResult>
	{
		public List<TResult> Items { get; set; }

		public int TotalItems { get; set; }

		public Pagination()
		{
			Items = new List<TResult>();
		}
	}
}
