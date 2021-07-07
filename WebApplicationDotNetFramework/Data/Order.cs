using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
namespace WebApplicationDotNetFramework.Data
{
    public class Order
    {
        public int OrderID { get; set; }
        public string RentalDate { get; set; }
        public string ReturnDate { get; set; }
        public int MovieID { get; set; }
        public double NetAmount { get; set; }
        public double Discount { get; set; }
        public double GrossAmount { get; set; }


        public List<Order> GetAllOrders(string connectionString)
        {
            List<Order> orders = new List<Order>();
            SqlConnection connection = new SqlConnection(connectionString);

            string selectAll = "SELECT * FROM ORDERS";
            connection.Open();

            SqlCommand command = new SqlCommand(selectAll, connection);
            SqlDataReader dr = command.ExecuteReader();

            if(dr!=null)
            {
                while(dr.Read())
                {
                    Order order = new Order();
                    order.OrderID = Convert.ToInt32(dr["ORDER_ID"]);
                    order.RentalDate = dr["RENTAL_DATE"].ToString();
                    order.ReturnDate = dr["RETURN_DATE"].ToString();
                    order.MovieID = Convert.ToInt32(dr["MOVIE_ID"]);
                    order.NetAmount = Convert.ToDouble(dr["NET_AMOUNT"]);
                    if (dr["DISCOUNT"] != DBNull.Value)
                        order.Discount = Convert.ToDouble(dr["DISCOUNT"]);
                    else
                        order.Discount = 0;
                    order.GrossAmount = Convert.ToDouble(dr["GROSS_AMOUNT"]);
                    orders.Add(order);
                }
            }
            connection.Close();
            return orders;
        }
    }
}