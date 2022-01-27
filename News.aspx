<%@ Page Title="" Language="C#" MasterPageFile="~/Phews.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Phews.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>News</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row pl-md-5" style="margin-top: 130px">
            <h1 class="display-3">News</h1>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row pl-md-5">
            <!-- ------------------------------------ search bar ------------------------------------ -->

            <nav class="navbar navbar-light bg-light form-inline ">

                <asp:TextBox ID="txtSearch" class="form-control mr-sm-2" placeholder="Search" aria-label="Search" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" Text="Search" runat="server" class="btn btn-outline-primary my-2 my-sm-0" OnClick="btnSearch_Click"></asp:Button>
                <asp:Button Text="clear filters" ID="btnClearSearch" runat="server" class="btn btn-outline-secondary my-2 my-sm-0 mx-sm-2" OnClick="btnClearSearch_Click"></asp:Button>
            </nav>
            <div class="col-md-1"></div>
            <div class="col-md-4">

                <asp:DropDownList CssClass="dropdown mt-3" runat="server" AppendDataBoundItems="true" DataTextField="Name" ID="ddlCategory" DataValueField="ID" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value=" ">Choose a category</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <!-- ------------------------------------ Article ------------------------------------ -->
    <div class="container-fluid">
        <asp:Repeater ID="rptrArticles" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row mt-md-3">
                    <div class="col-md-4 justify-content-center align-self-center text-center">
                        <asp:HyperLink ID="lnkImage" runat="server" NavigateUrl='<%# Eval("ID", "~/newsDetails.aspx?ID={0}") %>'><img src="img/<%# Eval("img_path") %>" class="img-thumbnail" width="300" /></asp:HyperLink>
                    </div>
                    <div class="col-md-8">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <span class="text-nowrap">
                                    <asp:HyperLink CssClass="nav-link" ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "~/newsDetails.aspx?ID={0}") %>'> <h1><%# Eval("title") %></h1></asp:HyperLink>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <p class="lead"><strong><%# Eval("subtitle") %></strong></p>
                        </div>
                        <div class="col-md-12">
                            <p class="text-muted">Category: <strong><%# Eval("Name") %></strong> | Posted By: <strong><%# Eval("username") %></strong> | Posted On: <strong><%# Eval("Posted_Date", "{0: dd/MM/yyyy}") %></strong></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <!-- <div class="row mt-md-3">
		<div class="col-md-4 justify-content-center align-self-center text-center">
			<a href="#">
				<img src="img/04-nature_721703848.jpg" class="img-thumbnail" width="300" /></a>
		</div>
		<div class="col-md-8">
			<div class="col-md-12">
				<a href="#" class="nav-link">
					<h1>Heading</h1>
				</a>
			</div>
			<div class="col-md-12">
				<p class="lead"><strong>some text some text some text some text some text some text some text some text some text some text some text some text</strong></p>
			</div>
			<div class="col-md-12">
				<p class="text-muted">Category: <strong>Movies</strong> | Posted By: <strong>Ahmed</strong> | Posted On: <strong>20-1-2021</strong></p>
			</div>
		</div>
	</div> -->
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
    </div>
    <!-- ------------------------------------ Page numbers ------------------------------------ -->
    <!--  <div class="row mt-md-3">
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
</asp:Content>