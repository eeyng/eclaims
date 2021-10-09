using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace eClaim.Web.Providers
{
    public class OAuthUtil
    {
        public static ClaimsIdentity CreateClaims(Employee employee, string authenticationType)
        {
            var claims = new List<System.Security.Claims.Claim>();

            // Add the name of the user.
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Name, employee.EmpName));

            // Add userId.
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, employee.EmployeeID.ToString()));

            return new ClaimsIdentity(claims, authenticationType);
        }
    }
}