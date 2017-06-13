using ArticleBrowserAddIn.Models.Data;
using ArticleBrowserAddIn.Models.Mocks;

namespace ArticleBrowserAddIn.Models.Factories
{
	public static class DataRetrieverFactory
	{
		public static IDataRetriever GetNewDataRetriever(ProdStatus status = ProdStatus.Production)
		{
			switch (status)
			{
				case ProdStatus.Production:
					return new DataRetriever();
				case ProdStatus.Test:
					return new MockDataRetriever();
				default:
					return null;
			}
		}
	}
}