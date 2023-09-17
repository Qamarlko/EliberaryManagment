<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminauthormanagement.aspx.cs" Inherits="WebApplication3.adminauthormanagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Auther Details</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/writer.png" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Auther ID</label>
                                <div class="form-group">
                                    <%-- Input Group class in BS is used to merge the input text box with Input button  --%>
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button class="btn btn-primary" ID="Button2" runat="server" Text="Go" />
                                    </div>
                                    <%-- text box and button are cuppled in one input group entity  button front test is directly proptional to the test area. if text is more the allotted grid area button will down to new line as block element  --%>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <label>Auther Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Author Name" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <%-- btn-block bootstrap class will make button to the full size of parent div otherwiae it will take some margin inside parent div --%>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-success" ID="Button4" runat="server" class="btn btn-primary btn-block btn-lg" Text="Add" />
                            </div>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-warning" ID="Button3" runat="server" class="btn btn-primary btn-block btn-lg" Text="Update" />
                            </div>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-danger" ID="Button5" runat="server" class="btn btn-primary btn-block btn-lg" Text="Delete" />
                            </div>

                        </div>

                    </div>



                </div>


                <a href="homepage.aspx"><< Back to Home</a><br />
                <br />
            </div>
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Auther List</h4>
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
                                <asp:GridView class="table table-striped table-bordered" ID ="GridView2" runat="server">
                                </asp:GridView>

                            </div>
                        </div>


                    </div>

                </div>


            </div>

        </div>

    </div>
</asp:Content>
