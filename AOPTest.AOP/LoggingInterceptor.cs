using ArxOne.MrAdvice.Advice;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.AOP
{
    public class LoggingInterceptor : Attribute, IMethodAdvice
    {
        private TextWriter _output = Console.Out;

        public void Advise(MethodAdviceContext context)
        {
            var methodName = context.TargetMethod.Name;
            var typeName = context.Target.ToString();
            var args = $"[{string.Join("], [", context.Arguments.Select(x => (x ?? "").ToString()))}]";

            _output.WriteLine($"{DateTime.Now}: [{typeName}] Entering {methodName} with arguments: {args}");

            try
            {
                context.Proceed();
            }
            catch (Exception ex)
            {
                _output.WriteLine($"{DateTime.Now}: [{typeName}] Method {methodName} threw exception {ex.GetType().Name}: {ex.Message}");
                throw;
            }
            finally
            {
                _output.WriteLine($"{DateTime.Now}: [{typeName}] Exiting {methodName} with result: [{(context.ReturnValue ?? "NULL")}]");
            }
        }
    }
}
