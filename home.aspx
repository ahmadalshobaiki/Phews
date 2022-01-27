<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Phews.Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <!-- page head -->
<head runat="server">
    <!-- META tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- bootstrap.js -->
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>

    <!-- animate.css -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css"/>

    <!-- bootstrap.css -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <!-- embedded stylesheet -->
    <style>
        a.logout:hover {
            background-color: crimson;
            color: white;
        }
         .navbar-nav .nav-item.active > .nav-link { 
            color: #FFE599;
            font-weight:bold;
            background-color: transparent;  
        }
    </style>

    <!-- page title -->
    <title>Home</title>
</head>

    <!-- page body -->
<body>
    <form id="form1" runat="server">
        
        <div class="container-fluid">

            <!-- navbar -->
        <nav class="navbar navbar-expand-md navbar-dark fixed-top" style="background-color:#085394; ">

            <!-- navbar brand -->
            <a href="#" class="navbar-brand" style="font-size:xx-large; font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color:#FFE599;"><img src="img/eercast2.png" height="60" width="60" />PHEWS</a>

            <!-- navbar toggle button -->
            <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#new_target"><span class="navbar-toggler-icon"></span></button>

            <!-- navbar navigation links -->
            <div id="new_target" class="collapse navbar-collapse">
                <ul class="navbar-nav">
                    <li class="nav-item active"><a class="nav-link " href="#">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">About Us</a></li>
                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" data-toggle="dropdown" data-target="news_dropdown" href="#">News<span class="caret"></span></a>

                        <div class="dropdown-menu" aria-labelledby="news_dropdown">
                            <a href="#" class="dropdown-item">All</a>
                            <a href="#" class="dropdown-item">Sports</a>
                            <a href="#" class="dropdown-item">Movies</a>
                        </div>
                    </li>
                    <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" data-toggle="dropdown" data-target="photos_dropdown" href="#">Photos<span class="caret"></span></a>

                        <div class="dropdown-menu" aria-labelledby="photos_dropdown">
                            <a href="#" class="dropdown-item">All</a>
                            <a href="#" class="dropdown-item">Mountains</a>
                            <a href="#" class="dropdown-item">oceans</a>
                        </div>
                    </li>
                </ul>

                <!-- navbar profile page link -->
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item dropdown ">
                            <a href="#" class="dropdown-toggle nav-link" data-toggle="dropdown" data-target="profile_dropdown">
                                <img class="rounded-circle" height="50" width="50" src="img/04-nature_721703848.jpg" />
                                <span class="caret"></span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right" aria-labelledby="profile_dropdown">
                                <a href="myProfile.aspx" class="dropdown-item">View Profile</a>
                                <div >
                                    <a href="#" class="dropdown-item logout">Log out</a>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            
        </nav>

        <!---------------------------------------------------------- navbar end --------------------------------------------------------->
             
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
            </div>
    </form>
</body>
</html>