using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Windows;

namespace WordAddIn1.Model.DataHandler
{
	/// <summary>
	/// Using 
	/// </summary>
	public class Interceptor : RealProxy
	{
		/// <summary>
		/// The decorated instance
		/// </summary>
		private readonly dynamic _decorated;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="interfaceType">Type of interface</param>
		/// <param name="decorated">Instance to be decorated</param>
		public Interceptor(Type interfaceType, object decorated) : base(interfaceType)
		{
			_decorated = decorated;
		}

		/// <summary>
		/// Wraps the decoration invocations in try-catch, showing MessageBox in error cases.
		/// </summary>
		/// <param name="msg">IMessage</param>
		/// <returns>IMessage</returns>
		public override IMessage Invoke(IMessage msg)
		{
			var methodCall = msg as IMethodCallMessage;
			if (methodCall == null)
			{
				Debugger.Break();
				throw new Exception("The impossible happened!");
			}
			var methodInfo = methodCall.MethodBase;
			dynamic result = null;
			try
			{
				result = methodInfo.Invoke(_decorated, methodCall.InArgs);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error connecting to database:{Environment.NewLine}{ex.InnerException?.Message ?? ex.Message}", "ERROR");
			}
			return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
		}
	}
}