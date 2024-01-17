using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication3
{
    public partial class adminbookinventory : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;

        protected void Page_Load(object sender, EventArgs e)//Page Load Button 
        {

           /* 
            * if (!IsPostBack)
             {
                TextBox1.Text = " ";        //   ........ok
            }
            else
            {
                TextBox1.Text = " ";
            } 
           */

            DropDownList1.ClearSelection();
            DropDownList2.ClearSelection();
            DropDownList3.ClearSelection();
            // ListBox1.Items[0].Selected = true;
            ListBox1.ClearSelection();
            TextBox4.Text = "";                  // Edition..........ok
            TextBox5.Text = "";                 //BookCost(PerUnit).....ok
            TextBox6.Text = "";
            TextBox7.Text = "";               //Actual Stock........ok
            TextBox11.Text = "";              //Current Stock........ok
            TextBox10.Text= "";             //Book Discription....ok
            TextBox2.Text = "";            //Book Name
            TextBox3.Text = " ";          //Publish Date




        } 

        protected void LinkButton1_Click(object sender, EventArgs e)//Go Button to fetch book detail by ID
        {
            GetBookbyID();

        }

        protected void Button4_Click(object sender, EventArgs e)   //Add New Book Button

        {
            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book with this id also exist. You can not add another Book with same Author ID');</script>");
            }
            else
            {
                //AddNewBook();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)  //Update Book
        {

        }
        protected void Button2_Click(object sender, EventArgs e) //Delete Book
        {
            deleteBookByID();
        }


        // user defined functions
        void deleteBookByID()
        {
            if (checkIfBookExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from book_master_tbl WHERE book_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Book Deleted Successfully');</script>");

                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }
            
        bool checkIfBookExists()
        {
            Response.Write("<script>alert('Check book');</script>");

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                     
                }

                SqlCommand cmd = new SqlCommand("SELECT * from book_master_tbl where book_id='" + TextBox1.Text.Trim() + "';", con);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Response.Write("<script>alert('Check book');</script>");
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

        void GetBookbyID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
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
                    TextBox11.Text = dt.Rows[0]["current_stock"].ToString();          //Current Stock........ok
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString();    //Book Discription....ok
                    TextBox9.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();

                    DropDownList3.ClearSelection();
                    DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //Language   
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
                    DropDownList1.SelectedValue = dt.Rows[0]["author_name"].ToString(); //Auther Name

                }
                else
                {
                    Response.Write("<script>alert('InvalidBookID');</script>");
                    DropDownList1.ClearSelection();
                    DropDownList2.ClearSelection();
                    DropDownList3.ClearSelection();
                    // ListBox1.Items[0].Selected = true;
                    ListBox1.ClearSelection();
                    TextBox4.Text = "";                 // Edition..........ok
                    TextBox5.Text = "";               //BookCost(PerUnit).....ok
                    TextBox6.Text = "";
                    TextBox7.Text = "";             //Actual Stock........ok
                    TextBox11.Text = "";            //Current Stock........ok
                    TextBox10.Text = "";          //Book Discription....ok
                    TextBox1.Text = "";          //Current Stock........ok

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                //return false;
            }
        }

     
       

    }
}
/* this time no move farword after sucessful compilation first practice this page throwly*/
/*    -----------------------------------------------NOTES--------------------------------------------
** issues still need to resolve: on refreshing the page textbox1 value remain on page and values in other textbox still remain on page when it should be clen on rfreshing or remain is also the option but logic have to clear behind the code
*

**Pages textbox not working when text box property was number. later changed to single line worked.



 ***The "" + construct is a way to explicitly force the numerical expression to be treated as a string.
         When a string is combined with a non-string value using +, the non-string value is automatically converted to a string.
The final result of the expression "" + (arithmetic expression) is a string that represents the numerical value of the arithmetic expression.**bard  https://g.co/bard/share/c4d7cc9d42ad   */
/*Issues still need to be resolved
 1.On deleting the book record book id remains on PAGE after deletion..


 */

/*
 *** DropDownList3.ClearSelection();
DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //Language   
DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
DropDownList1.SelectedValue = dt.Rows[0]["author_name"].ToString(); //Auther Name


Above code not working but suddnly start working
*/
/*Above code not working but suddnly start working
                    //DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //language
                    //Console.WriteLine("Genere = " + genre);
                    //DropDownList3.ClearSelection();
                    //DropDownList3.SelectedValue = null;
                    //string name = dt.Rows[0]["language"].ToString();
                    //DropDownList3.SelectedValue = name;
                    //Response.Write("<script>alert('Language Value = " + name + "');</script>");
                    //Response.Write("<script>alert('selected Value = " + DropDownList3.SelectedValue + "');</script>");
                    //Response.Write("<script>alert('Language Value = " + name + "');</script>");
                    //ListBox1.SelectedValue = dt.Rows[0]["genre"].ToString();      //Auther Name 
                    //TextBoxAuthor.Text = dt.Rows[0]["author_name"].ToString(); //Auther Name
                    //TextBoxPublisher.Text = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
                    //DropDownList3.Text = dt.Rows[0]["language"].ToString();//Akber Testing
                    //TextBox9.Text = dt.Rows[0]["issued_book"].ToString();          //Issued Book not in database.........................
                    */
