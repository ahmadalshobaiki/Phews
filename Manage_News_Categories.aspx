<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Manage_News_Categories.aspx.cs" Inherits="Phews.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script>

        var object = { status: false, ele: null };
        function confirmDelete(ev) {

            if (object.status) { return true; };
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Category!",
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
    <script type="text/javascript">

        function openModal() {
            $('#addNewsCategoryModal').modal({ show: true });
        }
    </script>
    <title>Manage News Categories</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                        <label runat="server" visible="false" style="color: red;" id="lblcategoryNameErr">The category Name already exists</label>
                        <label runat="server" visible="false" style="color: red;" id="lblDeleteErr"></label>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>
            </div>

            <div class="row mt-md-1">
                <div class="container">
                   
                    <asp:GridView
                        CssClass="table table-striped"
                        runat="server"
                        ID="gvManageNewsCategories"
                        AutoGenerateColumns="false"
                        OnRowDeleting="gvManageNewsCategories_RowDeleting"
                        DataKeyNames="ID"
                        AllowPaging="true"
                        AllowSorting="true"
                        PageSize="5"
                        OnPageIndexChanging="OnPaging"
                        ShowHeaderWhenEmpty="true"
                        EmptyDataText="No Records Found!"
                        OnRowEditing="gvManageNewsCategories_RowEditing"
                        OnRowCancelingEdit="gvManageNewsCategories_RowCancelingEditing"
                        OnRowUpdating="gvManageNewsCategories_RowUpdating">

                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Edit" CssClass="btn btn-primary" Text="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button runat="server" CommandName="Update" CssClass="btn btn-primary mb-sm-2" Text="Update" />
                                    <asp:Button runat="server" CommandName="Cancel" CssClass="btn btn-secondary mb-sm-2" Text="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirmDelete(this);"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="modal fade" id="deleteNewsCategoryModal" tabindex="-1" role="dialog" aria-labelledby="deleteNewsCategoryModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="deleteNewsCategoryModalLabel">Delete Category</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    Are you sure you want to Delete this Category?
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
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addNewsCategoryModal">Add Category</button>

                    <div class="modal" id="addNewsCategoryModal" tabindex="-1" role="dialog" aria-labelledby="addNewsCategoryModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="addNewsCategoryModalLabel">Add category</h5>
                                </div>
                                <div class="modal-body">
                                    <div>
                                        <div class="form-group">
                                            <label for="news-category-name" class="col-form-label">Category name:</label>
                                            <asp:TextBox runat="server" class="form-control" ID="txtNewCategory" />
                                            <label runat="server" visible="false" style="color: red;" id="lblcategoryAddNameErr"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnClose" runat="server" CausesValidation="false" OnClick="btnClose_Click" class="btn btn-secondary" Text="Close"></asp:Button>
                                    <asp:Button runat="server" data-backdrop="static" Text="Add category" ID="btnAddNewCategory" class="btn btn-primary" OnClick="btnAddNewCategory_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
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
</asp:Content>