using System;

namespace eClaim.Shared
{
    public class DBException : ApplicationException
    {
        public string SqlStatement { get; set; }
        public object[] ParameterArray { get; set; }

        public DBException(string sqlStatement, object[] parameters, Exception innerException)
            : base(innerException.Message, innerException)
        {
            this.SqlStatement = sqlStatement;
            this.ParameterArray = parameters;
        }
    }
}
