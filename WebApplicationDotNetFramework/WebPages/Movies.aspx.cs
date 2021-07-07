using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationDotNetFramework.Data;
using System.Web.Configuration;

namespace WebApplicationDotNetFramework.WebPages
{
    public partial class Movies : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillMoviesGrid();
                FillOrdersGrid();
            }
        }
        private void FillMoviesGrid()
        {
            List<Movie> movies = new List<Movie>();
            Movie movie = new Movie();

            movies = movie.GetAllMovies(connectionString);
            GridViewMovies.DataSource = movies;
            GridViewMovies.DataBind();
        }
        private void FillOrdersGrid()
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();

            orders = order.GetAllOrders(connectionString);
            GridViewOrders.DataSource = orders;
            GridViewOrders.DataBind();

        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Filtering.aspx");
        }
        protected void UpdateDeleteButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateDelete.aspx");
        }
    }
}