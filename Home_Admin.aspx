<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Home_Admin.aspx.cs" Inherits="Phews.WebForm10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row pl-md-5" style="margin-top: 130px">
            <h1 class="display-3">My Dashboard</h1>
        </div>
    </div>

    <!-- ------------------------------------ Photos ------------------------------------ -->
    <div class="container">
        <!-- ------------------------------------ Container Start ------------------------------------ -->
        <div class="row" style="margin-top: 130px">
            <!-- ------------------------------------ Row Start ------------------------------------ -->
            <div class="col-md-4 my-md-3">
                <div class="card">

                    <div class="card-body">
                        <h4 class="card-title">You published</h4>
                        <p runat="server" id="pArticlesPublished" class="card-text"></p>
                    </div>
                </div>
            </div>

            <div class="col-md-4 my-md-3">
                <div class="card">

                    <div class="card-body">
                        <h4 class="card-title">You published</h4>
                        <p runat="server" id="pPhotosPublished" class="card-text"></p>
                    </div>
                </div>
            </div>

            <div class="col-md-4 my-md-3">
                <div class="card">

                    <div class="card-body">
                        <h4 class="card-title">Number of Site users</h4>
                        <p class="card-text" runat="server" id="pNumberOfUsers"></p>
                    </div>
                </div>
            </div>

            <!-- ------------------------------------ Row End ------------------------------------ -->
        </div>
        <!-- ------------------------------------ Container End ------------------------------------ -->
    </div>
</asp:Content>