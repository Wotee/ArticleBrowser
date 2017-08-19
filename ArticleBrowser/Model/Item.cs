using System.Collections.Generic;

namespace WordAddIn1.Model
{
	public class Item
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public int Year { get; set; }
		public string Filepath { get; set; }
		public List<Category> Categories { get; set; }

		/// <summary>
		/// Constructor for initializing the list automatically.
		/// </summary>
		public Item()
		{
			Categories = new List<Category>();
		}

		// TODO: Can perhaps be removed later, or at least make some use of this? This shows fine data on debugger though
		public override string ToString()
		{
			return $"{Title}, {Author}, {Year}";
		}
	}
}