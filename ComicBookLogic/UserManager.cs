using ComicBookDataAccess;
using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLogic
{
    public class UserManager
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



        public User AuthenticateLogin(string email)
        {
            User _user = null;
            UserRoles _userRoles = null;

            try
            {
                //string pwhash = HashSHA256(password);
                if (1 == UserAccessor.VerifyLoginInfo(email))
                {
                    //password = null;

                    _user = UserAccessor.RetrieveUserWithEmail(email);

                    _userRoles.Roles = UserAccessor.RetrieveUserRoles(_user.UserID);
                }
                else
                {
                    throw new ApplicationException("Incorrect email or password.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error has occurred. " + ex.Message);
            }

            return _user;
        }
    }
}
