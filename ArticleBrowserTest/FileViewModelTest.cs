using NUnit.Framework;
using ArticleBrowserAddIn.Models.Factories;
using ArticleBrowserAddIn.Models.ViewModels;

namespace ArticleBrowserTest
{
	[TestFixture]
	public class FileViewModelTest
	{
		private ProdStatus status = ProdStatus.Test;

		// TODO: This whole idea of ProdStatus might be bad.. Reconsider
		[Test]
		public void ConstructorTest()
		{
			FileViewModel fvm = new FileViewModel();
			Assert.IsNotNull(fvm);
			Assert.IsNotNull(fvm.Items);
			Assert.AreEqual(1, fvm.Items.Count);
		}
	}
}
