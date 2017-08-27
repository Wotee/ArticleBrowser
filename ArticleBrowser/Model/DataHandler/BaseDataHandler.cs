using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Windows.Forms;
using Dapper;

namespace WordAddIn1.Model.DataHandler
{
	/// <summary>
	/// BaseDataHandler class providing similar parts for different DataHandlers
	/// </summary>
	public abstract class BaseDataHandler : IDataHandler
	{
		private const string GetterSql = "SELECT * FROM Item I LEFT JOIN ItemCategory IC ON I.ID = IC.ItemID LEFT JOIN Category C ON IC.CategoryID = C.ID";
		protected IDbConnection Connection;

		#region Actions
		protected Action CreateDataBase { get; }
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor. Initializes actions
		/// </summary>
		protected BaseDataHandler()
		{
			CreateDataBase = DatabaseCreation;
		}
		#endregion

		#region Private Methods
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
		//IList<Item> IDataHandler.GetItems()
		public IList<Item> GetAllItems()
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
		/// Creates the database
		/// </summary>
		private void DatabaseCreation()
		{
			var sql = Properties.Resources.SQL;
			Connection.Execute(sql);
		}
		#endregion
	}
}
