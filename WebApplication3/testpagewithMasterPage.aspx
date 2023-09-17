﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="testpagewithMasterPage.aspx.cs" Inherits="WebApplication3.testpagewithMasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div>

            <nav class="navbar navbar-expand-lg navbar-light">
                <a>
                    <img src="imgs/books.png" width="30" height="30" />
                    <b>E-Liberary</b>
                </a>

                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>


                <%-- Navbar from Simple snippest --%>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item active">
                            <a class="nav-link" href="homepage.aspx">Home <span class="sr-only">(current)</span></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Features</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Pricing</a>
                        </li>
                    </ul>

                    <ul class="navbar-nav">
                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton4" runat="server">View Books</asp:LinkButton>
                        </li>
                        

                 <%-- Right side Navbar  --%>


                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">User Login</asp:LinkButton>
                        </li>
                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Sign Up</asp:LinkButton>
                        </li>
                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Visible="False">Logout</asp:LinkButton>
                        </li>

                        <li class="nav-item active">
                            <asp:LinkButton class="nav-link" ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" Visible="False">Hello user</asp:LinkButton>

                        </li>
                    </ul>



                </div>
            </nav>






        </div>


        <%-- Main Content Placeholder --%>
        <div>
            <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server">
            </asp:ContentPlaceHolder>

        </div>
 

        <%-- Main Content Placeholder --%>

        <!-- Footer -->
         <footer>
            <div id="footer1" class="container-fluid">
             <div class="row">

                    <div class="col-xs-12 col-sm-12 col-md-12  text-center">
   <%--         <div class="col col-md-5 col-lg-5 col-12 order-1"> --%>
                        <p>
                            <asp:LinkButton class="footerlinks" ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Admin Login</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton class="footerlinks" ID="LinkButton11" runat="server" OnClick="LinkButton11_Click" Visible="False">Author Management</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton class="footerlinks" ID="LinkButton12" runat="server" OnClick="LinkButton12_Click" Visible="False">Publisher Management</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton class="footerlinks" ID="LinkButton8" runat="server" Visible="False" OnClick="LinkButton8_Click">Book Inventory</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton class="footerlinks" ID="LinkButton9" runat="server" Visible="False" OnClick="LinkButton9_Click">Book Issuing</asp:LinkButton>
                            &nbsp;
                             <asp:LinkButton class="footerlinks" ID="LinkButton10" runat="server" Visible="False" OnClick="LinkButton10_Click">Member Management</asp:LinkButton>
                        </p>

                    </div>

                </div>
            </div>

            <div id="footer2" class="container-fluid">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-center">
                        <p style="color:whitesmoke">&copy All right Reversed. <a class="footerlinks" href="#" target="_blank">Simple Snippets</a></p>
                    </div>
                </div>
            </div>

        </footer>
        <!-- ./Footer -->

</asp:Content>
