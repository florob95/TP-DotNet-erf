using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace MongoDB
{
    public class LogProxy<T>: RealProxy
    {
        private T _decorated;

        public LogProxy(T decorated): base(typeof(T))
        {
            _decorated = decorated;
        }


        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
            Console.WriteLine(methodCall.MethodName);
            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
        }
    }
}
