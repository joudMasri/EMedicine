using System.Data.SqlClient;
using System.Data;
using System;

namespace E_Medicine_BE.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection) {
            Response response = new Response(); 
            SqlCommand cmd = new SqlCommand("sp_register",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Last_Name", users.Last_Name);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Successfully";
            }
            else {
                response.StatusCode = 100;
                response.StatusMessage = "User Registration Failed";

            }

            return response;
        }
        public Response Login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login",connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email",users.Email);
            da.SelectCommand.Parameters.AddWithValue("Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.First_Name = Convert.ToString(dt.Rows[0]["First_Name"]);
                user.Last_Name = Convert.ToString(dt.Rows[0]["Last_Name"]);
                user.Email= Convert.ToString(dt.Rows[0]["Email"]);
                user.Type= Convert.ToString(dt.Rows[0]["Type"]);


                response.StatusCode = 200;
                response.StatusMessage = "User is Valid";
                response.User = user;
            }
            else {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.User = null;
            }
            return response;
        }

        public Response viewUser(Users users, SqlConnection connection) {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.First_Name = Convert.ToString(dt.Rows[0]["First_Name"]);
                user.Last_Name = Convert.ToString(dt.Rows[0]["Last_Name"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.Created_On = Convert.ToDateTime(dt.Rows[0]["Created_On"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);


                response.StatusCode = 200;
                response.StatusMessage = "User is Exist";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Does not Exist";
                response.User = user;
            }
            return response;
        }

        public Response updateProfile(Users users, SqlConnection connection) {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_updateProfile",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@First_Name",users.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", users.Last_Name);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record Updated Successfully";

            }
            else {
                response.StatusCode=100;
                response.StatusMessage = "Some error occured. Try after somtime";
            }

            return response;
        }

        public Response addToCart(Cart cart, SqlConnection connection)    {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_Id",cart.User_Id);
            cmd.Parameters.AddWithValue("Unit_Price", cart.User_Id);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@Total_Price", cart.Total_Price);
            cmd.Parameters.AddWithValue("@Medicine_Id", cart.Medicine_Id);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item Added Successfully";
            }
            else {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }

            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection) {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed Successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }



            return response;

        }

        public Response orderList(Users users, SqlConnection connection) {
            Response response = new Response();
            List<Orders> listOrders =new List<Orders>();    
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList",connection);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type",users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID",users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Rows.Count; i++) {
                    Orders order  =new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.Order_No = Convert.ToString(dt.Rows[i]["Order_No"]);
                    order.Order_Total = Convert.ToDecimal(dt.Rows[i]["Order_Total"]);
                    order.Order_Status = Convert.ToString(dt.Rows[i]["Order_Status"]);
                    listOrders.Add(order);  
                }

                if (listOrders.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details Fetched";
                    response.List_Orders = listOrders;
                }
                else {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not Available";
                    response.List_Orders = null;
                }
            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not Available";
                response.List_Orders = null;
            }

            return response;
        }

        public Response addUpdateMedicine(Medicines medicines, SqlConnection connection) {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_addUpdateMedicine");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name",medicines.Name);
            cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@Unit_Price", medicines.Unit_Price);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@Exp_Date", medicines.Exp_Date);
            cmd.Parameters.AddWithValue("@Image_Url", medicines.Image_Url);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            cmd.Parameters.AddWithValue("@Type", medicines.Type);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine inserted successfully";
            }
            else {
                response.StatusCode = 200;
                response.StatusMessage = "Medicine did not save, try agian";
            }



            return response;
        }

        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
           
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user= new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.First_Name= Convert.ToString(dt.Rows[i]["First_Name"]);
                    user.Last_Name= Convert.ToString(dt.Rows[i]["Last_Name"]);
                    user.Password= Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund= Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.Created_On = Convert.ToDateTime(dt.Rows[i]["Created_On"]);
                    listUsers.Add(user);
                }

                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users details Fetched";
                    response.List_Users = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users details are not Available";
                    response.List_Users = null;
                }
            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Users details are not Available";
                response.List_Orders = null;
            }

            return response;
        }


    }
}
