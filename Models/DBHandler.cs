using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheatreMgnt.Models
{
    public class DBHandler
    {
        private SqlConnection con;
        private DataSet ds;
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
        }

        public bool add_category(Category category)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("add_category", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cat_type", category.Cat_type);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public DataTable ddlQuery(string str)
        {
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            sda.Fill(dt);
            con.Close();
            return dt;
        }
        public bool add_movie(Movie movie)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("add_movie", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Movie_name", movie.Movie_name);
            cmd.Parameters.AddWithValue("@Release_date", movie.Release_date);
            cmd.Parameters.AddWithValue("@Cat_id", movie.Cat_id);
            cmd.Parameters.AddWithValue("@Movie_rate", movie.Movie_rate);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public int login_user(User u)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("user_login", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", u.User_name);
            cmd.Parameters.AddWithValue("@pass", u.User_password);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                int id = (int)dr["User_id"];
                return id;
                
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                return 0;
            }
        }

        public List<Movie> GetAllMovies()
        {
            Connection();
            List<Movie> movielist = new List<Movie>();

            SqlCommand cmd = new SqlCommand("movie_display", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sda.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                movielist.Add(
                    new Movie
                    {
                        Movie_id = Convert.ToInt32(dr["Movie_id"]),
                        Movie_name = dr["Movie_name"].ToString(),
                        Release_date = Convert.ToDateTime(dr["Release_date"]),
                        Cat_id = Convert.ToInt32(dr["Cat_id"]),
                        Movie_rate = Convert.ToInt32(dr["Movie_rate"])
                    });
            }
            return movielist;
        }

        public List<Category> GetAllCategories()
        {
            Connection();
            List<Category> categorylist = new List<Category>();
            SqlCommand cmd = new SqlCommand("cat_display", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                categorylist.Add(
                    new Category
                    {
                        Cat_id = Convert.ToInt32(dr["Cat_id"]),
                        Cat_type = dr["Cat_type"].ToString()
                    });
            }
            return categorylist;
        }

        public Boolean UpdateUser(int uid, User user)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("user_update",con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_id", uid);
            cmd.Parameters.AddWithValue("@User_name", user.User_name);
            cmd.Parameters.AddWithValue("@User_email", user.User_email);
            cmd.Parameters.AddWithValue("@User_password", user.User_password);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            con.Open();
            int i = cmd.ExecuteNonQuery();
            //sda.Fill(dt);
            con.Close();
            //return dt;
            if(i>= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMovie(int id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("DELETE FROM tbl_movie WHERE Movie_id="+id, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i>= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean addBooking(Booking booking,int User_id,int amt)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("add_booking", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_id", User_id);
            cmd.Parameters.AddWithValue("@Cat_id", booking.Cat_id);
            cmd.Parameters.AddWithValue("@Movie_id", booking.Movie_id);
            cmd.Parameters.AddWithValue("@No_of_tickets", booking.No_of_tickets);
            cmd.Parameters.AddWithValue("@amt", amt);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public int get_rate(int id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SELECT Movie_rate FROM tbl_movie WHERE Movie_id=@Movie_id", con);
            cmd.Parameters.AddWithValue("@Movie_id", id);
            con.Open();
            int rate = (int)cmd.ExecuteScalar();
            con.Close();
            if (rate != 0)
            {
                return rate;
            }
            else
            {
                return 0;
            }
        }

        public DataTable GetMoviesByCat(int cat_id)
        {
            Connection();
            List<Movie> movielist = new List<Movie>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_movie WHERE Cat_id=@Cat_id", con);
            cmd.Parameters.AddWithValue("@Cat_id", cat_id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    movielist.Add(
            //        new Movie
            //        {
            //            Movie_id = Convert.ToInt32(dr["Movie_id"]),
            //            Movie_name = dr["Movie_name"].ToString()
            //        });
            //}        
            return dt;
        }

        //public List<Movie> GetMoviesByCategory(int categoryId)
        //{
        //    List<Movie> movies = new List<Movie>();
        //    Connection();
        //    SqlCommand cmd = new SqlCommand("GetMoviesByCategory", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Cat_id", categoryId);
        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Movie movie = new Movie
        //        {
        //            Movie_id = Convert.ToInt32(dr["Movie_id"]),
        //            Movie_name = dr["Movie_name"].ToString(),
        //            Release_date = Convert.ToDateTime(dr["Release_date"]),
        //            Cat_id = Convert.ToInt32(dr["Cat_id"]),
        //            Movie_rate = Convert.ToDecimal(dr["Movie_rate"])
        //        };
        //        movies.Add(movie);
        //    }
        //    con.Close();
        //    return movies;
        //}

        public List<Booking> GetBookings()
        {
            Connection();
            List<Booking> bookingList = new List<Booking>();

            // SQL Join to get readable Names and Rate instead of raw IDs
            string query = @"SELECT b.Booking_ID, b.User_ID, b.Cat_ID, b.Movie_ID, 
                            b.No_of_tickets, b.Amount, 
                            c.Cat_type, m.Movie_name, m.Rate 
                     FROM tbl_booking b
                     INNER JOIN tbl_movie_cat c ON b.Cat_id = c.Cat_id
                     INNER JOIN tbl_movie m ON b.Movie_id = m.Movie_id";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Booking bk = new Booking
                {
                    Booking_id = Convert.ToInt32(dr["Booking_ID"]),
                    User_id = Convert.ToInt32(dr["User_ID"]),
                    Cat_id = Convert.ToInt32(dr["Cat_ID"]),
                    Movie_id = Convert.ToInt32(dr["Movie_ID"]),
                    No_of_tickets = Convert.ToInt32(dr["No_of_tickets"]),
                    Amount = Convert.ToInt32(dr["Amount"]),
                    // Populating display values
                    Cat_type = Convert.ToString(dr["Cat_Type"]),
                    Movie_name = Convert.ToString(dr["Movie_name"]),
                    Movie_rate = Convert.ToInt32(dr["Rate"])
                };
                bookingList.Add(bk);
            }
            con.Close();
            return bookingList;
        }
    }
}