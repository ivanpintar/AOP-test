using ArxOne.MrAdvice.Advice;
using System;
using System.IO;
using System.Linq;

namespace AOPTest.AOP
{
    public class LoggingInterceptor : Attribute, IMethodAdvice
    {
        private TextWriter _output = Console.Out;

        public void Advise(MethodAdviceContext context)
        {
            var methodName = context.TargetMethod.Name;
            var typeName = context.TargetType.ToString();
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
                var returnValue = context.HasReturnValue ? context.ReturnValue : null;
                _output.WriteLine($"{DateTime.Now}: [{typeName}] Exiting {methodName} with result: [{returnValue ?? "NULL"}]");
            }
        }
    }
}
