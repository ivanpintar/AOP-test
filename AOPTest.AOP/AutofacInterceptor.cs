using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.AOP
{
    public class AutofacInterceptor : IInterceptor
    {
        private TextWriter _output = Console.Out;

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
