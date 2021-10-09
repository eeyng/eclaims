using eClaim.Data;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eClaim.Business
{
   public class ClaimComponent
    {
        public List<Claim> GetClaimList(Employee employee, string searchCondition, int currentPage, int pageSize)
        {
            ClaimDAC claimDAC = new ClaimDAC();
         return  claimDAC.SelectByFilteration(employee.EmployeeID, searchCondition, currentPage, pageSize);
        }

        public Claim CreateClaim(Claim claim)
        {
            ClaimDAC claimDAC = new ClaimDAC();
            return claimDAC.Create(claim);
        }

        public void UpdateClaim(Claim claim)
        {
            ClaimDAC claimDAC = new ClaimDAC();
             claimDAC.Update(claim);
        }

        public void ApproveClaim(Claim claim)
        {
            ClaimDAC claimDAC = new ClaimDAC();
            claimDAC.Update(claim);
        }
    }
}
