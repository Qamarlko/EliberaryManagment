using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.imgs
{



    public partial class publisher : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //Add Publisher 
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (CheckIfPublisherExists())
            {
                Response.Write("<script>alert('Publisher with this id also exist. You can not add another Author with same AuPublisher ID');</script>");
            }
            else
            {
                AddNewPublisher();
            }
        }


        //Update Publisher 
        protected void Button3_Click(object sender, EventArgs e)
        {

            if (CheckIfPublisherExists())
            {
                UpdatePublisher();

            }
            else
            {

                Response.Write("<script>alert('Publisher with this id do not exist. You can not update another Author id without being adding it forst');</script>");
            }

        }


        //Delete Publisher 
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (CheckIfPublisherExists())
            {
                DeletePublisher();



            }
            else
            {

                Response.Write("<script>alert('Publisher with this id do not exist. You can not Delete  Author id without being adding it forst');</script>");
            }
        }

        //Go Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            GetPublisherByID();
        }


        // user defined method

        void GetPublisherByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from publisher_master_tbl where publisher_id='" + TextBox1.Text.Trim() + "';", con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('InvalidAuthorID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                //return false;
            }


        }
        void UpdatePublisher()
        {
            try
            {
                //Response.Write("<script>alert('*2*Database Connection Open by Updateauthor()');</script>");

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }


                //SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name = @author_name WHERE author_id = '" + TextBox2.Text.Trim() + "'", con);
                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name = @publisher_name WHERE  publisher_id = @publisher_id", con);


                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                // cmd.Parameters.AddWithValue("@account_status", "pending"); status only meant for user not admin and also this field is not avaialable in author mangemant table 
                try
                {
                     int rowsAffected = cmd.ExecuteNonQuery();

                    Response.Write($"Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }


                //cmd.ExecuteNonQuery(); freeze here because executed same command in line 98 with storing its boolen response in "int rowsAffected"                     Response.Write($"con value: {con}");

                con.Close();
                Response.Write("<script>alert('Update Author Successful & SQL connection closed . Go to User Login to Login');</script>");
                clearform();
                GridView2.DataBind();/*it will create live update from database to gridview*/
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }








            void DeletePublisher()
            {
                try
                {


                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }


                    SqlCommand cmd = new SqlCommand("Delete from  author_master_tbl  WHERE author_id = @author_id", con);



                    cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

                    // cmd.Parameters.AddWithValue("@account_status", "pending"); status only meant for user not admin and also this field is not avaialable in author mangemant table 
                    try
                    {
                        //Response.Write("<script>alert('*4*Entered in Try Box');</script>");
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Response.Write($"Rows affected: {rowsAffected}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");

                    }


                    //cmd.ExecuteNonQuery(); freeze here because executed same command in line 98 with storing its boolen response in "int rowsAffected"                     Response.Write($"con value: {con}");

                    con.Close();
                    //Response.Write("<script>alert('Delete Successful & SQL connection closed . Go to User Login to Login');</script>");
                    clearform();
                    GridView2.DataBind();/*it will create live update from database to gridview*/

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }

        }
        void DeletePublisher()
        {
            try
            {


                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }


                SqlCommand cmd = new SqlCommand("Delete from  publisher_master_tbl  WHERE publisher_id = @publisher_id", con);



                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

                // cmd.Parameters.AddWithValue("@account_status", "pending"); status only meant for user not admin and also this field is not avaialable in author mangemant table 
                try
                {
                    //Response.Write("<script>alert('*4*Entered in Try Box');</script>");
                    int rowsAffected = cmd.ExecuteNonQuery();

                    Response.Write($"Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }


                //cmd.ExecuteNonQuery(); freeze here because executed same command in line 98 with storing its boolen response in "int rowsAffected"                     Response.Write($"con value: {con}");

                con.Close();
                //Response.Write("<script>alert('Delete Successful & SQL connection closed . Go to User Login to Login');</script>");
                clearform();
                GridView2.DataBind();/*it will create live update from database to gridview*/

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }
        void AddNewPublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //.  Response.Write("<script>alert('*3*connection was closed now opened by signupfunction');</script>");

                }
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id,publisher_name) values(@publisher_id,@publisher_name)", con);
                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                // cmd.Parameters.AddWithValue("@account_status", "pending"); status only meant for user not admin and also this field is not avaialable in author mangemant table 
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    Response.Write($"Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }


                //cmd.ExecuteNonQuery(); freeze here because executed same command in line 79 with storing its boolen response in "int rowsAffected"
                con.Close();
                Response.Write("<script>alert('Sign Up Successful & SQL connection closed . Go to User Login to Login');</script>");
                clearform();
                //GridView2.DataBind();/*it will create live update from database to gridview*/

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }



        }
        bool CheckIfPublisherExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from publisher_master_tbl where publisher_id='" + TextBox1.Text.Trim() + "';", con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }

        }
        void clearform()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

       
    }
}

