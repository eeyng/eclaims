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

namespace eClaim.Services.Http
{
    [RoutePrefix("api/employee")]
    public class EmployeeService : ApiController 
    {
        [HttpPost]
        [Route("Authenticate")]
        public Employee Authenticate(Employee employee)
        {
            try
            {
                EmployeeComponent bc = new EmployeeComponent();
                return bc.Authenticate(employee);
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
