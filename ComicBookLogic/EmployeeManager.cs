using ComicBookDataAccess;
using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLogic
{
    public class EmployeeManager
    {
        public Employee AuthenticateEmpLogin(string email, string password)
        {
            Employee _employee = null;

            try
            {
                if (1 == EmployeeAccessor.VerifyEmpLoginInfo(email, password))
                {
                    password = null;

                    _employee = EmployeeAccessor.RetrieveEmployeeWithEmail(email);
                }
                else
                {
                    throw new ApplicationException("An error has occurred.");
                }
            }
            catch (Exception)
            {

                throw new ApplicationException("Incorrect email or password.");
            }

            return _employee;
        }

        public bool CreateEmployee(string firstName, string lastName, string phoneNum,
                                                    string zip, string email)
        {
            var result = false;

            try
            {
                result = (1 == EmployeeAccessor.CreateEmployee(firstName, lastName, phoneNum, zip, email));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + ex.StackTrace);
            }
            return result;
        }

        public bool UpdateEmployee(Employee employee, string firstName, string lastName, string phoneNum,
                                                    string zip, string email, bool active)
        {
            var result = false;
            int? employeeID = employee.EmployeeID;
            string oldFirstName = employee.FirstName;
            string oldLastName = employee.LastName;
            string oldPhoneNum = employee.PhoneNumber;
            string oldZip = employee.Zip;
            string oldEmail = employee.Email;
            bool oldActive = employee.Active;

            try
            {
                result = (1 == EmployeeAccessor.UpdateEmployee(employeeID, oldFirstName, oldLastName, oldPhoneNum, oldZip, oldEmail, oldActive,
                                                        firstName, lastName, phoneNum, zip, email, active));
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred updating customer info." + ex.Message);
            }

            return result;
        }

        public Employee RetrieveEmployeeByID(int? id)
        {
            Employee _employee = null;

            try
            {
                _employee = EmployeeAccessor.RetrieveEmployeeWithID(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _employee;
        }

        public Employee RetrieveEmployeeByEmail(string email)
        {
            Employee _employee = null;

            try
            {
                _employee = EmployeeAccessor.RetrieveEmployeeWithEmail(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _employee;
        }
    }
}
