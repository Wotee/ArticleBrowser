using System;
using System.Collections.Generic;

namespace WordAddIn1.Model.DataRetriever
{
	public interface IDataRetriever
	{
		#region Wrapper methods
		void RunQuery(Action action);

		T RunQuery<T>(Func<T> action);

		TResult RunQuery<T, TResult>(Func<T, TResult> action, T parameter);
		#endregion

		#region Actual Methods
		Func<IList<Item>> GetAllItems { get; }

		Action<Item> AddItem { get; }
		#endregion
	}
}