using eClaim.Entities;
using eClaim.Shared;
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
   public class EmployeeDAC:DataAccessComponent
    {
        public Employee SelectByUserNamePassword(string Email, string password)
        {
            const string SQL_STATEMENT =
                " SELECT EmployeeID, EmpName, BranchCode,IsApprover " +
                " FROM Employees " +
                " WHERE EmpEmail = @EmpEmail and EmpPass =@EmpPass";

            Employee employee = null;

            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {

                    db.AddInParameter(cmd, "@EmpEmail", DbType.AnsiString, Email);
                    db.AddInParameter(cmd, "@EmpPass", DbType.AnsiString, password);

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        if (dr.Read())
                        {
                            // Create a new Customer
                            employee = new Employee();

                            // Read values.
                            employee.EmployeeID = GetDataValue<int>(dr, "EmployeeID");
                            employee.EmpName = GetDataValue<string>(dr, "EmpName");
                            employee.BranchCode = GetDataValue<string>(dr, "BranchCode");
                            employee.IsApprover = GetDataValue<bool>(dr, "IsApprover");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { Email,password }, ex);
            }

            return employee;
        }



        public Employee SelectByEmployeeID(int employeeID)
        {
            const string SQL_STATEMENT =
                " SELECT EmployeeID, isApprover,BranchCode " +
                " FROM Employees " +
                " WHERE employeeID = @employeeID";

            Employee employee = null;

            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {

                    db.AddInParameter(cmd, "@employeeID", DbType.Int32, employeeID);
                
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        if (dr.Read())
                        {
                            // Create a new Customer
                            employee = new Employee();

                            // Read values.
                            employee.EmployeeID = GetDataValue<int>(dr, "EmployeeID");
                            employee.BranchCode = GetDataValue<string>(dr, "BranchCode");
                            employee.IsApprover = GetDataValue<bool>(dr, "IsApprover"); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { employeeID }, ex);
            }

            return employee;
        }
    }
}
