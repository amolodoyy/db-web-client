using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationDotNetFramework.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace WebApplicationDotNetFramework.WebPages
{
    public partial class UpdateDelete : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Order> orders = new List<Order>();
                Order order = new Order();
                orders = order.GetAllOrders(connectionString);

                ordersGrid.DataSource = orders;
                orderIds.DataSource = orders.Select(x => x.OrderID);
                ordersGrid.DataBind();
                orderIds.DataBind();
            }
        }
        protected void updateButton_Click(object sender, EventArgs e)
        {
            string ORDER_ID = orderIds.SelectedValue;
            string updateQ = "UPDATE ORDERS SET ";
            if (!String.IsNullOrEmpty(rentalDate.Text))
            {
                updateQ += $"RENTAL_DATE=\'{rentalDate.Text}\', ";
            }
            if (!String.IsNullOrEmpty(returnDate.Text))
            {
                updateQ += $"RETURN_DATE=\'{returnDate.Text}\', ";
            }
            if (!String.IsNullOrEmpty(movieID.Text))
            {
                updateQ += $"MOVIE_ID={movieID.Text}, ";
            }
            if (!String.IsNullOrEmpty(netAmount.Text))
            {
                updateQ += $"NET_AMOUNT={netAmount.Text}, ";
            }
            if (!String.IsNullOrEmpty(discount.Text))
            {
                updateQ += $"DISCOUNT={discount.Text}, ";
            }
            if (!String.IsNullOrEmpty(grossAmount.Text))
            {
                updateQ += $"GROSS_AMOUNT={grossAmount.Text}, ";
            }
            // maybe all fields were empty, then do nothing
            if (updateQ == $"UPDATE ORDERS SET ")
                return;
            updateQ = updateQ.Remove(updateQ.Length - 2); // removing last comma
            updateQ += $" WHERE ORDER_ID={ORDER_ID}";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(updateQ, connection);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                    updateResultMessage.Text = "Editing successful. 1 row was affected.";
                else
                    updateResultMessage.Text = "The error occured, and this is not an exception.";
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                updateResultMessage.Text = $"!!! ERROR: {ex.Message} !!!";
            }
        }
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rowsToDelete.Text))
                return;
            string deleteQ = "DELETE FROM ORDERS WHERE ";
            string[] ORDER_IDS = rowsToDelete.Text.Split(' ');
            for(int i = 0; i < ORDER_IDS.Length; i++)
            {
                if (i == 0)
                    deleteQ += $"ORDER_ID={ORDER_IDS[i]} ";
                else
                    deleteQ += $"AND ORDER_ID={ORDER_IDS[i]} ";
            }
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(deleteQ, connection);
            try
            {
                if (command.ExecuteNonQuery() == ORDER_IDS.Length)
                    deleteResultMessage.Text = "Everything was done correctly.";
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch(Exception deleteException)
            {
                deleteResultMessage.Text = $"!!! ERROR: {deleteException.Message}";
            }
            connection.Close();
        }

        protected void homePageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Movies.aspx");
        }
    }
}