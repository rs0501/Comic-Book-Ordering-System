using ComicBookDataAccess;
using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLogic
{
    public class ComicManager
    {
        public List<Comic> AvailableComics
        {
            get
            {
                try
                {
                    return ComicAccessor.RetrieveComicByStatus(true);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Problem retrieving comics" + ex.Message + ex.StackTrace);
                }
            }

        }

        public List<Comic> RetrieveAvailableComics(bool status)
        {
            List<Comic> comics = null;

            try
            {
                comics = ComicAccessor.RetrieveComicByStatus(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem retrieving comics" + ex.Message + ex.StackTrace);
            }

            return comics;
        }

        public List<Comic> ComicsByTitle(string title)
        {
            List<Comic> _comics = new List<Comic>();

            try
            {
                _comics = ComicAccessor.RetrieveComicByTitle(title);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem retrieving comics" + ex.Message + ex.StackTrace);
            }

            return _comics;
        }

        public List<Comic> ComicsByAuthors(string authors)
        {
            List<Comic> _comics = new List<Comic>();

            try
            {
                _comics = ComicAccessor.RetrieveComicByAuthors(authors);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem retrieving comics" + ex.Message + ex.StackTrace);
            }

            return _comics;
        }

        public List<Comic> ComicsByPublisher(string publisher)
        {
            List<Comic> _comics = new List<Comic>();

            try
            {
                _comics = ComicAccessor.RetrieveComicByPublisher(publisher);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem retrieving comics" + ex.Message + ex.StackTrace);
            }

            return _comics;
        }

        public bool AddComicToInventory(string title, int issue, string authors, string publisher, string comicType,
                                        int distributorID, string rating, string description, int quantity, decimal price, bool status)
        {
            var result = false;

            try
            {
                result = (1 == ComicAccessor.AddComic(title, issue, authors, publisher, comicType, distributorID, rating, description, quantity, price, status));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem with adding comic." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public bool UpdateComicInfo(Comic comic, string title, int issue, string authors, string publisher, string comicType,
                                        int distributorID, string rating, string description, int quantity, decimal price, bool status)
        {
            var result = false;
            int? comicID = comic.ComicID;
            string oldTitle = comic.Title;
            int oldIssue = comic.Issue;
            string oldAuthors = comic.Authors;
            string oldPublisher = comic.Publisher;
            string oldComicType = comic.ComicType;
            int oldDistributorID = comic.DistributorID;
            string oldRating = comic.Rating;
            string oldDescription = comic.Description;
            int oldQuantity = comic.Quantity;
            decimal oldPrice = comic.Price;
            bool oldStatus = comic.Status;

            try
            {
                result = (1 == ComicAccessor.UpdateComic(comicID, oldTitle, oldIssue, oldAuthors, oldPublisher, oldComicType, 
                                                            oldDistributorID, oldRating, oldDescription, oldQuantity, oldPrice, oldStatus, 
                                                            title, issue, authors, publisher, comicType, distributorID, rating, 
                                                            description, quantity, price, status));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem with updating comic." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public bool AddComicToPullList(int? customerID, int? comicID)
        {
            var result = false;

            try
            {
                result = (1 == ComicAccessor.AddToPullList(customerID, comicID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem adding to pull list." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public bool RemoveComicFromPullList(int? comicID, int? customerID)
        {
            var result = false;

            try
            {
                result = (1 == ComicAccessor.RemoveFromPullList(comicID, customerID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem removing from pull list." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public List<Comic> PullListForCustomer(int? customerID)
        {
            List<Comic> _comics = new List<Comic>();

            try
            {
                _comics = CustomerAccessor.RetrieveCustomerPull(customerID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Pull list of comics not found." + ex.Message + ex.StackTrace);
            }

            return _comics;
        }

        public bool AddComicToOrderForm(int? customerID, int comicID, DateTime date)
        {
            var result = false;

            try
            {
                result = (1 == ComicAccessor.AddToOrder(customerID, comicID, date));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem adding to order form." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public bool RemoveComicFromOrderForm(int comicID, int? customerID)
        {
            var result = false;

            try
            {
                result = (1 == ComicAccessor.RemoveFromOrderForm(comicID, customerID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem removing from order form." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        public List<Comic> OrderFormForCustomer(int? customerID)
        {
            List<Comic> _comics = new List<Comic>();

            try
            {
                _comics = CustomerAccessor.RetrieveCustomerOrder(customerID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Your order form of comics was not found." + ex.Message + ex.StackTrace);
            }

            return _comics;
        }

        //public decimal CalculateOrderTotal(int? customerID, int comicID)
        //{
        //    decimal orderTotal = 0;

        //    try
        //    {
        //        orderTotal = ComicAccessor.RetrieveOrderTotal(customerID, comicID);
        //    }
        //    catch (Exception)
        //    {
        //        throw new ApplicationException("Incorrect email or password.");
        //    }

        //    return orderTotal;
        //}
    }
}
