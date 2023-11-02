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

                Response.Write("<script>alert('Author with this id also exist. You can not add another Author with same Author ID');</script>");
            else
            {
                AddNewAuthor();
            }

        }

        //Update Button
        protected void Button3_Click(object sender, EventArgs e)
        {

        }


        //Delete Button
        protected void Button5_Click(object sender, EventArgs e)
        {

        }


        //Go Button
        protected void Button2_Click(object sender, EventArgs e)
        {

        }


        // user defined method



        void AddNewAuthor()
        {
            Response.Write("<script>alert('Entered in signup member function');</script>");
            try
            {
                Response.Write("<script>alert('Database Connection Open by signUpNewMember()');</script>");
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    Response.Write("<script>alert('connection was closed now opened by signupfunction');</script>");

                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name) values(@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                 
               // cmd.Parameters.AddWithValue("@account_status", "pending");
                try
                {
                    Response.Write("<script>alert('Entered in Try Box');</script>");
                    int rowsAffected = cmd.ExecuteNonQuery();

                    Response.Write($"Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }


                //cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successful & SQL connection closed . Go to User Login to Login');</script>");
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
                    Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

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
    }
}