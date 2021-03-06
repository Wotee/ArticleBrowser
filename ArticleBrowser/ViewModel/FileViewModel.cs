﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WordAddIn1.Model;
using WordAddIn1.Model.DataHandler;

namespace WordAddIn1.ViewModel
{
	/// <summary>
	/// A view model for files
	/// </summary>
	public class FileViewModel : BaseViewModel
	{
		#region Private Members
		private string _authorSearch = "";
		private string _titleSearch = "";
		private GridViewColumnHeader _lastHeaderClicked;
		ListSortDirection _lastDirection = ListSortDirection.Ascending;
		private ObservableCollection<Item> _visualItems = new ObservableCollection<Item>();
		private List<Item> _items = new List<Item>();

		#endregion

		#region Public properties

		/// <summary>
		/// Source for TreeView showing filtered items
		/// </summary>
		public ObservableCollection<Item> VisualItems
		{
			get { return _visualItems; }
			set { SetProperty(ref _visualItems, value); }
		}

		/// <summary>
		/// In-memory representation of the database.
		/// Used to achieve fast filtering to the UI.
		/// </summary>
		internal List<Item> InMemoryItems
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		/// <summary>
		/// Author to filter items shown in <see cref="VisualItems"/>
		/// </summary>
		public string AuthorSearchBox
		{
			get
			{
				return _authorSearch;
			}
			set
			{
				SetProperty(ref _authorSearch, value);
				FilterItemsForView();
			}
		}

		/// <summary>
		/// Title to filter items shown in <see cref="VisualItems"/>
		/// </summary>
		public string TitleSearchBox
		{
			get
			{
				return _titleSearch;
			}
			set
			{
				SetProperty(ref _titleSearch, value);
				FilterItemsForView();
			}
		}
		#endregion

		#region Public Commands

		public ICommand OpenCommand { get; }
		public ICommand ColumnSortCommand { get; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public FileViewModel()
		{
			// Initialize commands
			OpenCommand = new RelayCommand(Open);
			ColumnSortCommand = new RelayCommand(ColumnSort);

			// Get stuff from DB to in-memory model
			DataHandler = IoC.Get<DataHandler>();
			InMemoryItems = DataHandler.GetAllItems() as List<Item>;

			// Show correct stuff in UI
			FilterItemsForView();
		}

		/// <summary>
		/// Open the item
		/// </summary>
		/// <param name="itemToBeOpened"></param>
		private void Open(object itemToBeOpened)
		{
			var item = itemToBeOpened as Item;
			if (item == null) return;

			MessageBox.Show(item.ToString());
			// I guess this should be now passed to DataHandler?
		}

		/// <summary>
		/// Sort logic for defining the column and sort direction to be used
		/// </summary>
		/// <param name="sender">Clicked ColumnHeader</param>
		private void ColumnSort(object sender)
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
			var dataView = CollectionViewSource.GetDefaultView(VisualItems);
			dataView.SortDescriptions.Clear();
			var sd = new SortDescription(sortBy, direction);
			dataView.SortDescriptions.Add(sd);
			dataView.Refresh();
		}

		/// <summary>
		/// FilterItemsForView the items showed in UI
		/// </summary>
		internal void FilterItemsForView()
		{
			if (VisualItems == null) return;

			var title = new Regex(TitleSearchBox);
			var author = new Regex(AuthorSearchBox);

			// TODO: Can the be faster?
			VisualItems = new ObservableCollection<Item>(InMemoryItems.Where(x => author.IsMatch(x.Author) && title.IsMatch(x.Title)).Select(x => x));
		}
	}
}