using eClaim.Entities;
using eClaim.Shared;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Data
{
  public  class CurrencyDAC:DataAccessComponent
    {

        public List<Currency> GetCurrencies()
        {
            string SQL_STATEMENT =
            "SELECT Code,Name from Currencies";

            List<Currency> result = new List<Currency>();

            // Connect to database.

            try
            {
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            // Create a new Contact
                            Currency currency = new Currency();

                            // Read values.

                            // Read values.
                            currency.Code = base.GetDataValue<string>(dr, "Code");
                            currency.Name = base.GetDataValue<string>(dr, "Name");
                   
                            // Add to List.
                            result.Add(currency);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { }, ex);
            }
        }
    }
}
