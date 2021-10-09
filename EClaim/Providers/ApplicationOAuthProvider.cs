using eClaim.Entities;
using eClaim.Process;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EClaim.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                EmployeeProcess _upc = new EmployeeProcess();
                Employee employee = new Employee();

                employee = context.OwinContext.Get<Employee>("Authenticated");

                try
                {
                    employee = _upc.Authenticate(employee);

                    if (employee == null)
                    {
                        context.SetError("invalid_credential", "Invalid username or password.");
                    }

                    //ClientProfile clientProfile = _upc.ValidateClient(user.UserID);
                }
                catch (HttpResponseException aex)
                {
                    throw new HttpResponseException(aex.Response);
                }


                var properties = CreateProperties(employee.EmployeeID,employee.EmpName);
                var ticket = new AuthenticationTicket(OAuthUtil.CreateClaims(employee, OAuthDefaults.AuthenticationType),
                                                        properties);

                context.Validated(ticket);
            }
            catch (HttpResponseException ex)
            {
                context.SetError("invalid_credential", "Invalid username or password.");
            }
            catch (Exception ex)
            {
                context.SetError("invalid_credential", ex.Message);
            }
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = default(string);
            string clientSecret = default(string);

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            { 
                Employee emp = new Employee();
                emp.EmpEmail = clientId;
                emp.EmpPass = clientSecret;
                context.OwinContext.Set<Employee>("Authenticated", emp);
                context.Validated();
            }
            else
            {
                context.SetError("invalid_credential", "Invalid username or password.");
                context.Rejected();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// This method is for adding extra properties to Token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(int empID,string empName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
                {
                { "employeeID", empID.ToString() },
                { "name", empName}
                };
            return new AuthenticationProperties(data);
        }
    }
}