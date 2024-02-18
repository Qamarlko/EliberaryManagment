using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//public: This keyword makes the class accessible from other parts of your code.
//partial: This keyword indicates that the class definition might be split across multiple files. This is a common feature of web forms in ASP.NET.
//System.Web.UI.Page: This is a base class provided by ASP.NET that provides functionality for web pages. It contains properties and methods specific to web forms, such as handling page events, interacting with controls, and managing the page lifecycle.

namespace WebApplication3
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        //below line picking the SQL connection string from web.config file so this connection string can be used in this page by invoking just(strcon)local varable on this pAGE you dont need to specify the connection string each and every time.
        //create new sqlconnection and connection to database by using connection string from web.config file
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;

        protected void Page_Load(object sender, EventArgs e)//Page Load Button 
        {


            if (!IsPostBack) 
             //Means page loaded first time not re-directed from server due to any page event with any service tag in URL.
            {          //**Or refreshed**

                ClearFormFields();
                fillAuthorPublisherValues();

            }

            GridView1.DataBind();



        }
        //Go Button to fetch book detail by ID
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetBookbyID();

        }

        //Add New Book Button
        protected void Button4_Click(object sender, EventArgs e)

        {
            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book with this id also exist. You can not add another Book with same Book ID');</script>");
            }
            else
            {
                AddNewBook();
            }
        }





        //Update Book
        protected void Button1_Click(object sender, EventArgs e)
        {
            updateBookByID();
        }

        //Delete Book
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteBookByID();
        }


        //User defined functions

        //UpdateBookbyID


        /*self version with errors*/
        protected void AddNewBook()
        {


            try
            {

                //foreach (int i in ListBox1.GetSelectedIndices()): This loop iterates through each index of the ***(selected items) in the ListBox1 control.
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                // genres = Adventure,Self Help,
                genres = genres.Remove(genres.Length - 1);

                /*ChatGpt Version for testing...this code worked*/
                string filepath = "~/book_inventory/books1.png";
                if (FileUpload1.HasFile)
                {
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/book_inventory/" + filename));
                    filepath = "~/book_inventory/" + filename;
                }

                /*Below code hade error but there was suspetion to SQL Block.. 2 Week it will took to sort out with help of ChatGpt*/
                /*Possible Error by ChatGpt.... Check if the file actually exists by verifying the value of filename. It's possible that no file is selected, and filename is empty.*/

                /*
                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
               */

                SqlConnection con = new SqlConnection(strcon);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }



                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl (book_id, book_name, genre, author_name, publish_date, book_cost, no_of_pages, book_description, actual_stock, current_stock, book_img_link, publisher_name, language, edition) VALUES (@book_id, @book_name, @genre, @author_name, @publish_date, @book_cost, @no_of_pages, @book_description, @actual_stock, @current_stock, @book_img_link, @publisher_name, @language, @edition)", con);



                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@author_name", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filepath);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@language", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                Response.Write("<script>alert('Book Added Successfully');</script>");
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }


        /*ChatGpt version
        protected void AddNewBook()
       {
           try
           {
               string genres = string.Join(",", ListBox1.GetSelectedIndices().Select(i => ListBox1.Items[i].Text));

               string filepath = "~/book_inventory/books1.png";
               if (FileUpload1.HasFile)
               {
                   string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                   FileUpload1.SaveAs(Server.MapPath("~/book_inventory/" + filename));
                   filepath = "~/book_inventory/" + filename;
               }

               using (SqlConnection con = new SqlConnection(strcon))
               {
                   con.Open();

                   SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl (book_id, book_name, genre, author_name, publish_date, book_cost, no_of_pages, book_description, actual_stock, current_stock, book_img_link, publisher_name, language, edition) VALUES (@book_id, @book_name, @genre, @author_name, @publish_date, @book_cost, @no_of_pages, @book_description, @actual_stock, @current_stock, @book_img_link, @publisher_name, @language, @edition)", con);

                   cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                   cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                   cmd.Parameters.AddWithValue("@genre", genres);
                   cmd.Parameters.AddWithValue("@author_name", DropDownList1.SelectedItem?.Value ?? ""); // Null check
                   cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                   cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                   cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                   cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                   cmd.Parameters.AddWithValue("@actual_stock", TextBox4.Text.Trim());
                   cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                   cmd.Parameters.AddWithValue("@book_img_link", filepath);
                   cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem?.Value ?? ""); // Null check
                   cmd.Parameters.AddWithValue("@language", DropDownList3.SelectedItem?.Value ?? ""); // Null check
                   cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());

                   cmd.ExecuteNonQuery();
               }

               GridView1.DataBind();
               Response.Write("<script>alert('Book Added Successfully');</script>");
           }
           catch (Exception ex)
           {
               Response.Write("<script>alert('" + ex.Message + "');</script>");
           }
       } */

        void updateBookByID()
        {
            //Response.Write("<script>alert('entered updateBookByID(function) Successfully');</script>");

            if (checkIfBookExists())
            {
                try
                {

                    {
                        // Response.Write("<script>alert('TextBox7 is empty');</script>");
                    }


                    int actual_stock = Convert.ToInt32(TextBox7.Text.Trim());


                    int current_stock = Convert.ToInt32(TextBox11.Text.Trim());



                    if (global_actual_stock == actual_stock)
                    {
                        //Response.Write("<script>alert('entered first if box Successfully');</script>");
                    }
                    else
                    {
                        if (actual_stock < global_issued_books)
                        {

                            Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            return;
                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            TextBox11.Text = "" + current_stock;
                        }
                    }

                    //foreach (int i in ListBox1.GetSelectedIndices()): This loop iterates through each <index> of the *******(selected items) in the ListBox1 control.
                    //The loop starts with the first selected item (index 0 in the array) and continues until it reaches the last selected item.
                    //ListBox1.GetSelectedIndices() returns an array of integers representing the indices of the selected items in the ListBox.
                    /*string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())//i represents the index of the current selected item. if nothing is selected then code will genrate error.
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);

                    */

                    string genres = "";

                    if (ListBox1.GetSelectedIndices().Length > 0)
                    {
                        foreach (int i in ListBox1.GetSelectedIndices())
                        {
                            genres = genres + ListBox1.Items[i] + ",";
                        }

                        genres = genres.Remove(genres.Length - 1);
                    }

                    string filepath = "~/book_inventory/books1";
                    //below started FileUpload Function

                    string filename = "";
                    

                    if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
                    {
                        filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                         
                      Response.Write("<script>alert('entered File uplad(function) Successfully=' + filename );</script>");
                        filepath = "~/book_inventory/" + filename;

                    }
                    else
                    {

                        if (filename == "" || filename == null)
                        {
                            filepath = global_filepath; 

                        }
                        else   
                        {
                            FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                           
                        }
                                                                    
                               
                  

                    }

                   
                    
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@author_name", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publish_date", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                    cmd.Parameters.AddWithValue("@book_img_link", filepath);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Updated Successfully');</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID');</script>");
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

                    // ListBox1.ClearSelection();
                    TextBox4.Text = dt.Rows[0]["edition"].ToString();                // Edition..........ok
                    TextBox5.Text = dt.Rows[0]["book_cost"].ToString();              //BookCost(PerUnit).....ok
                    TextBox6.Text = dt.Rows[0]["no_of_pages"].ToString();
                    //Pages  not working when text box property was number. later changed to single line worked.
                    TextBox7.Text = dt.Rows[0]["actual_stock"].ToString();     //Actual Stock........ok
                    TextBox11.Text = dt.Rows[0]["current_stock"].ToString();          //Current Stock........ok
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString();    //Book Discription....ok
                    TextBox9.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();
                    //Response.Write("<script>alert('Value of Global actual_stock = " + global_actual_stock + "');</script>");
                    //Response.Write("<script>alert('Value of Current current_stock = " + global_current_stock + "');</script>");

                    DropDownList3.ClearSelection();
                    DropDownList3.SelectedValue = dt.Rows[0]["language"].ToString(); //Language   
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString(); //Publisher Name
                    DropDownList1.SelectedValue = dt.Rows[0]["author_name"].ToString(); //Auther Name

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

                }
                else  //****if book not found previous values should be cleared 
                {
                    Response.Write("<script>alert('InvalidBookID');</script>");
                    DropDownList1.ClearSelection();
                    DropDownList2.ClearSelection();
                    DropDownList3.ClearSelection();
                    // ListBox1.Items[0].Selected = true;
                    ListBox1.ClearSelection();
                    //.Split(','): This method splits the string into an array of substrings based on the comma (',') delimiter. "Action,Drama,Comedy", == ["Action", "Drama", "Comedy"].

                    TextBox4.Text = "";                  // Edition..........ok
                    TextBox5.Text = "";                 //BookCost(PerUnit).....ok
                    TextBox6.Text = "";                //Pages.........................Workingnot 
                    TextBox3.Text = "";               //Publish Date........ok
                    TextBox7.Text = "";              //Actual Stock........ok
                    TextBox11.Text = "";            //Current Stock........ok
                    TextBox10.Text = "";           //Book Discription....ok
                    TextBox1.Text = "";           //Current Stock........ok
                    TextBox9.Text = "";          //Issued Book........ok  ................Workingnot 

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                //return false;
            }
        }

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
            //Response.Write("<script>alert('Check book');</script>");

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
                //  Response.Write("<script>alert('Check book');</script>");
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

        private void ClearFormFields()
        {

            DropDownList1.ClearSelection();
            DropDownList2.ClearSelection();
            DropDownList3.ClearSelection();
            ListBox1.Items[0].Selected = true;
            ListBox1.ClearSelection();
            TextBox4.Text = "";                   // Edition..........ok
            TextBox5.Text = "";                  //BookCost(PerUnit).....ok
            TextBox6.Text = "";
            TextBox7.Text = "";                //Actual Stock........ok
            TextBox11.Text = "";              //Current Stock........ok
            TextBox10.Text = "";             //Book Discription....ok
            TextBox2.Text = "";             //Book Name
            TextBox3.Text = " ";           //Publish 
        }

        void fillAuthorPublisherValues()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT author_name from author_master_tbl;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "author_name";
                DropDownList2.DataBind();

                cmd = new SqlCommand("SELECT publisher_name from publisher_master_tbl;", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DropDownList1.DataSource = dt;
                DropDownList1.DataValueField = "publisher_name";
                DropDownList1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }






    }
}
/* this time no move farword after sucessful compilation first practice this page throwly*/
/*    -----------------------------------------------NOTES--------------------------------------------


issue still Pending 
{
on refreshing the page value in textbox1 remains and execute the get book by id function autometically 
}
*
{...**TroubleShootingDoneSuccessfully
**Pages textbox not working when text box property was number. later changed to single line worked.
*"TextBox5.Text = "" + current_stock;" could also be the solution of above problem
}


 ***The "" + construct is a way to explicitly force the numerical expression to be treated as a string.
         When a string is combined with a non-string value using +, the non-string value is automatically converted to a string.
The final result of the expression "" + (arithmetic expression) is a string that represents the numerical value of the arithmetic expression.**bard  https://g.co/bard/share/c4d7cc9d42ad   */
/*Issues still need to be resolved
 1.On deleting the book record book id remains on PAGE after deletion..
** issues still need to resolve: on refreshing the page textbox1 value remain on page and values in other textbox still remain on page when it should be clen on rfreshing or remain is also the option but logic have to clear behind the code
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
//first error was occured when database file name was missmatch in datbase and C# page
//Second error was on page load funtion was clearing the book id in textbox1 and i was serching error in textbox properties why it is showing error. while it was programe logic error. 
//third error was in add new book function (mentioned in addfunction ) file uplad vlue was null so it was creating error and i am serching error in sql string.
//fourth error text box intger value was invisible, visible on making textbox property "inline".

//conclusion: there should be comolete map of page structure and logic in your mind for truble shooting. like complete paragraph of poetry not single line.