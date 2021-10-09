using eClaim.Data;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eClaim.Business
{
    public class ClaimComponent
    {
        public List<Claim> GetClaimList(Employee employee, string searchCondition)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();
                EmployeeDAC employeeDAC = new EmployeeDAC();
                Employee searchemployee = employeeDAC.SelectByEmployeeID(employee.EmployeeID);
                return claimDAC.SelectByFilteration(searchemployee, searchCondition);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get claim list");
            }
        }
        private void ValidateClaim(Claim claim)
        {
            if (string.IsNullOrEmpty(claim.BankCode))
            {
                throw new ApplicationException("Please enter bank code");
            }
            if (string.IsNullOrEmpty(claim.BankAccNo))
            {
                throw new ApplicationException("Please enter bank account number");
            }
            if (string.IsNullOrEmpty(claim.BankAccName))
            {
                throw new ApplicationException("Please enter bank account name");
            }

            if (claim.ClaimDetails.Count == 0)
            {
                throw new ApplicationException("Please insert at least 1 expenses");
            }
        }

        public void ValidateClaimDetail(ClaimDetail claimDetail)
        {
            try
            {
                if (string.IsNullOrEmpty(claimDetail.SerialNo))
                    throw new ApplicationException("Please enter reference number");

                if (claimDetail.TransactionDate.Year==1)
                    throw new ApplicationException("Please enter transaction date");

                if (string.IsNullOrEmpty(claimDetail.CostCenter))
                    throw new ApplicationException("Please select cost center");

                if (string.IsNullOrEmpty(claimDetail.GLCodeID))
                    throw new ApplicationException("Please select GL Code");

                if (string.IsNullOrEmpty(claimDetail.Currency))
                    throw new ApplicationException("Please select currency");

                if (claimDetail.Amount  == 0)
                    throw new ApplicationException("Please enter amount");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to validate claim detail");
            }
        }
            public Claim GetClaim(Claim claim)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();

                return claimDAC.GetClaim(claim.ClaimID);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get claim");
            }
        }
        public List<Currency> GetCurrencyList()
        {
            try
            {
                CurrencyDAC currencyDAC = new CurrencyDAC();
                return currencyDAC.GetCurrencies();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get currencies");
            }
        }

        public Claim SaveDraft(Claim claim)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();
                ClaimDetailDAC claimdetailDAC = new ClaimDetailDAC();
                ValidateClaim(claim);
                claim.Status = Status.Draft;

                if (claim.ClaimID == 0)
                {

                    claim = claimDAC.Create(claim);
                    foreach (ClaimDetail claimdetail in claim.ClaimDetails)
                    {
                        claimdetail.ClaimID = claim.ClaimID;
                        claimdetailDAC.Create(claimdetail);
                    }
                }
                else
                {
                    UpdateClaim(claim);
                }


                return claim;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create claim");
            }
        }

        public Claim SubmitClaim(Claim claim)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();
                ClaimDetailDAC claimdetailDAC = new ClaimDetailDAC();
                ValidateClaim(claim);

                claim.Status = Status.Submitted;
                claim.DateSubmitted = DateTime.Now;

                if (claim.ClaimID == 0)
                {
                    claim = claimDAC.Create(claim);
                    foreach (ClaimDetail claimdetail in claim.ClaimDetails)
                    {
                        claimdetail.ClaimID = claim.ClaimID;
                        claimdetailDAC.Create(claimdetail);
                    }
                }
                else
                {
                    UpdateClaim(claim);
                }


                return claim;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to submit claim");
            }
        }

        private void UpdateClaim(Claim claim)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();
                ClaimDetailDAC claimDetailDAC = new ClaimDetailDAC();
                claimDAC.Update(claim);
                foreach (ClaimDetail claimDetail in claim.ClaimDetails)
                {
                    claimDetail.ClaimID = claim.ClaimID;

                    if (claimDetail.ClaimDetailID == 0)
                    {
                        claimDetailDAC.Create(claimDetail);
                    }
                    else
                    {
                        claimDetailDAC.Update(claimDetail);
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to update claim");
            }
        }

        public void ApproveRejectClaim(Claim claim)
        {
            try
            {
                ClaimDAC claimDAC = new ClaimDAC();
                claimDAC.ApproveRejectClaim(claim);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to approv or reject claim");
            }
        }
    }
}
