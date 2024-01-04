using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication3
{
    public partial class adminbookinventory : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBookbyID();
            }
        }
        //Go Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetBookbyID();

        }


        void GetBookbyID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //Response.Write("<script>alert('Database Connection Open by Running Code');</script>");

                }

                SqlCommand cmd = new SqlCommand("SELECT * from book_master_tbl where book_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)

                {

                    TextBox2.Text = dt.Rows[0]["book_name"].ToString();              //Book Name
                    TextBox3.Text = dt.Rows[0]["publish_date"].ToString();          //Publish Date


                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    //  Console.WriteLine("Genere = " + genre);
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;
                            }
                        }
                    }


                    TextBox4.Text = dt.Rows[0]["edition"].ToString();                // Edition..........ok
                    TextBox5.Text = dt.Rows[0]["book_cost"].ToString();              //BookCost(PerUnit).....ok
                    TextBox6.Text = dt.Rows[0]["no_of_pages"].ToString();          
                    //Pages  not working when text box property was number. later changed to single line worked.
                    TextBox7.Text = dt.Rows[0]["actual_stock"].ToString();           //Actual Stock........ok
                    TextBox8.Text = dt.Rows[0]["current_stock"].ToString();          //Current Stock........ok
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString();    //Book Discription....ok
                    DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //Language   
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
                    DropDownList1.SelectedValue = dt.Rows[0]["author_name"].ToString(); //Auther Name








                    //DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //language
                    //DropDownList3.ClearSelection();
                    //DropDownList3.SelectedValue = null;


                    //string name = dt.Rows[0]["language"].ToString();
                    // DropDownList3.SelectedValue = name;

                    // Response.Write("<script>alert('Language Value = " + name + "');</script>");



                    //  Response.Write("<script>alert('selected Value = " + DropDownList3.SelectedValue + "');</script>");
                    //Response.Write("<script>alert('Language Value = " + name + "');</script>");
                    //ListBox1.SelectedValue = dt.Rows[0]["genre"].ToString();      //Auther Name 
                    //TextBoxAuthor.Text = dt.Rows[0]["author_name"].ToString(); //Auther Name
                    //TextBoxPublisher.Text = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
                    //DropDownList3.Text = dt.Rows[0]["language"].ToString();//Akber Testing
                    //TextBox9.Text = dt.Rows[0]["issued_book"].ToString();          //Issued Book not in database.........................




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

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}