using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.AOP
{
    public class LoggingInterceptor : IInterceptor
    {
        private static string _logFile = "log.txt";
        private static TextWriter _output;

        private static TextWriter Output
        {
            get
            {
                // reset the log file on each run
                if (_output == null)
                {
                    File.Delete(_logFile);
                    _output = File.CreateText(_logFile);
                }
                return _output;
            }
        }
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var typeName = invocation.TargetType.ToString();
            var args = $"[{string.Join("], [", invocation.Arguments.Select(GetObjectType))}]";

            Output.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: [{typeName}] Entering {methodName} with arguments: {args}");

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Output.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: [{typeName}] Method {methodName} threw exception {ex.GetType().Name}: {ex.Message}");
                throw;
            }
            finally
            {
                var returnValue = invocation.ReturnValue;
                Output.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: [{typeName}] Exiting {methodName} with result: [{GetObjectType(returnValue)}]");
            }

            Output.Flush();
        }

        private string GetObjectType(object value)
        {
            if (value == null)
            {
                return "NULL";
            }

            var type = value.GetType();

            var enumerableType = GetEnumerableType(type);

            if (enumerableType != null)
            {
                return $"Enumerable of {enumerableType.FullName}";
            }
            else
            {
                return $"{type.FullName}:{value.ToString()}";
            }
        }

        private Type GetEnumerableType(Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;

        }
    }
}
