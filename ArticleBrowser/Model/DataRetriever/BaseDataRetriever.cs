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

		/// <summary>
		/// Add item to database
		/// </summary>
		/// <param name="item"></param>
		public void AddItem(Item item)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get all of the items from database
		/// </summary>
		/// <returns>IEnumerable of items, or null if empty</returns>
		IList<Item> IDataRetriever.GetItems()
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
	}
}
