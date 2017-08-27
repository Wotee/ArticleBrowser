using System;
using WordAddIn1.Model.DataHandler;

namespace WordAddIn1
{
	public static class IoC
	{
		//public static IDataHandler Get<T>() where T : IDataHandler, new()
		//{
		//	return new T();
		//}

		public static IDataHandler Get<T>() where T : IDataHandler, new()
		{
			return new Interceptor(typeof(IDataHandler), new T()).GetTransparentProxy() as IDataHandler;
		}
	}
}