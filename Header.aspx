<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="Phews.Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        a.logout:hover {
            background-color: crimson;
            color: white;
        }

        .navbar-nav .nav-item.active > .nav-link {
            color: #FFE599;
            font-weight: bold;
            background-color: transparent;
        }
    </style>
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container-fluid">
            <!-- ------------------------------------Navigation bar start------------------------------------ -->
            <nav class="navbar navbar-expand-md navbar-dark fixed-top" style="background-color: #085394;">
                <!-- ------------------------------------Brand name and logo------------------------------------ -->
                <a href="#" class="navbar-brand" style="font-size: xx-large; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color: #FFE599;">
                    <img src="img/eercast2.png" height="60" width="60" />PHEWS</a>
                <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#new_target"><span class="navbar-toggler-icon"></span></button>
                <!-- ------------------------------------Menu List------------------------------------ -->
                <div id="new_target" class="collapse navbar-collapse">
                    <ul class="navbar-nav">
                        <li class="nav-item active"><a class="nav-link " href="#">Home</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">About Us</a></li>
                        <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" data-toggle="dropdown" data-target="news_dropdown" href="#">News<span class="caret"></span></a>
                            <!-- ------------------------------------News Dropdown List------------------------------------ -->
                            <div class="dropdown-menu" aria-labelledby="news_dropdown">
                                <a href="#" class="dropdown-item">All</a>
                                <a href="#" class="dropdown-item">Sports</a>
                                <a href="#" class="dropdown-item">Movies</a>
                            </div>
                        </li>
                        <li class="nav-item dropdown"><a class="nav-link dropdown-toggle" data-toggle="dropdown" data-target="photos_dropdown" href="#">Photos<span class="caret"></span></a>
                            <!-- ------------------------------------Photos Dropdown List------------------------------------ -->
                            <div class="dropdown-menu" aria-labelledby="photos_dropdown">
                                <a href="#" class="dropdown-item">All</a>
                                <a href="#" class="dropdown-item">Mountains</a>
                                <a href="#" class="dropdown-item">oceans</a>
                            </div>
                        </li>
                    </ul>
                    <!-- ------------------------------------Profile and logout Dropdown List------------------------------------ -->
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item dropdown ">
                            <a href="#" class="dropdown-toggle nav-link" data-toggle="dropdown" data-target="profile_dropdown">
                                <img class="rounded-circle" height="50" width="50" src="img/04-nature_721703848.jpg" />
                                <span class="caret"></span>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right" aria-labelledby="profile_dropdown">
                                <a href="#" class="dropdown-item">View Profile</a>
                                <div>
                                    <a href="#" class="dropdown-item logout">Log out</a>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <!-- ------------------------------------Navigation bar end------------------------------------ -->
            </nav>
        </div>
    </form>
</body>
</html>