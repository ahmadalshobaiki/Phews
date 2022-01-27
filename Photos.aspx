<%@ Page Title="" Language="C#" MasterPageFile="~/Phews.Master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="Phews.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a.no-style:hover {
            text-decoration: none;
        }

        .cropped1 {
            width: 100%; /* width of container */
            min-height: 200px;
            max-height: 200px;
            object-fit: cover;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/css/lightgallery-bundle.min.css" />
    <title>Album</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container-fluid">
            <div class="row pl-md-5" style="margin-top: 130px">
                <h1 class="display-3" runat="server" id="hAlbumTitle"></h1>
            </div>
             <p class="lead ml-md-5" runat="server" id="pAlbumDescription">  </p>
        </div>
        <div class="container-fluid">
            <div class="row pl-md-5">
                <!-- ------------------------------------ search bar ------------------------------------ -->
                <nav class="navbar navbar-light bg-light form-inline ">

                    <asp:TextBox ID="txtSearch" class="form-control mr-sm-2" placeholder="Search" aria-label="Search" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" Text="Search" runat="server" class="btn btn-outline-primary my-2 my-sm-0" OnClick="btnSearch_Click"></asp:Button>
                    <asp:Button Text="clear" ID="btnClearSearch" runat="server" class="btn btn-outline-secondary my-2 my-sm-0 mx-sm-2" OnClick="btnClearSearch_Click"></asp:Button>
                </nav>
            </div>
        </div>
        <!-- ------------------------------------ Photos ------------------------------------ -->

        <!-- ------------------------------------ Container Start ------------------------------------ -->
        <div class="gallery-container row ml-4" id="lightgallery">
            <!-- ------------------------------------ Row Start ------------------------------------ -->

            <asp:Repeater ID="rptrPhotos" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>

                    <!-- <img class="card-img-top" src="<%# Eval("Img_Path") %>"> -->
                    <a class="gallery-item my-2 col-3 no-style" style=" min-width:150px;" href="img/<%# Eval("Img_Path") %>" data-sub-html="<h2><%# Eval("Title") %></h2><p><strong>Posted by </strong> <%# Eval("Username") %></p>">
                        <div class="card" style="height: 100%;">
                            <img src="img/<%# Eval("Img_Path") %>" class="cropped1">
                            <div class="card-body">
                                <h4 class="card-title text-dark"><%# Eval("Title") %></h4>
                                <p class="card-text text-dark"><span>Posted By: <strong><%# Eval("Username") %></strong> </span>| <span>Posted On: <strong class="text-nowrap"><%# Eval("Posted_Date", "{0: dd/MM/yyyy}") %></strong></span>  </p>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>

          

            <!-- ------------------------------------ Row End ------------------------------------ -->
        </div>
        <div class="row justify-content-center my-3">
            <table style="width: 600px;">
                <tr>
                    <td>
                        <asp:LinkButton ID="lbFirst" runat="server"
                            OnClick="lbFirst_Click" CssClass="btn btn-link border">First</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbPrevious" runat="server"
                            OnClick="lbPrevious_Click" CssClass="btn btn-link border">Previous</asp:LinkButton>
                    </td>
                    <td>
                        <asp:DataList ID="rptPaging" runat="server"
                            OnItemCommand="rptPaging_ItemCommand"
                            OnItemDataBound="rptPaging_ItemDataBound"
                            RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbPaging" runat="server"
                                    CommandArgument='<%# Eval("PageIndex") %>'
                                    CommandName="newPage"
                                    Text='<%# Eval("PageText") %> ' CssClass="btn btn-link border">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbNext" runat="server"
                            OnClick="lbNext_Click" CssClass="btn btn-link border">Next</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbLast" runat="server"
                            OnClick="lbLast_Click" CssClass="btn btn-link border">Last</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label CssClass="text-nowrap" ID="lblpage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!-- ------------------------------------ Container End ------------------------------------ -->

 
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/lightgallery.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/plugins/fullscreen/lg-fullscreen.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/plugins/autoplay/lg-autoplay.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/plugins/zoom/lg-zoom.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightgallery/2.1.8/plugins/thumbnail/lg-thumbnail.umd.min.js"></script>
    <script type="text/javascript">
       
        lightGallery(document.getElementById('lightgallery'), {
            plugins: [lgZoom, lgFullscreen, lgAutoplay],
            speed: 500,
            showZoomInOutIcons: true,
            actualSize: false,
            animateThumb: false,
            zoomFromOrigin: false,
            allowMediaOverlap: true,
            toggleThumb: true,
        });
    </script>
</asp:Content>