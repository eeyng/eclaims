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
   public class ClaimDetailDAC:DataAccessComponent
    {
        public ClaimDetail Create(ClaimDetail claimdetail)
        {
            const string SQL_STATEMENT =
                    "INSERT INTO dbo.ClaimDetails (ClaimID,TransactionDate,SerialNo " +
          " ,CostCenter,GLCodeID,ClaimDetailDesc,Currency ,Amount " +
          "  ,GST ,ExchangeRate ,TotalAmt ,DateCreated,Status) " +
                    "VALUES(@ClaimID,@TransactionDate,@SerialNo " +
          " ,@CostCenter,@GLCodeID,@ClaimDetailDesc,@Currency ,@Amount " +
          "  ,@GST ,@ExchangeRate ,@TotalAmt ,@DateCreated,1) ";

            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@ClaimID", DbType.Int32, claimdetail.ClaimID);
                    db.AddInParameter(cmd, "@TransactionDate", DbType.DateTime, claimdetail.TransactionDate);
                    db.AddInParameter(cmd, "@SerialNo", DbType.AnsiString, claimdetail.SerialNo);
                    db.AddInParameter(cmd, "@CostCenter", DbType.AnsiString, claimdetail.CostCenter);
                    db.AddInParameter(cmd, "@GLCodeID", DbType.AnsiString, claimdetail.GLCodeID);
                    db.AddInParameter(cmd, "@ClaimDetailDesc", DbType.AnsiString, claimdetail.ClaimDetailDesc);
                    db.AddInParameter(cmd, "@Currency", DbType.AnsiString, claimdetail.Currency);
                    db.AddInParameter(cmd, "@Amount", DbType.Double, claimdetail.Amount);
                    db.AddInParameter(cmd, "@GST", DbType.Double, claimdetail.GST);
                    db.AddInParameter(cmd, "@ExchangeRate", DbType.Double, claimdetail.ExchangeRate);
                    db.AddInParameter(cmd, "@TotalAmt", DbType.Double, claimdetail.TotalAmt);
                    db.AddInParameter(cmd, "@DateCreated", DbType.DateTime, DateTime.Now);


                    claimdetail.ClaimDetailID = Convert.ToInt32(db.ExecuteScalar(cmd));

                }

                return claimdetail;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { claimdetail }, ex);
            }
        }

        public void Update(ClaimDetail claimdetail)
        {
            const string SQL_STATEMENT =
                    "Update dbo.ClaimDetailS set TransactionDate=@TransactionDate,  " +
                    "SerialNo = @SerialNo, " +
                    "CostCenter = @CostCenter, " +
                    "GLCodeID = @GLCodeID, " +
                    "ClaimDetailDesc = @ClaimDetailDesc, " +
                    "Currency = @Currency, " +
                    "Amount = @Amount, " +
                    "GST = @GST, " +
                    "ExchangeRate = @ExchangeRate, " +
                    "TotalAmt = @TotalAmt, " +
                         "Status = @Status " +
                    " where claimDetailid = @claimDetailid ";


            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@ClaimDetailID", DbType.Int32, claimdetail.ClaimDetailID);
                    db.AddInParameter(cmd, "@TransactionDate", DbType.DateTime, claimdetail.TransactionDate);
                    db.AddInParameter(cmd, "@SerialNo", DbType.AnsiString, claimdetail.SerialNo);
                    db.AddInParameter(cmd, "@CostCenter", DbType.AnsiString, claimdetail.CostCenter);
                    db.AddInParameter(cmd, "@GLCodeID", DbType.AnsiString, claimdetail.GLCodeID);
                    db.AddInParameter(cmd, "@ClaimDetailDesc", DbType.AnsiString, claimdetail.ClaimDetailDesc);
                    db.AddInParameter(cmd, "@Currency", DbType.AnsiString, claimdetail.Currency);
                    db.AddInParameter(cmd, "@Amount", DbType.Decimal, claimdetail.Amount);
                    db.AddInParameter(cmd, "@Status", DbType.Byte, claimdetail.Status);
                    db.AddInParameter(cmd, "@GST", DbType.Decimal, claimdetail.GST);
                    db.AddInParameter(cmd, "@ExchangeRate", DbType.Decimal, claimdetail.ExchangeRate);
                    db.AddInParameter(cmd, "@TotalAmt", DbType.Decimal, claimdetail.TotalAmt);

                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { claimdetail }, ex);
            }
        }

        public void Remove(ClaimDetail claimdetail)
        {
            const string SQL_STATEMENT =
                    "Update dbo.ClaimDetail set status=0 where claimdetailID =@ClaimDetailID ";


            try
            {
                // Connect to database.
                var db = new SqlDatabase(base.ConnectionString);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    // Set parameter values.
                    db.AddInParameter(cmd, "@ClaimDetailID", DbType.Int32, claimdetail.ClaimDetailID);
                 
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { claimdetail }, ex);
            }
        }

        public List<ClaimDetail> GetListByClaimID(int claimID)
        {
            string SQL_STATEMENT =
               " select ClaimDetailID,TransactionDate,SerialNo " +
          " ,CostCenter,GLCodeID,ClaimDetailDesc,Currency ,Amount " +
          "  ,GST ,ExchangeRate ,TotalAmt,status " + 
          " FROM ClaimDetails"+
          " WHERE status= 1 AND ClaimID = @ClaimID ";


            List<ClaimDetail> result = new List<ClaimDetail>();

            // Connect to database.

            try
            {
                var db = new SqlDatabase(base.ConnectionString);

                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    db.AddInParameter(cmd, "@ClaimID", DbType.Int32, claimID);

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        while (dr.Read())
                        {
                            // Create a new Contact

                            ClaimDetail claimDetail = new ClaimDetail();
                            // Read values.

                            claimDetail.ClaimDetailID = base.GetDataValue<int>(dr, "ClaimDetailID");
                            claimDetail.TransactionDate = base.GetDataValue<DateTime>(dr, "TransactionDate");
                            claimDetail.SerialNo = base.GetDataValue<string>(dr, "SerialNo");
                            claimDetail.CostCenter = base.GetDataValue<string>(dr, "CostCenter");
                            claimDetail.GLCodeID = base.GetDataValue<string>(dr, "GLCodeID");
                            claimDetail.ClaimDetailDesc = base.GetDataValue<string>(dr, "ClaimDetailDesc");
                            claimDetail.Currency = base.GetDataValue<string>(dr, "Currency");
                            claimDetail.Amount = base.GetDataValue<Decimal>(dr, "Amount");
                            claimDetail.GST = base.GetDataValue<Decimal>(dr, "GST");
                            claimDetail.Status = base.GetDataValue<byte>(dr, "Status");
                            claimDetail.ExchangeRate = base.GetDataValue<Decimal>(dr, "ExchangeRate");
                            claimDetail.TotalAmt = base.GetDataValue<Decimal>(dr, "TotalAmt");

                            result.Add(claimDetail);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(SQL_STATEMENT, new object[] { claimID }, ex);
            }
        }

    }
}
