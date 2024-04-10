<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookinventory.aspx.cs" Inherits="WebApplication3.adminbookinventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Book Detail</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/books.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">

                                

                                <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />

                            </div>
                            <div class="col">
                                <asp:Label ID="lblmessage" runat="server" Text="Label for File Status" ClientIDMode="AutoID"></asp:Label>
                                </div>



                        </div>


                        <div class="row">
                            <div class="col-md-3">
                                <label>Book ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Member ID"></asp:TextBox>
                                        <asp:LinkButton class="btn btn-primary" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><i class="fas fa-check-circle"> </i></asp:LinkButton>
                                    </div>

                                </div>
                            </div>


                            <div class="col-md-9">
                                <label>Book Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Book Name" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-md-4">
                                <label>Language</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList3" runat="server">

                                        <asp:ListItem Text="Language" Value="" />
                                        <asp:ListItem Text="English" Value="English" />
                                        <asp:ListItem Text="Hindi" Value="Hindi" />
                                        <asp:ListItem Text="Marathi" Value="Marathi" />
                                        <asp:ListItem Text="French" Value="French" />
                                        <asp:ListItem Text="German" Value="German" />
                                        <asp:ListItem Text="Urdu" Value="Urdu" />

                                    </asp:DropDownList>

                                </div>

                                <label>Publisher Name</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList2" runat="server">

                                        <asp:ListItem Text="Publisher Name" Value="" />
                                        <asp:ListItem Text="Ravi" Value="Ravi" />
                                        <asp:ListItem Text="Shyam" Value="Shyam" />
                                        <asp:ListItem Text="Meera" Value="Meera" />
                                        <asp:ListItem Text="Geeta" Value="Geeta" />
                                        <asp:ListItem Text="Sita" Value="Sita" />
                                        <asp:ListItem Text="Rita" Value="Rita" />

                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <label>Author Name</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">

                                                                               
                                        <asp:ListItem Text="Author Name" Value="" />
                                        <asp:ListItem Text="Tom" Value="Tom" />
                                        <asp:ListItem Text="Honey" Value="Honey" />
                                        <asp:ListItem Text="Johan" Value="Johan" />
                                        <asp:ListItem Text="Michal" Value="Michal" />
                                        <asp:ListItem Text="Harry" Value="Harry" />
                                        <asp:ListItem Text="Mona" Value="Mona" />
                                    </asp:DropDownList>

                                </div>

                                <label>Publish Date </label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Book Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Genre</label>
                                <div class="form-group">
                                    <asp:ListBox CssClass="form-control" ID="ListBox1" runat="server" SelectionMode="Multiple" Rows="5">
                                        <asp:ListItem Text="Genre" Value="" />
                                        <asp:ListItem Text="Action" Value="Action" />
                                        <asp:ListItem Text="Adventure" Value="Adventure" />
                                        <asp:ListItem Text="Comic Book" Value="Comic Book" />
                                        <asp:ListItem Text="Self Help" Value="Self Help" />
                                        <asp:ListItem Text="Motivation" Value="Motivation" />
                                        <asp:ListItem Text="Healthy Living" Value="Healthy Living" />
                                        <asp:ListItem Text="Wellness" Value="Wellness" />
                                        <asp:ListItem Text="Crime" Value="Crime" />
                                        <asp:ListItem Text="Drama" Value="Drama" />
                                        <asp:ListItem Text="Fantasy" Value="Fantasy" />
                                        <asp:ListItem Text="Horror" Value="Horror" />
                                        <asp:ListItem Text="Poetry" Value="Poetry" />
                                        <asp:ListItem Text="Personal Development" Value="Personal Development" />
                                        <asp:ListItem Text="Romance" Value="Romance" />
                                        <asp:ListItem Text="Science Fiction" Value="Science Fiction" />
                                        <asp:ListItem Text="Suspense" Value="Suspense" />
                                        <asp:ListItem Text="Thriller" Value="Thriller" />
                                        <asp:ListItem Text="Art" Value="Art" />
                                        <asp:ListItem Text="Autobiography" Value="Autobiography" />
                                        <asp:ListItem Text="Encyclopedia" Value="Encyclopedia" />
                                        <asp:ListItem Text="Health" Value="Health" />
                                        <asp:ListItem Text="History" Value="History" />
                                        <asp:ListItem Text="Math" Value="Math" />
                                        <asp:ListItem Text="Textbook" Value="Textbook" />
                                        <asp:ListItem Text="Science" Value="Science" />
                                        <asp:ListItem Text="Travel" Value="Travel" />
                                    </asp:ListBox>



                                </div>





                            </div>

                        </div>



                        <div class="row">
                            <div class="col-md-4">

                                <label>Edition</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Edition" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-4">

                                <label>BookCost(PerUnit)</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="BookCost(PerUnit)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">

                                <label>Pages</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Book Name"></asp:TextBox>
                                </div>
                            </div>





                        </div>




                        <div class="row">
                            <div class="col-md-4">

                                <label>Actual Stock</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Actual Stock" EnableViewState="false">Actual Stock</asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-4">

                                <label>Current Stock</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">

                                <label>Issued Books</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Pages" TextMode="SingleLine" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>



                        </div>



                        <div class="row">
                            <div class="col-12">
                                <label>Book Discription</label>
                                <div class="form-group">

                                    <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Book Discription" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>



                        </div>




                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="Button4" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button4_Click" />
                            </div>

                            <div class="col-4">
                                <asp:Button ID="Button1" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button1_Click" />
                            </div>
                            <div class="col-4">

                                <asp:Button ID="Button2" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button2_Click" />
                            </div>

                        </div>




                    </div>


                </div>

                <a href="homepage.aspx"><< Back to Home</a><br>
                <br>

                <div class="row">
                    <div class="col-md-7">
                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
