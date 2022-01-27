<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Manage_Albums.aspx.cs" Inherits="Phews.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script>

        var object = { status: false, ele: null };
        function confirmDelete(ev) {

            if (object.status) { return true; };
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Album!",
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
    </script>
    <title>Manage Albums</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 130px">
            <!-- ------------------------------------- Empty col --------------------------------- -->
            <div class="col-md-2"></div>
            <!-- ------------------ middle col ------------------------------- -->
            <div class="col-md-8" style="margin-top: 40px">
                <!-- ------------------------------------------------ search and filter row ----------------------------------- -->
                <div class="container-fluid">
                    <div class="row">

                        <!-- ------------------------------------------------ search ----------------------------------- -->
                        <div class="col-md-5">
                            <nav class="navbar navbar-light bg-light form-inline ">
                                <asp:TextBox class="form-control mr-sm-2" ID="txtSearch" placeholder="Search" aria-label="Search" runat="server"></asp:TextBox>
                                <asp:Button Text="Search" ID="btnSearch" runat="server" class="btn btn-outline-primary my-2 my-sm-0" OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button Text="clear" ID="btnClearSearch" runat="server" class="btn btn-outline-secondary my-2 my-sm-0" OnClick="btnClearSearch_Click"></asp:Button>
                            </nav>
                        </div>

                        <div class="col-md-5">
                            <label runat="server" visible="false" style="color: red;" id="lblDeleteErr"></label>
                        </div>

                        <div class="col-md-2">
                            <a href="Albums.aspx" target="_blank" class="btn btn-primary mt-2" style="color: white">Preview</a>
                        </div>
                    </div>
                </div>

                <div class="row mt-md-1">
                    <div class="container">

                        <asp:GridView
                            CssClass="table table-striped"
                            runat="server"
                            ID="gvManageAlbums"
                            AutoGenerateColumns="false"
                            OnRowDeleting="gvManageAlbums_RowDeleting"
                            DataKeyNames="ID"
                            AllowPaging="true"
                            AllowSorting="true"
                            PageSize="5"
                            OnPageIndexChanging="OnPaging"
                            ShowHeaderWhenEmpty="true"
                            EmptyDataText="No Records Found!"
                            OnRowEditing="gvManageAlbums_RowEditing">

                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                <asp:BoundField HeaderText="Title" DataField="Title" />
                                <asp:BoundField HeaderText="Description" DataField="Description" />
                                
                                 <asp:TemplateField HeaderText="Thumbnail">
                                <ItemTemplate>
                                 <img src="img/<%# Eval("Thumbnail") %>" width="50" height="50" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField HeaderText="Poster Name" DataField="Username" />

                                <asp:TemplateField HeaderText="Publish Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPublishDate" runat="server" CssClass="text-nowrap" Text='<%# Eval("Publish_Date" , "{0: dd/MM/yyyy}") %>' />
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

                        <div class="modal fade" id="deleteAlbumModal" tabindex="-1" role="dialog" aria-labelledby="deleteAlbumModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="deleteAlbumModalLabel">Delete Album</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        Are you sure you want to Delete this Album?
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                                        <button type="button" class="btn btn-primary">Yes</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ----------------------------------------- table ends ---------------------------------- -->
                </div>
                <!-- ----------------------------------------- table row ends ---------------------------------- -->
                <div class="row mt-md-3">
                    <div class="col-md-12 text-center">
                        <asp:Button runat="server" Text="Add Album" ID="btnAddNewAlbum" class="btn btn-primary" OnClick="btnAddNewAlbum_Click" />
                    </div>
                </div>
                <!-- ------------------------------------ Page numbers ------------------------------------ -->
                <!--   <div class="row mt-md-3">
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
                    </div>    -->
            </div>

            <div class="col-md-2">
            </div>
        </div>
    </div>
</asp:Content>
