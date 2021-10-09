using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eClaim.Data
{
    public abstract class DataAccessComponent
    {
        protected const string CONNECTION_NAME = "default";



        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[CONNECTION_NAME].ConnectionString;
            }
        }

        protected int PageSize
        {
            get
            {
                // TODO: Define PageSize in config file.
                return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            }
        }

        protected T GetDataValue<T>(IDataReader dr, string columnName)
        {
            int i = dr.GetOrdinal(columnName);

            if (!dr.IsDBNull(i))
                return (T)dr.GetValue(i);
            else
                return default(T);
        }

        protected string FormatFilterStatement(string filter)
        {
            return Regex.Replace(filter, "^(AND|OR)", string.Empty);
        }
    }
}
