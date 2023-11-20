<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="publisher.aspx.cs" Inherits="WebApplication3.imgs.publisher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

                $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable()
        });

    </script> 
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
                                    <h4>Publisher Details</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/publisher.png" />
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
                                <label>Publisher ID</label>
                                <div class="form-group">
                                    <%-- Input Group class in BootStrap is used to merge the input text box with Input button  --%>
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button class="btn btn-primary" ID="Button2" runat="server" Text="Go" OnClick="Button2_Click" />
                                    </div>
                                    <%-- text box and button are cuppled in one input group entity  button front test is directly proptional to the test area. if text is more the allotted grid area button will down to new line as block element  --%>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <label>Publisher Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Publisher Name" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <%-- btn-block bootstrap class will make button to the full size of parent div otherwiae it will take some margin inside parent div --%>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-success" ID="Button4" runat="server" class="btn btn-primary btn-block btn-lg" Text="Add" OnClick="Button4_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-warning" ID="Button3" runat="server" class="btn btn-primary btn-block btn-lg" Text="Update" OnClick="Button3_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button CssClass="btn btn-lg btn-block btn-danger" ID="Button5" runat="server" class="btn btn-primary btn-block btn-lg" Text="Delete" OnClick="Button5_Click" />
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
                                    <h4>Publisher List</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:elibraryDBConnectionString %>" SelectCommand="SELECT * FROM [publisher_master_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID ="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="publisher_id" DataSourceID="SqlDataSource1">
                                     
                                     <Columns>
                                        <asp:BoundField DataField="publisher_id" HeaderText="publisher_id" ReadOnly="True" SortExpression="author_id" />
                                        <asp:BoundField DataField="publisher_name" HeaderText="publisher_name" SortExpression="author_name" />
                                    </Columns>
                               
                                </asp:GridView>

                            </div>
                        </div>


                    </div>

                </div>


            </div>
        </div>
    </div>


</asp:Content>
