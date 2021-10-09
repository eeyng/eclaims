using eClaim.Data;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eClaim.Business
{
    public class EmployeeComponent
    {
        public Employee Authenticate(Employee employee)
        {
            EmployeeDAC employeeDAC = new EmployeeDAC();
            return employeeDAC.SelectByUserNamePassword(employee.EmpEmail, employee.EmpPass);
        }
    }
}
