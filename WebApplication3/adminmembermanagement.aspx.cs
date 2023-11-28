using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class adminmembermanagement : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

       // public object TextBox11 { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView2.DataBind(); //for refreshing the live data from database in grid view.
        }


        // Go Button will fill the all detail of User by User_ID

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetMemberbyID();
        }

        //Activate Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updatememberstatusbyID("Active");
        }

        //Pending Button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updatememberstatusbyID("Pending");
        }
        
        //Deactive Button

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updatememberstatusbyID("Deactive");
        }

        //Delete Button
        protected void Button4_Click(object sender, EventArgs e)
        {
            DeleteMemberById();

        }



        //User Defined Function 
        void GetMemberbyID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_table where member_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows) 
               
                {
                    while (dr.Read())
                    {
                        TextBox1.Text = dr.GetValue(8).ToString();      //Member ID
                        TextBox2.Text = dr.GetValue(0).ToString();     //Full Name
                        TextBox7.Text = dr.GetValue(10).ToString();     //Account Status   
                        TextBox3.Text = dr.GetValue(1).ToString();      //DOB
                        TextBox9.Text = dr.GetValue(2).ToString();      //Contact Number
                        TextBox4.Text = dr.GetValue(3).ToString();      //Email-ID
                        TextBox5.Text = dr.GetValue(4).ToString();     //State
                        TextBox6.Text = dr.GetValue(5).ToString();      //City
                        TextBox8.Text = dr.GetValue(6).ToString();     //Pin-Code
                        TextBox10.Text = dr.GetValue(7).ToString();     //Full Postal Address 
                    }

                }
                else
                {
                    Response.Write("<script>alert('InvalidAuthorID');</script>");
                    /**/
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                //return false;
            }
        }

        void updatememberstatusbyID(string status)
        {



            //below is the update function copied from adminauthor managamnet 

            if (CheckIfMemberExists())
            {
                try
                {

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }


                    SqlCommand cmd = new SqlCommand("UPDATE member_master_table SET account_status = '" + status + "' WHERE member_id = '" + TextBox1.Text.Trim() + "'", con);


                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Response.Write($"Rows affected: {rowsAffected}");
                        Response.Write($"<script>alert('Member Status Updated')</script>");
                        con.Close();
                        GridView2.DataBind();//to refresh the data updated by this function execution 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");

                    }




                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Member dont exist');</script>");

            }

        }

        void DeleteMemberById()
        {
            //if (TextBox1.Text.Trim().Equals(""))
            if (CheckIfMemberExists())

            {
                try
                {


                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }

                    //have to check what hapeend when alteration in "" and '';  
                    SqlCommand cmd = new SqlCommand("Delete from  member_master_table  WHERE member_id ='" + TextBox1.Text.Trim() + "' ", con);

                    //test browser link dash board


                    // cmd.Parameters.AddWithValue("@account_status", "pending"); status only meant for user not admin and also this field is not avaialable in author mangemant table 
                    try
                    {
                        //Response.Write("<script>alert('*4*Entered in Try Box');</script>");
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Response.Write($"Rows affected: {rowsAffected}");
                        Response.Write("<script> alert('User Deleted Successfully ') </script>");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");

                    }


                    con.Close();
                    //Response.Write("<script>alert('Delete Successful & SQL connection closed . Go to User Login to Login');</script>");
                    clearform();
                    GridView2.DataBind();/*it will create live update from database to gridview after execution of this function*/

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }


            else
            {
                Response.Write("<script> alert('Member doesnot exost') </script>");

            }

        }

         void clearform()
                {
                    TextBox1.Text = "";      //Member ID
                    TextBox2.Text = "";
                    TextBox7.Text = "";
                    TextBox3.Text = "";
                    TextBox9.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox8.Text = "";
                    TextBox10.Text = "";
                    
                }
                    
        bool CheckIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_table where member_id='" + TextBox1.Text.Trim() + "';", con);


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