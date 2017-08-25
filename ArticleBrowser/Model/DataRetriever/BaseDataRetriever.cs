using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace WordAddIn1.Model.DataRetriever
{
	public abstract class BaseDataRetriever : IDataRetriever
	{
		private const string GetterSql = "SELECT * FROM Item I LEFT JOIN ItemCategory IC ON I.ID = IC.ItemID LEFT JOIN Category C ON IC.CategoryID = C.ID";
		protected IDbConnection Connection;

		#region Public Properties
		public Action<Item> AddItem { get; }

		public Func<IList<Item>> GetAllItems { get; }
		#endregion


		protected BaseDataRetriever()
		{
			AddItem = AddSingleItem;
			GetAllItems = GetItems;
		}

		/// <summary>
		/// Add item to database
		/// </summary>
		/// <param name="item"></param>
		private void AddSingleItem(Item item)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get all of the items from database
		/// </summary>
		/// <returns>IEnumerable of items, or null if empty</returns>
		//IList<Item> IDataRetriever.GetItems()
		private IList<Item> GetItems()
		{
			var lookup = new Dictionary<string, Item>();
			Connection.Query<Item, Category, Item>(GetterSql, (i, c) =>
			{
				Item item;
				if (!lookup.TryGetValue(i.Title, out item))
					lookup.Add(i.Title, item = i);
				item.Categories.Add(c);
				return item;
			});
			return lookup.Values.ToList();
		}


		/// <summary>
		/// Wrapper for error handling
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <returns></returns>
		public T RunQuery<T>(Func<T> query)
		{
			try
			{
				return query();

			}
			catch (Exception ex)
			{
#if RELEASE
					MessageBox.Show($"Could not connect to DB:{Environment.NewLine}{ex.Message}", "ERROR");
#endif
				return default(T);
			}
		}

		/// <summary>
		/// Wrapper for error handling
		/// </summary>
		/// <typeparam name="TResult">Type of parameter</typeparam>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="query">Query method</param>
		/// <param name="parameter">Parameter to query</param>
		/// <returns></returns>
		public TResult RunQuery<T, TResult>(Func<T, TResult> query, T parameter)
		{
			try
			{
				return query(parameter);
			}
			catch (Exception ex)
			{
#if RELEASE
					MessageBox.Show($"Could not connect to DB:{Environment.NewLine}{ex.Message}", "ERROR");
#endif
				return default(TResult);
			}
		}

		public List<Item> RunQuery<T>()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper for error handling
		/// </summary>
		/// <param name="query"></param>
		public void RunQuery(Action query)
		{
			try
			{
				query();
			}
			catch (Exception ex)
			{
#if RELEASE
					MessageBox.Show($"Could not connect to DB:{Environment.NewLine}{ex.Message}", "ERROR");
#endif
			}
		}
	}
}
