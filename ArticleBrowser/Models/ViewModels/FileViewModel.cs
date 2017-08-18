using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ArticleBrowserAddIn.Models.Data;
using ArticleBrowserAddIn.Models.ViewModels.Base;
using Microsoft.Office.Tools.Word;

namespace ArticleBrowserAddIn.Models.ViewModels
{
	/// <summary>
	/// A view model for files
	/// </summary>
	public class FileViewModel : BaseViewModel
	{
		#region Private Members
		private string _authorSearch = "";
		private string _titleSearch = "";
		private GridViewColumnHeader _lastHeaderClicked = null;
		ListSortDirection _lastDirection = ListSortDirection.Ascending;
		#endregion

		#region Public properties
		/// <summary>
		/// Source for TreeView showing filtered items
		/// </summary>
		public ObservableCollection<Item> Items { get; set; }

		/// <summary>
		/// In-memory representation of the database.
		/// Used to achieve fast filtering to the UI.
		/// </summary>
		internal List<Item> InMemoryItems { get; set; }

		/// <summary>
		/// Author to filter items shown in <see cref="Items"/>
		/// </summary>
		public string AuthorSearchBox
		{
			get
			{
				return _authorSearch;
			}
			set
			{
				_authorSearch = value;
				Filter();
			}
		}

		/// <summary>
		/// Title to filter items shown in <see cref="Items"/>
		/// </summary>
		public string TitleSearchBox
		{
			get
			{
				return _titleSearch;
			}
			set
			{
				_titleSearch = value;
				Filter();
			}
		}
		#endregion

		#region Public Commands

		public ICommand OpenCommand { get; }
		public ICommand HeaderSortClickCommand { get; }

		#endregion

		public FileViewModel()
		{
			// Initialize commands
			OpenCommand = new RelayCommand(Open);
			HeaderSortClickCommand = new RelayCommand(HeaderClickSort);

			// Get stuff from DB
			var retriever = IoC.Get<DataRetriever>();
			InMemoryItems = retriever.GetItems() as List<Item>;
			// Show correct stuff in UI
			Filter();
		}

		private void Open()
		{
			throw new NotImplementedException("Toimiiks tää ees");
		}

		/// <summary>
		/// Sort logic for defining the column and sort direction to be used
		/// </summary>
		/// <param name="sender">Clicked ColumnHeader</param>
		private void HeaderClickSort(object sender)
		{
			var headerClicked = sender as GridViewColumnHeader;

			// Check for invalid options and return if found
			if (headerClicked == null) return;
			if (headerClicked.Role == GridViewColumnHeaderRole.Padding) return;

			ListSortDirection direction;
			if (!headerClicked.Equals(_lastHeaderClicked))
			{
				direction = ListSortDirection.Ascending;
			}
			else
			{
				direction = _lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
			}

			var header = headerClicked.Column.Header as string;
			Sort(header, direction);

			// Visualization change
			//(direction == ListSortDirection.Ascending)
			//{
			//	headerClicked.Column.HeaderTemplate =
			//		Resources["HeaderTemplateArrowUp"] as DataTemplate;
			//}
			//			else
			//			{
			//	headerClicked.Column.HeaderTemplate =
			//		Resources["HeaderTemplateArrowDown"] as DataTemplate;
			//}

			// Remove arrow from previously sorted header  
			if (_lastHeaderClicked != null && !_lastHeaderClicked.Equals(headerClicked))
			{
				_lastHeaderClicked.Column.HeaderTemplate = null;
			}

			_lastHeaderClicked = headerClicked;
			_lastDirection = direction;
		}

		/// <summary>
		/// The actual sort method
		/// </summary>
		/// <param name="sortBy"></param>
		/// <param name="direction"></param>
		private void Sort(string sortBy, ListSortDirection direction)
		{
			var dataView = CollectionViewSource.GetDefaultView(Items);
			dataView.SortDescriptions.Clear();
			var sd = new SortDescription(sortBy, direction);
			dataView.SortDescriptions.Add(sd);
			dataView.Refresh();
		}

		/// <summary>
		/// Filter the items showed in UI
		/// </summary>
		internal void Filter()
		{
			var title = new Regex(TitleSearchBox);
			var author = new Regex(AuthorSearchBox);

			// TODO: This can be made faster, if the LINQ is ripped out of there
			Items = new ObservableCollection<Item>(InMemoryItems.Where(x => author.IsMatch(x.Author) && title.IsMatch(x.Title)).Select(x => x));
		}


	}
}