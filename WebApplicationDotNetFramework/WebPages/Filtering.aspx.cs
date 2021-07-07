using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationDotNetFramework.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Web.Configuration;
namespace WebApplicationDotNetFramework.WebPages
{
    public partial class Filtering : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        private static List<Movie> currentFilteredMovies = new List<Movie>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitFilteringButton_Click(object sender, EventArgs e)
        {
            Movie movie = new Movie();
            List<Movie> movies = new List<Movie>();

            currentFilteredMovies = movie.selectFiltered(connectionString, StartDateInput.Text, EndDateInput.Text, specKeyword.Text, minRating.Text);
            filteredTable.DataSource = currentFilteredMovies;
            filteredTable.DataBind();
            movieIds.DataSource = currentFilteredMovies.Select(x => x.MovieID);
            movieIds.DataBind();
        }
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            if (movieIds.SelectedValue == null)
                return;
            // getting selected row as Movie instance
             Movie selectedMovie = new Movie();
             foreach(var el in currentFilteredMovies)
             {
                 if(el.MovieID == Convert.ToInt32(movieIds.SelectedValue))
                 {
                     selectedMovie = el;
                 }
             }
            // insert order into orders table based on user's choice
            DateTime now = DateTime.Today;
            string rentalDate = now.ToString("u"); 
            rentalDate = rentalDate.Substring(0, 10);
            string movieID = selectedMovie.MovieID.ToString();
            string netAmount = selectedMovie.Price.ToString();
            DateTime RELEASE_DATE = DateTime.Parse(selectedMovie.ReleaseDate);
            bool giveDiscount = RELEASE_DATE > now.AddYears(-5);
            double discount = 0;
            if (giveDiscount)
                discount = 0.25;
            string grossAmount;
            if (discount == 0.25)
            { 
                grossAmount = ((Convert.ToDouble(netAmount) - Convert.ToDouble(netAmount) * discount) +
                (0.26 * Convert.ToDouble(netAmount))).ToString();
            }
            else
            {
                grossAmount = (Convert.ToDouble(netAmount) + (0.26 * Convert.ToDouble(netAmount))).ToString();
            }
            string returnDate;
            if (giveDiscount) // in case it is old movie - rent for 7 days
            {
                DateTime SevenDays = DateTime.Today.AddDays(7);
                returnDate = SevenDays.ToString("u");
                returnDate = returnDate.Substring(0, 10);
            }    
            else // in case it is new movie - rent for 3 days
            {
                DateTime ThreeDays = DateTime.Today.AddDays(3);
                returnDate = ThreeDays.ToString("u");
                returnDate = returnDate.Substring(0, 10);
            }

             // just in case it converts to 99,99 not 99.99
            netAmount = netAmount.Replace(",", ".");
            grossAmount = grossAmount.Replace(",", ".");
            string Discount = discount.ToString();
            if (discount == 0)
                Discount = "NULL";
            Discount = Discount.Replace(",", ".");
            string insertOrder = $"INSERT INTO ORDERS (RENTAL_DATE,RETURN_DATE,MOVIE_ID,NET_AMOUNT,DISCOUNT,GROSS_AMOUNT)" +
                $"VALUES (\'{rentalDate}\', \'{returnDate}\', {movieID}, {netAmount}, {Discount}, {grossAmount})";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(insertOrder, connection);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                    insertResult.Text = "Order added. Data inserted succesfully!";
            }
            catch(Exception exception)
            {
                insertResult.Text = $"Something went wrong. {exception.Message}";
            }
            connection.Close();
        }

        protected void mainPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Movies.aspx");
        }
    }
}