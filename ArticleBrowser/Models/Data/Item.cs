using System.Collections.Generic;

namespace ArticleBrowserAddIn.Models.Data
{
	public class Item
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public int Year { get; set; }
		public string Filepath { get; set; }
		public List<Category> Categories { get; set; }
	}
}