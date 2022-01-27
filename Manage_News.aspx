<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Manage_News.aspx.cs" Inherits="Phews.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script>

        var object = { status: false, ele: null };
        function confirmDelete(ev) {

            if (object.status) { return true; };
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this News Article!",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, delete it!",
                closeOnConfirm: true
            },
                function () {

                    object.status = true;
                    object.ele = ev;
                    object.ele.click();

                });
            return false;
        }
        function confirmDeleteAll(ev) {
            if (<%= deletedRecords  %> <= 0) {

            } else {
               
                if (object.status) { return true; };
                swal({
                    title: "Are you sure?",
                    text: "You are about to delete <%= deletedRecords  %> Articles \nYou will not be able to recover these Articles!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Yes, delete!",
                    closeOnConfirm: true
                },
                    function () {

                        object.status = true;
                        object.ele = ev;
                        object.ele.click();

                    });

                return false;
            }



        }
    </script>
    <title>Manage News</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 130px">
        <!-- ------------------------------------- Empty col --------------------------------- -->
        <div class="col-md-2"></div>
        <!-- ------------------ middle col ------------------------------- -->
        <div class="col-md-8" style="margin-top: 10px">
            <!-- ------------------------------------------------ search and filter row ----------------------------------- -->
            <div class="container-fluid">
                <div class="row">

                    <!-- ------------------------------------------------ search ----------------------------------- -->
                    <div class="col-md-5">
                        <nav class="navbar navbar-light bg-light form-inline ">
                            <asp:TextBox ID="txtSearch" class="form-control mr-sm-2" placeholder="Search" aria-label="Search" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" Text="Search" runat="server" class="btn btn-outline-primary my-2 my-sm-0" type="button" OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button Text="clear filters" ID="btnClearSearch" runat="server" class="btn btn-outline-secondary my-2 my-sm-0" OnClick="btnClearSearch_Click"></asp:Button>
                        </nav>
                    </div>

                    <!-- ------------------------------------------------  filter  ----------------------------------- -->
                    <div class="col-md-3" >
                        <div class="container">
                            <asp:DropDownList CssClass="dropdown mt-3 ml-2" runat="server" AppendDataBoundItems="true" DataTextField="Name" ID="ddlCategory" DataValueField="ID" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">category</asp:ListItem>
                            </asp:DropDownList>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-funnel-fill se mt-2" viewBox="0 0 16 16">
                                <path d="M1.5 1.5A.5.5 0 0 1 2 1h12a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-.128.334L10 8.692V13.5a.5.5 0 0 1-.342.474l-3 1A.5.5 0 0 1 6 14.5V8.692L1.628 3.834A.5.5 0 0 1 1.5 3.5v-2z" />
                            </svg>
                        </div>
                     
                    </div>
                    <!-- ---------------------------------------- preview and delete selected -------------------------------- -->
                    <div class="col-md-4 text-center">
                        <asp:Button Text="Delete selcted" ID="btnDeleteSelected" runat="server" class="btn btn-outline-danger my-2 ml-3" OnClick="btnDeleteSelected_Click" OnClientClick="return confirmDeleteAll(this)"></asp:Button>
                        <a href="News.aspx" target="_blank" class="btn btn-primary my-2 ml-1" style="color: white">Preview</a>
                    </div>
                </div>
            </div>
            <!-- ------------------------------------------- table row ---------------------------------------------- -->
            <div class="row mt-md-1">
                <div class="container">

                  
                    <asp:GridView
                        CssClass="table table-striped"
                        runat="server"
                        ID="gvManageNews"
                        AutoGenerateColumns="false"
                        OnRowDeleting="gvManageNews_RowDeleting"
                        DataKeyNames="ID"
                        AllowPaging="true"
                        AllowSorting="true"
                        PageSize="20"
                        OnPageIndexChanging="OnPaging"
                        ShowHeaderWhenEmpty="true"
                        EmptyDataText="No Records Found!"
                        OnRowEditing="gvManageNews_RowEditing">

                        <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="cbSelectAll" AutoPostBack="true" OnCheckedChanged="cbSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbSelect" AutoPostBack="true" OnCheckedChanged="cbSelect_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ID" DataField="ID" />
                            <asp:BoundField HeaderText="Title" DataField="Title" />
                            <asp:BoundField HeaderText="Subtitle" DataField="Subtitle" />
                            <asp:BoundField HeaderText="Category" DataField="Name" />
                          
                              <asp:TemplateField HeaderText="Thumbnail">
                                <ItemTemplate>
                                 <img src="img/<%# Eval("Img_Path") %>" width="50" height="50" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Poster" DataField="Username" />
                              <asp:TemplateField HeaderText="Publish Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDate" CssClass="text-nowrap" runat="server" Text='<%# Eval("Posted_Date"  , "{0: dd/MM/yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Edit" CssClass="btn btn-primary" Text="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirmDelete(this);"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!-- ----------------------------------------- table row ends ---------------------------------- -->
            <div class="row mt-md-3">
                <!-- ---------------------------------- Add Photo  ---------------------------------- -->
                <div class="col-md-12 text-center">
                    <a href="newArticle.aspx" class="btn btn-primary">Add Article</a>
                </div>
            </div>
            <!-- ------------------------------------ Page numbers ------------------------------------ -->
            <!-- <div class="row mt-md-3">
                <div class="col-md-12">
                    <nav aria-label="Standard pagination">
                        <ul class="pagination justify-content-center">
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Previous">
                                    <span aria-hidden="true">«</span>
                                </a>
                            </li>
                            <li class="page-item"><a class="page-link" href="#">1</a></li>
                            <li class="page-item"><a class="page-link" href="#">2</a></li>
                            <li class="page-item"><a class="page-link" href="#">3</a></li>
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Next">
                                    <span aria-hidden="true">»</span>
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div> -->
        </div>

        <div class="col-md-2">
        </div>
    </div>
</asp:Content>