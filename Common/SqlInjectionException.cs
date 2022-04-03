using System;

namespace Common
{
    internal class SqlInjectionException : Exception
    {
        public SqlInjectionException(string message) : base(message) { }
    }
}
