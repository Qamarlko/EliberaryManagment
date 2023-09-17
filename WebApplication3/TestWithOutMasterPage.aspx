<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWithOutMasterPage.aspx.cs" Inherits="WebApplication3.TestWithOutMasterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <!-- bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@100;300;400;500;900&family=Ubuntu:wght@300;400;700&display=swap" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />



    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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

    </form>
</body>
</html>
