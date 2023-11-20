using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class adminauthormanagement : System.Web.UI.Page

    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {




        }

        //Add Button
        protected void Button4_Click(object sender, EventArgs e)
        {


            if (CheckIfAutherExists())
            {
                Response.Write("<script>alert('Author with this id also exist. You can not add another Author with same Author ID');</script>");
            }
            else
            {
                AddNewAuthor();
            }

        }

        //Update Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckIfAutherExists())
            {
                UpdateAuthor();

            }
            else
            {

                Response.Write("<script>alert('Author with this id do not exist. You can not update another Author id without being adding it forst');</script>");
            }
        }


        //Delete Button
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (CheckIfAutherExists())
            {
                DeleteAuthor();
                


            }
            else
            {

                Response.Write("<script>alert('Author with this id do not exist. You can not Delete  Author id without being adding it forst');</script>");
            }

        }


        //Go Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            getAuthorByID();
        }

        // user defined method

        void getAuthorByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);


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
        void UpdateAuthor()
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
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name = @author_name WHERE author_id = @author_id", con);


                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
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
                Response.Write("<script>alert('Update Author Successful & SQL connection closed . Go to User Login to Login');</script>");
                clearform();
                GridView2.DataBind();/*it will create live update from database to gridview*/
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }


        }
        void DeleteAuthor()
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


                //cmd.ExecuteNonQuery(); freeze here because executed same command in line 98 with storing its boolen response in "int rowsAffected"
                //Response.Write($"con value: {con}");

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
        void AddNewAuthor()
        {
             try
            {
                 SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                  //.  Response.Write("<script>alert('*3*connection was closed now opened by signupfunction');</script>");

                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name) values(@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                 
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
                GridView2.DataBind();/*it will create live update from database to gridview*/

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }



        }
        bool CheckIfAutherExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);


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