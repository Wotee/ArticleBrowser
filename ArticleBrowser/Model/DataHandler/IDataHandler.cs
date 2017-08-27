using System;
using System.Collections.Generic;

namespace WordAddIn1.Model.DataHandler
{
	public interface IDataHandler
	{
		IList<Item> GetAllItems();

		void AddItem(Item item);
	}
}