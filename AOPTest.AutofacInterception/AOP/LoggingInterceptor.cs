using Castle.DynamicProxy;
using System;
using System.IO;
using System.Linq;

namespace AOPTest.AutofacInterception.AOP
{
    public class LoggingInterceptor : IInterceptor
    {
        private TextWriter _output;

        public LoggingInterceptor(TextWriter output)
        {
            _output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var typeName = invocation.InvocationTarget.ToString();
            var args = $"[{string.Join("], [", invocation.Arguments.Select(x => (x ?? "").ToString()))}]";

            _output.WriteLine($"{DateTime.Now}: [{typeName}] Entering {methodName} with arguments: {args}");

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _output.WriteLine($"{DateTime.Now}: [{typeName}] Method {methodName} threw exception {ex.GetType().Name}: {ex.Message}");
                throw;
            }
            finally
            {
                _output.WriteLine($"{DateTime.Now}: [{typeName}] Exiting {methodName} with result: [{(invocation.ReturnValue ?? "NULL")}]");
            }
        }
    }
}
