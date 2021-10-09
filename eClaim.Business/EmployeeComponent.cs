using eClaim.Data;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using eClaim.Shared.Security;
using System.Configuration;

namespace eClaim.Business
{
    public class EmployeeComponent
    {
        public Employee Authenticate(Employee employee)
        {
            EmployeeDAC employeeDAC = new EmployeeDAC();

            employee.EmpPass = Cryptography.Encrypt(employee.EmpPass, ConfigurationManager.AppSettings["crypass"]);
            employee =employeeDAC.SelectByUserNamePassword(employee.EmpEmail, employee.EmpPass);

            if(employee == null)
            {
                throw new ApplicationException("Invalid email or password");
            }

            return employee;
        }
    }
}
