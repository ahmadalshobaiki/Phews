<%@ Page Title="" Language="C#" MasterPageFile="~/Phews.Master" AutoEventWireup="true" CodeBehind="Home_General.aspx.cs" Inherits="Phews.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 130px">
            <!-- ------------------------------------ left paragraph ------------------------------------ -->
            <div class="col-md-4 justify-content-center align-self-center pl-md-5">

                <h2 class="animate__animated animate__fadeInLeft animate__delay-1s" style="text-align: center;"><span style="white-space: nowrap">Out of the Loop?</span></h2>
                <p class="lead animate__animated animate__fadeInRight animate__delay-1s" style="text-align: center;">keep you up to date with the latest news submitted by our devoted team</p>
                <p class="text-center animate__animated animate__fadeInLeft animate__delay-1s" id="pNews" runat="server"><a href="news.aspx" class="btn" style="background-color: #085394; color: white" id="lnkNews" runat="server">Go to News</a></p>
                
            </div>
            <!-- ------------------------------------ Center image ------------------------------------ -->
            <div class="col-md-4">
                <img class="img-fluid animate__animated animate__fadeInDown " src="img/artworks-000015086198-nqimp4-t500x500.jpg" />
            </div>
            <!-- ------------------------------------ Right paragraph ------------------------------------ -->
            <div class="col-md-4 justify-content-center align-self-center pr-md-5">

                <h2 class="animate__animated animate__fadeInRight animate__delay-1s" style="text-align: center;"><span style="white-space: nowrap">Discover</span></h2>
                <p class="lead animate__animated animate__fadeInLeft animate__delay-1s" style="text-align: center;">Feast your eyes and browse our collection of countless photos </p>
                <p class="text-center animate__animated animate__fadeInRight animate__delay-1s"  id="pPhotos" runat="server"><a href="Albums.aspx" class="btn" style="background-color: #085394; color: white" id="lnkPhotos" runat="server">See Photo Gallery</a></p>
            </div>
        </div>
    </div>
</asp:Content>