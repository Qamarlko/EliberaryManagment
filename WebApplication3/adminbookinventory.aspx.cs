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

        protected void Page_Load(object sender, EventArgs e)//Page Load Event 
        {
                        
            if (!IsPostBack)
            //Means page loaded first time not re-directed from server due to any page event with any service tag in URL.
            {          //**Or refreshed**

                ClearFormFields();
                fillAuthorPublisherValues();
            }
              /*if (IsPostBack)
               //Means page loaded first time not re-directed from server due to any page event with any service tag in URL.
               {   //**Or refreshed**

                   ClearFormFields();
                   fillAuthorPublisherValues();
                }*/
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
            // ListBox1.ClearSelection();//for clearing the previous selected value of Genre 

            try
            {


                //foreach (int i in ListBox1.GetSelectedIndices()): This loop iterates through each index of the ***(selected items) in the ListBox1 control.
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }

                // genres = Adventure,Self Help,
                // Remove the last comma
                if (!string.IsNullOrEmpty(genres))
                {
                    genres = genres.Remove(genres.Length - 1);
                }
                else
                {
                    // Prompt the user to select at least one genre
                    Response.Write("<script>alert('Please select at least one genre.');</script>");
                    return; // Exit the method
                }


                /*ChatGpt Version for testing...this code worked*/
                string filepath = "~/Upload/books1.png";
                
                try
                {
                    
                    

                        if (FileUpload1.HasFile)
                        {//In C#, System.IO.Path is a class that provides static methods for working with file paths. It enables you to perform various operations on file and directory paths, such as combining paths, extracting file names, getting the extension of a file, and more.

                        // Check file size
                        if (FileUpload1.PostedFile.ContentLength > 2 * 1024 * 1024) // 2MB limit
                        {
                            lblmessage.Text = "File size should not exceed 2MB";
                            lblmessage.ForeColor = System.Drawing.Color.Red;
                            return; // Exit the method
                        }

                            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
                            if (fileExtension.ToLower() != ".jpg" && fileExtension.ToLower() != ".jpeg")
                            {
                                lblmessage.Text = "File Not Uploaded";
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                                lblmessage.Font.Bold = true;
                                lblmessage.Font.Size = FontUnit.Point(24);
                                lblmessage.BackColor = System.Drawing.Color.Yellow;
                                lblmessage.ForeColor = System.Drawing.Color.Green;

                                lblmessage.Text = "only JPEG & PNG extention files are allowed";
                                lblmessage.ForeColor = System.Drawing.Color.Red;
                                Response.Write("<script>alert('message');</script>");
                            return;

                            }


                            else
                            {

                                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                                filepath = "~/Upload/" + filename;
                                FileUpload1.SaveAs(Server.MapPath("~/Upload/" + filename));
                                lblmessage.Text = "File Uploaded";
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                                lblmessage.Font.Bold = true;
                                lblmessage.Font.Size = FontUnit.Point(24);
                                lblmessage.BackColor = System.Drawing.Color.Yellow;
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                             //   break;
                            }
                        }

                        else
                        {
                            lblmessage.Text = "Please Select a file for upload";
                            //break;
                        }
                    
                }


            catch (Exception ex)
            {
                // Log the exception or print the error message to understand the issue.
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
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
                cmd.Parameters.AddWithValue("@book_cost", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filepath);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@language", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox4.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                Response.Write("<script>alert('Book Added with id " +  TextBox1.Text.Trim()  + " Successfully');</script>");
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


                    //foreach (int i in ListBox1.GetSelectedIndices()): This loop iterates through each index of the ***(selected items) in the ListBox1 control.
                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }

                    // genres = Adventure,Self Help,
                    // Remove the last comma
                    if (!string.IsNullOrEmpty(genres))
                    {
                        genres = genres.Remove(genres.Length - 1);
                    }
                    else
                    {
                        // Prompt the user to select at least one genre
                        Response.Write("<script>alert('Please select at least one genre.');</script>");
                        return; // Exit the method
                    }


                    /*ChatGpt Version for testing...this code worked*/
                    string filepath = "~/Upload/books1.png";

                    try
                    {



                        if (FileUpload1.HasFile)
                        {//In C#, System.IO.Path is a class that provides static methods for working with file paths. It enables you to perform various operations on file and directory paths, such as combining paths, extracting file names, getting the extension of a file, and more.

                            // Check file size
                            if (FileUpload1.PostedFile.ContentLength > 2 * 1024 * 1024) // 2MB limit
                            {
                                lblmessage.Text = "File size should not exceed 2MB";
                                lblmessage.ForeColor = System.Drawing.Color.Red;
                                return; // Exit the method
                            }

                            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
                            if (fileExtension.ToLower() != ".jpg" && fileExtension.ToLower() != ".jpeg")
                            {
                                lblmessage.Text = "File Not Uploaded";
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                                lblmessage.Font.Bold = true;
                                lblmessage.Font.Size = FontUnit.Point(24);
                                lblmessage.BackColor = System.Drawing.Color.Yellow;
                                lblmessage.ForeColor = System.Drawing.Color.Green;

                                lblmessage.Text = "only JPEG & PNG extention files are allowed";
                                lblmessage.ForeColor = System.Drawing.Color.Red;
                                Response.Write("<script>alert('message');</script>");
                                return;

                            }


                            else
                            {

                                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                                filepath = "~/Upload/" + filename;
                                FileUpload1.SaveAs(Server.MapPath("~/Upload/" + filename));
                                lblmessage.Text = "File Uploaded";
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                                lblmessage.Font.Bold = true;
                                lblmessage.Font.Size = FontUnit.Point(24);
                                lblmessage.BackColor = System.Drawing.Color.Yellow;
                                lblmessage.ForeColor = System.Drawing.Color.Green;
                                //   break;
                            }
                        }

                        else
                        {
                            lblmessage.Text = "Please Select a file for upload";
                            //break;
                        }

                    }


                    catch (Exception ex)
                    {
                        // Log the exception or print the error message to understand the issue.
                        Response.Write($"<script>alert('Error: {ex.Message}');</script>");
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
                    cmd.Parameters.AddWithValue("@book_cost", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@current_stock", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_img_link", filepath);
                    cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@language", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", TextBox4.Text.Trim());

                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Added with id " + TextBox1.Text.Trim() + " Successfully');</script>");
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
            ClearFormFields();
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
                    Response.Write("<script>alert('Book with ID " + TextBox1.Text.Trim() + " Deleted Successfully');</script>");
                    TextBox1.Text = "";
                    ClearFormFields();
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
            TextBox3.Text = "";           //Publish 
            TextBox9.Text = "";           //Remaining stock
            lblmessage.Text = string.Empty;
            lblmessage.Font.Bold = false;
            lblmessage.Font.Size = FontUnit.Empty;
            lblmessage.BackColor = System.Drawing.Color.Empty;
            lblmessage.ForeColor = System.Drawing.Color.Empty;

            //TextBox1.Text = "";      //it will clear the text box value on page load which shows error
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
/*    
................................try to solve by ChatGpt, *Bard ...............................................
...........................................................Current............................................
file uplaod function have to read from youtube and apply code for image size and image type also practicre on design and running code on new system from git and pen drive.(https://www.youtube.com/watch?v=irF6Zomjxwc)

#file uploading is over writing existing file. means if a diffrent user uplads a file with same name it will overwrite the existing file with same name

#if extention is not matching it is showing error on lalbel but not halting executon of progrrame.



1.)if any genere is not selected AddNewBookFunction not working.
{
sol0l:    Without gerene let Nonquery executed.
Sol02:    Prompt User to Enter Genere. and recall and call.
Sol03:    Javascript code no.
Answer:   Solved by ChatGPT Code.(if (!string.IsNullOrEmpty(genres)))
}

2.)If any file path selected code is breaking from that point.
{
possible reasons;
1.) wrong format of file selected.
2.) some error in code seqencing.
Answer:Solved By ChatGpt by just changing the sequence of code.

}
3.)there should be any jscript code to check book id format at the time of updating or adding the book id.
{
sol:1 Prompt user to enter the id in specified format.
Sol:2 genrate book id autometically.


4.)Problem in page load event in get book by ID function. 
{
genere not refreahing on executing getBookbyID Function second time, means previous selected values remains with the new selected value aasociated with genere current book id.
}
.....................................................Current.......................................................

1.) Current_Stock text box is missplaced with wrong textbox.
2.)
3.)

..........................................Issue Still Pending ..........................................................
{
on refreshing the page value in textbox1 remains and execute the get book by id function autometically 


}
*

--------------------------------------------------------NOTES--------------------------------------------------------

 ***The "" + construct is a way to explicitly force the numerical expression to be treated as a string.
         When a string is combined with a non-string value using +, the non-string value is automatically converted to a string.
The final result of the expression "" + (arithmetic expression) is a string that represents the numerical value of the arithmetic expression.**bard  https://g.co/bard/share/c4d7cc9d42ad   */
/*Issues still need to be resolved
 1.
** issues still need to resolve: on refreshing the page textbox1 value remain on page and values in other textbox still remain on page when it should be clen on rfreshing or remain is also the option but logic have to clear behind the code
 */

/*
  
.............................................**TroubleShootingDoneSuccessfully......................................................

{
**Pages textbox not working when text box property was number. later changed to single line worked.
*"TextBox5.Text = "" + current_stock;" could also be the solution of above problem
*On deleting the book record book id remains on PAGE after deletion..(Solved by clear field function)

 
//first error was occured, when database file name was missmatch in datbase and C# query.
//Second error was on page load funtion was clearing the book id in textbox1 at time of load and it was showing  error in textbox properties. while it was programe logic error. 
//third error was in add new book function (mentioned in addfunction ) file uplad value was null so it was creating error and i am serching error in sql string.
//fourth error text box intger value was invisible, visible on making textbox property "inline".
//fifth error genre section of code producing error: some code lines were misplaced 
//conclusion: there should be comolete map of page structure and logic in your mind for truble shooting. like complete paragraph of poetry not single line.
//textbox numbers were incorrect in SQL Query. matched with frontend page to sql query. another option to do that is fill every input box with distinct number and match in databse.

....................below File Name selction code was not working .................................

 //if the below code executed program breaks form this point 
                /* if (FileUpload1.HasFile)
                 {

                   1.)  string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                   2.)  FileUpload1.SaveAs(Server.MapPath("~/book_inventory/" + filename));
                   3.)  filepath = "~/book_inventory/" + filename;
                 }
just swaped the ABOVE 2 & 3 LINE IN MASTER CODE AS 3 THEN 2 ,, IT WORKED {{UPDATEFUNCTION}} ...............
.............................Below ChatGpt Code.code working properly IN   .................................. 
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



}*/