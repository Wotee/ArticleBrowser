using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordAddIn1.Model;
using WordAddIn1.Model.DataHandler;

namespace WordAddIn1.ViewModel
{
	public class AddItemViewModel : BaseViewModel
	{
		public string Author { get; set; } = "";
		public string Title { get; set; } = "";
		public int Year { get; set; } = 0;
		public List<Category> Categories { get; set; } = new List<Category>();

		private readonly string _fileLocation = "";

		public RelayCommand AddCommand;

		public AddItemViewModel(IDataHandler handler, string location)
		{
			_fileLocation = location;
			DataHandler = handler;
			AddCommand = new RelayCommand(AddToDatabase);
		}

		private void AddToDatabase()
		{
			var item = new Item()
			{
				Author = Author,
				Title = Title,
				Year = Year,
				Categories = Categories,
				Content = File.ReadAllBytes(_fileLocation)
			};
			DataHandler.AddItem(item);
		}
	}
}
