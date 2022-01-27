<%@ Page Title="" Language="C#" MasterPageFile="~/HomePageHeader.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="Phews.WebForm3" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title>Home</title>

</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- home page content -->
             <div class="row" style="margin-top:130px">
        <div class="col-md-4 justify-content-center align-self-center ">
            
            <h2 class="animate__animated animate__fadeInLeft animate__delay-1s" style="text-align:center;"><span style="white-space: nowrap">Out of the Loop?</span></h2>
            <p class="lead animate__animated animate__fadeInRight animate__delay-1s" style="text-align:center; "> keep you up to date with the latest news submitted by our devoted team</p>
            <p class="text-center animate__animated animate__fadeInLeft animate__delay-1s"><button style="background-color:#085394;  color:white" type="button" class="btn">Go to News</button></p>
          
        </div>
         <div class="col-md-4">
             <img class="img-fluid animate__animated animate__fadeInDown "  src="img/artworks-000015086198-nqimp4-t500x500.jpg"/>
        </div>
         <div class="col-md-4 justify-content-center align-self-center ">
            
            <h2 class="animate__animated animate__fadeInRight animate__delay-1s" style="text-align:center;"><span style="white-space: nowrap">Discover</span></h2>
            <p class="lead animate__animated animate__fadeInLeft animate__delay-1s" style="text-align:center;">Feast your eyes and browse our collection of countless photos </p>
            <p class="text-center animate__animated animate__fadeInRight animate__delay-1s"><button style="background-color:#085394; color:white" type="button" class="btn">See Photo Gallery</button></p>
          
        </div>
                 </div>

</asp:Content>
