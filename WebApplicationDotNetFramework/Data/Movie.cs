using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace WebApplicationDotNetFramework.Data
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string ReleaseDate { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; }

        public List<Movie> GetAllMovies(string connectionString)
        {
            List<Movie> movies = new List<Movie>();
            SqlConnection connection = new SqlConnection(connectionString);

            string selectAll = "SELECT * FROM MOVIES";
            connection.Open();

            SqlCommand command = new SqlCommand(selectAll, connection);
            SqlDataReader dr = command.ExecuteReader();

            if(dr != null)
            {
                while(dr.Read())
                {
                    Movie movie = new Movie();
                    movie.MovieID = Convert.ToInt32(dr["MOVIE_ID"]);
                    movie.MovieTitle = dr["MOVIE_TITLE"].ToString();
                    movie.ReleaseDate = dr["RELEASE_DATE"].ToString();
                    movie.Price = Convert.ToDouble(dr["PRICE"]);
                    movie.Rating = Convert.ToDouble(dr["RATING"]);
                    movie.Genre = dr["GENRE"].ToString();

                    movies.Add(movie);
                }
            }
            connection.Close();
            return movies;
        }
        public List<Movie> selectFiltered(string connectionString, string startDate, string endDate, string keyword, string rating)
        {
            List<Movie> movies = new List<Movie>();
            SqlConnection connection = new SqlConnection(connectionString);
            string selectFiltered = "SELECT * FROM MOVIES WHERE ";
            // adding queries step by step
            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                selectFiltered += $"RELEASE_DATE BETWEEN \'{startDate}\' AND" +
                   $"\'{endDate}\' " ;
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                if (selectFiltered != "SELECT * FROM MOVIES WHERE ")
                    selectFiltered += "AND ";
                selectFiltered += $" GENRE LIKE \'%{keyword}%\' ";
            }
            if (!String.IsNullOrEmpty(rating))
            {
                if (selectFiltered != "SELECT * FROM MOVIES WHERE ")
                    selectFiltered += "AND ";
                selectFiltered += $"RATING>{Convert.ToDouble(rating)}";
            }

            if (selectFiltered == "SELECT * FROM MOVIES WHERE ")
                selectFiltered = "SELECT * FROM MOVIES";

            connection.Open();

            SqlCommand command = new SqlCommand(selectFiltered, connection);
            SqlDataReader dr = command.ExecuteReader();
            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        Movie movie = new Movie();
                        movie.MovieID = Convert.ToInt32(dr["MOVIE_ID"]);
                        movie.MovieTitle = dr["MOVIE_TITLE"].ToString();
                        movie.ReleaseDate = dr["RELEASE_DATE"].ToString();
                        movie.Price = Convert.ToDouble(dr["PRICE"]);
                        movie.Rating = Convert.ToDouble(dr["RATING"]);
                        movie.Genre = dr["GENRE"].ToString();

                        movies.Add(movie);
                    }
                }
                catch
                {
                    connection.Close();
                    return movies;
                }

            }
            connection.Close();
            return movies;

        }
    }
}