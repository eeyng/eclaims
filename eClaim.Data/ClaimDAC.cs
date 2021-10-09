using eClaim.Entities;
using eClaim.Shared;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Data
{
    public class ClaimDAC:DataAccessComponent
    {
        public Claim Create(Claim claim)
        {
            const string SQL_STATEMENT =
                    "INSERT INTO dbo.Claims (EmployeeID,DateCreated,Status,BankCode,BankAccNo,BankAccName " +
                    ",ApproverID,TotalAmount) " +
                    "VALUES(@EmployeeID,@DateCreated,@Status,@BankCode,@BankAccNo,@BankAccName " +
                    ",@ApproverID,@TotalAmount) SELECT SCOPE_IDENTITY(); ";

            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@EmployeeID", DbType.Int32, claim.EmployeeID);
                    db.AddInParameter(cmd, "@DateCreated", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@Status", DbType.Byte, (byte)claim.Status);
                    db.AddInParameter(cmd, "@BankCode", DbType.AnsiString, claim.BankCode);
                    db.AddInParameter(cmd, "@BankAccNo", DbType.AnsiString, claim.BankAccNo);
                    db.AddInParameter(cmd, "@BankAccName", DbType.AnsiString, claim.BankAccName);
                    db.AddInParameter(cmd, "@ApproverID", DbType.AnsiString, claim.ApproverID);
                    db.AddInParameter(cmd, "@TotalAmount", DbType.AnsiString, claim.TotalAmount);

               claim.ClaimID = Convert.ToInt32(db.ExecuteScalar(cmd));
                }

                return claim;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] {claim}, ex);
            }
        }

        public void ApproveRejectClaim(Claim claim)
        {
            string sql =
                   "Update dbo.Claims set Status=@Status,  " +
                   "approverID = @approverID, " +
                   "DateApproved = @DateApproved " +
                    " where claimid = @claimid ";


            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(sql))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@Status", DbType.Byte, claim.Status);
                    db.AddInParameter(cmd, "@approverID", DbType.AnsiString, claim.ApproverID);
                    db.AddInParameter(cmd, "@DateApproved", DbType.AnsiString, DateTime.Now);
                    db.AddInParameter(cmd, "@claimid", DbType.Int32, claim.ClaimID);

                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new DBException(sql, new object[] { claim }, ex);
            }
        }

        public void Update(Claim claim)
        {
            string sql =
                   "Update dbo.Claims set Status=@Status,  " +
                   "BankCode = @BankCode, " +
                   "BankAccNo = @BankAccNo, " +
                   "BankAccName = @BankAccName, " +
                   "TotalAmount = @TotalAmount ";

                    if(claim.Status == Status.Submitted)
            {
                sql = sql + ",DateSubmitted = @DateSubmitted ";

            }
            sql= sql+ " where claimid = @claimid ";


            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(sql))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@Status", DbType.Byte, claim.Status);
                    db.AddInParameter(cmd, "@BankCode", DbType.AnsiString, claim.BankCode);
                    db.AddInParameter(cmd, "@BankAccNo", DbType.AnsiString, claim.BankAccNo);
                    db.AddInParameter(cmd, "@BankAccName", DbType.AnsiString, claim.BankAccName);
                    db.AddInParameter(cmd, "@TotalAmount", DbType.AnsiString, claim.TotalAmount);
                    if (claim.Status == Status.Submitted)
                    {
                        db.AddInParameter(cmd, "@DateSubmitted", DbType.DateTime, claim.DateSubmitted);
                    }
                    db.AddInParameter(cmd, "@claimid", DbType.Int32, claim.ClaimID);

                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new DBException(sql, new object[] { claim }, ex);
            }
        }



        public Claim GetClaim(int claimID)
        {
            string SQL_STATEMENT =
               "SELECT  claimID, EmployeeID, datecreated, status, BankCode,BankAccNo, BankAccName,totalamount,DateSubmitted " +
                                  "FROM Claims " +
               " WHERE claimID = @claimID ";



            Claim claim = new Claim();
            ClaimDetailDAC claimdetialDAC = new ClaimDetailDAC();
            // Connect to database.

            try
            {
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {

                    db.AddInParameter(cmd, "@claimID", DbType.Int32, claimID);

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        if (dr.Read())
                        {
                            // Create a new Contact
                             claim = new Claim();

                            // Read values.

                            // Read values.
                            claim.ClaimID = Convert.ToInt32(dr["ClaimID"]);
                            claim.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                            claim.DateCreated = base.GetDataValue<DateTime>(dr, "DateCreated");
                            claim.DateSubmitted = base.GetDataValue<DateTime>(dr, "DateSubmitted");
                            claim.Status = (Status)base.GetDataValue<byte>(dr, "Status");
                            claim.BankCode = base.GetDataValue<string>(dr, "BankCode");
                            claim.BankAccNo = base.GetDataValue<string>(dr, "BankAccNo");
                            claim.BankAccName = base.GetDataValue<string>(dr, "BankAccName");
                            claim.TotalAmount= base.GetDataValue<decimal>(dr, "TotalAmount");
                            claim.ClaimDetails = claimdetialDAC.GetListByClaimID(claimID);


                        }
                    }
                }

                return claim;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { claimID }, ex);
            }
        }


        public List<Claim> SelectByFilteration(Employee employee, string searchCondition)
        {
            string filter = "";

            if (employee.IsApprover == true)
            {
                filter = "WHERE Employees.branchCode =@branchCode AND (Employees.EmployeeID = @employeeID or (Employees.EmployeeID <> @EmployeeID AND Claims.Status <> 0))   ";
            }
            else
            {
                filter = "WHERE claims.employeeID =  @employeeID ";

            }

            if(!string.IsNullOrEmpty(searchCondition))
            {
                filter = filter + " AND status in (" + searchCondition + ")";
            }
            string SQL_STATEMENT =
               "SELECT  claimID, Employees.empName, datecreated, status, totalamount,DateSubmitted, dateapproved " +
                                  "FROM Claims " +
               " INNER JOIN Employees on Employees.EmployeeID = Claims.EmployeeID " +
              filter +
                   " ORDER BY datecreated desc ";

            

            List<Claim> result = new List<Claim>();

            // Connect to database.

            try
            {
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {

                    db.AddInParameter(cmd, "@employeeid", DbType.Int32, employee.EmployeeID);
                    db.AddInParameter(cmd, "@branchcode", DbType.Int32, employee.BranchCode);

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            // Create a new Contact
                            Claim claim = new Claim();

                            // Read values.

                            // Read values.
                            claim.ClaimID = Convert.ToInt32(dr["ClaimID"]);
                            claim.EmpName = base.GetDataValue<string>(dr, "EmpName");
                            claim.DateCreated = base.GetDataValue<DateTime>(dr, "DateCreated");
                            claim.DateSubmitted = base.GetDataValue<DateTime>(dr, "DateSubmitted");
                            claim.DateApproved = base.GetDataValue<DateTime>(dr, "Dateapproved");
                            claim.Status = (Status)base.GetDataValue<byte>(dr, "Status");

                            // Add to List.
                            result.Add(claim);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { employee, searchCondition }, ex);
            }
        }
    }
}
