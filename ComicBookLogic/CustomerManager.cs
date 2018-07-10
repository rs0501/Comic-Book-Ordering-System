using ComicBookDataAccess;
using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using BCrypt.Net;

namespace ComicBookLogic
{
    public class CustomerManager
    {
        internal string HashSHA256(string source)
        {
            var result = "";

            // this logic is always the same for our purposes
            // create a byte array (8 bit unsigned int)
            byte[] data;

            // Hash providers are all created with factory methods.
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // use a stringbuilder to conserve memory
            var s = new StringBuilder();

            // loop through the bytes creating characters
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();
            return result;
        }



        public Customer AuthenticateLogin(string email, string password)
        {
            Customer _customer = null;

            try
            {
                string pwhash = HashSHA256(password);
                if (1 == CustomerAccessor.VerifyLoginInfo(email, pwhash))
                {
                    password = null;

                    _customer = CustomerAccessor.RetrieveCustomerWithEmail(email);
                }
                else
                {
                    throw new ApplicationException("An error has occurred.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Incorrect email or password.");
            }

            return _customer;
        }

        public bool UpdateCustomerEmail(int? customerID, string email)
        {
            var result = false;

            try
            {
                result = (1 == CustomerAccessor.UpdateCustomerEmail(customerID, email));
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred updating email" + ex.Message);
            }

            return result;
        }

        public bool UpdateCustomer(Customer customer, string firstName, string lastName, string phoneNum,
                                                    string zip, string email, bool active)
        {
            var result = false;
            int? customerID = customer.CustomerID;
            string oldFirstName = customer.FirstName;
            string oldLastName = customer.LastName;
            string oldPhoneNum = customer.PhoneNumber;
            string oldZip = customer.Zip;
            string oldEmail = customer.Email;
            bool oldActive = customer.Active;

            try
            {
                result = (1 == CustomerAccessor.UpdateCustomerInfo(customerID, oldFirstName, oldLastName, oldPhoneNum, oldZip, oldEmail, oldActive,
                                                        firstName, lastName, phoneNum, zip, email, active));
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred updating customer info." + ex.Message);
            }

            return result;
        }

        public bool UpdateCustomerPassword(int? customerID, string oldPassword, string newPassword)
        {
            var result = false;

            try
            {
                result = (1 == CustomerAccessor.UpdateCustomerPasswordHash(customerID, oldPassword, newPassword));
            }
            catch (Exception ex)
            {

                throw new Exception("Something went wrong. Password not updated." + ex.Message + ex.StackTrace);
            }
            return result;
        }

        public bool CreateCustomerAccount(string firstName, string lastName, string phoneNum,
                                                    string zip, string email)
        {
            var result = false;

            try
            {
                result = (1 == CustomerAccessor.AddNewCustomer(firstName, lastName, phoneNum, zip, email));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + ex.StackTrace);
            }
            return result;
        }

        public List<Customer> CurrentCustomersList()
        {
            List<Customer> customers = null;

            try
            {
                customers = CustomerAccessor.RetrieveCurrentCustomers(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("A problem occurred loading this list of current customers" + ex.Message);
            }

            return customers;
        }

        public List<Customer> DeactivatedCustomersList()
        {
            List<Customer> customers = null;

            try
            {
                customers = CustomerAccessor.RetrieveDeactivatedCustomers(false);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("A problem occurred loading this list of current customers" + ex.StackTrace);
            }

            return customers;
        }

        public List<Customer> CurrentCustomers(bool active)
        {
            List<Customer> customers = null;

            try
            {
                customers = CustomerAccessor.RetrieveCurrentCustomers(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred loading current customers list" + ex.StackTrace);
            }

            return customers;
        }

        public Customer RetrieveCustomerByID(int? id)
        {
            Customer _customer = null;

            try
            {
                _customer = CustomerAccessor.RetrieveCustomerWithID(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _customer;
        }

        public int? RetrieveCustomerIdByEmail(string email)
        {
            int? id = 0;

            try
            {
                id = CustomerAccessor.RetrieveCustomerWithEmail(email).CustomerID;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return id;
        }
    }
}
