using ArticleBrowserAddIn.Models.Data;

namespace ArticleBrowserAddIn
{
	public static class IoC
	{
		public static IDataRetriever Get<T>() where T : IDataRetriever, new()
		{
			return new T();
		}
	}
}