using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class adminlogin : System.Web.UI.Page
    {


        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // login button click event
        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {

                    con.Open();
                    Response.Write("<script>alert('connection was close now open');</script>");

                }
                Response.Write("<script>alert('above');</script>");

                SqlCommand cmd = new SqlCommand("select * from admin_login_tbl where username='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();


                Response.Write("<script>alert('below');</script>");


                if (dr.HasRows)
                {
                    Response.Write("<script>alert(' inside dr block');</script>");

                    while (dr.Read())
                    {
                        Response.Write("<script>alert('password id matched');</script>");

                        Response.Write("<script>alert('Successful login');</script>");
                        Session["username"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(2).ToString();
                        Session["role"] = "admin";
                        //Session["status"] = dr.GetValue(10).ToString(); "because admin always active this check is only for *user*"
                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid credentials');</script>");
                }
            }



            catch (Exception ex)
            {
                Response.Write("<script>alert('123444 " + ex.Message + "');</script>");
            }
        }
    }

}    
        

    
    
