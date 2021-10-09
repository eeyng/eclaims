using eClaim.Business;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eClaim.Process
{
  public  class EmployeeProcess : ProcessBase
    {
        [HttpPost]
        [Route("Authenticate")]
        public Employee Authenticate(Employee employee)
        {
            try
            {

                using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<Employee>(HttpMethod.Post, "api/employee/Authenticate", employee)).Result)
                {
                    employee = base.VerifyStatusCodeAsync<Employee>(response);
                }

                return employee;
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }
    }
}
