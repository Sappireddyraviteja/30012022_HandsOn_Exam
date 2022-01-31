using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRecomendationDTO;

namespace BookRecomendationDataAccessLayer
{
    //DO NOT MODIFY THE METHOD NAMES : Adding of parameters / changing the return types of the given methods may be required.
    public class BookRecomendationDAL
    {


        BookRecomendationContext contextObj;
        SqlCommand cmdObj;
        SqlConnection conObj;
        public BookRecomendationDAL()
        {
            contextObj = new BookRecomendationContext();
            conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["BookConnectionString"].ConnectionString);
        }

        public List<BookDTO> FetchReviewsForBook()
        {
            try
            {
                var result = contextObj.Reviews.ToList();
                List<BookDTO> lstOfReview = new List<BookDTO>();
                foreach (var rev in result)
                {
                    lstOfReview.Add(new BookDTO()
                    {
                        BookIsbn = (int)rev.book_isbn,
                        BookRating = rev.rating,
                        BookReview = rev.review1

                    });

                }
                return lstOfReview;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int SaveReviewForBookToDB(BookDTO newReviewObj)
        {
            try
            {
                cmdObj = new SqlCommand();
                cmdObj.CommandText = @"uspAddReview";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@book_isbn", newReviewObj.BookIsbn);
                cmdObj.Parameters.AddWithValue("@rating", newReviewObj.BookRating);
                cmdObj.Parameters.AddWithValue("@review", newReviewObj.BookReview);

                SqlParameter parRetValue = new SqlParameter();
                parRetValue.Direction = ParameterDirection.ReturnValue;
                parRetValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(parRetValue);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(parRetValue.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}