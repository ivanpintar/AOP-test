using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.AOP
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
            var args = $"[{string.Join("], [", invocation.Arguments.Select(x => (x ?? "").ToString()))}]";

            _output.WriteLine($"{DateTime.Now}: Entering {methodName} with arguments: {args}");

            try
            {
                invocation.Proceed();
            } catch (Exception ex)
            {
                _output.WriteLine($"{DateTime.Now}: Method {methodName} threw exception {ex.GetType().Name}: {ex.Message}");
                throw;
            }
            finally
            {
                _output.WriteLine($"{DateTime.Now}: Exiting {methodName} with result: [{invocation.ReturnValue}]");
            }
        }
    }
}
