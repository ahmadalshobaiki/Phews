<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Manage_Users.aspx.cs" Inherits="Phews.WebForm11" %>

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
    <title>Manage Users</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 130px">
        <!-- ------------------------------------- Empty col --------------------------------- -->
        <div class="col-md-2"></div>
        <!-- ------------------ middle col ------------------------------- -->
        <div class="col-md-8" style="margin-top: 10px">
            <!-- ------------------------------------------------ search and preview row ----------------------------------- -->
            <div class="container-fluid">
                <div class="row">
                    <!-- ------------------------------------------------ search ----------------------------------- -->
                    <div class="col-md-5">
                        <nav class="navbar navbar-light bg-light form-inline ">
                            <asp:TextBox ID="txtSearch" class="form-control mr-sm-2" placeholder="Search" aria-label="Search" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" Text="Search" runat="server" class="btn btn-outline-primary my-2 my-sm-0" type="button"></asp:Button>
                            <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" runat="server" class="btn btn-outline-secondary my-2 my-sm-0" type="button"></asp:Button>
                        </nav>
                    </div>

                    <div class="col-md-5">
                    </div>
                    <!-- ---------------------------------------- preview  -------------------------------- -->
                    <div class="col-md-2">
                    </div>
                </div>
            </div>
            <!-- ------------------------------------------- table row ---------------------------------------------- -->
            <div class="row mt-md-1">
                <div class="container">
                    <asp:GridView
                        CssClass="table table-striped"
                        runat="server"
                        ID="gvManageUsers"
                        AutoGenerateColumns="false"
                        OnRowDeleting="gvManageUsers_RowDeleting"
                        DataKeyNames="ID"
                        AllowPaging="true"
                        AllowSorting="true"
                        PageSize="5"
                        OnPageIndexChanging="gvManageUsers_PageIndexChanging"
                        ShowHeaderWhenEmpty="true"
                        EmptyDataText="No Records Found!"
                        OnRowEditing="gvManageUsers_RowEditing"
                        OnRowCancelingEdit="gvManageUsers_RowCancelingEdit"
                        OnRowUpdating="gvManageUsers_RowUpdating"
                        OnRowDataBound="gvManageUsers_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFname" runat="server" Text='<%# Eval("Fname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblLname" runat="server" Text='<%# Eval("Lname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username">
                                <ItemTemplate>
                                    <asp:Label ID="lbUsername" runat="server" Text='<%# Eval("Username") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" CssClass="text-nowrap" runat="server" Text='<%# Eval("DOB"  , "{0: dd/MM/yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admin">
                                <ItemTemplate>

                                    <asp:Literal ID="ltrStatusAdmin" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbAdmin" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserP">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrStatusUserP" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbUserP" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserN">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrStatusUserN" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbUserN" />
                                </EditItemTemplate>
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
                </div>

                <!-- ----------------------------------------- table ends ---------------------------------- -->
            </div>
        </div>

        
    </div>
</asp:Content>