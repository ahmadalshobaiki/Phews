<%@ Page Title="" Language="C#" MasterPageFile="~/Phews.Master" AutoEventWireup="true" CodeBehind="Albums.aspx.cs" Inherits="Phews.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Albums</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="container-fluid">
            <div class="row pl-md-5" style="margin-top: 130px">
                <h1 class="display-3">Albums</h1>
            </div>
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
        <!-- ------------------------------------ Albums ------------------------------------ -->
        <div class="container">
            <!-- ------------------------------------ Container Start ------------------------------------ -->
            <div class="row">
                <!-- ------------------------------------ Row Start ------------------------------------ -->
                <asp:Repeater ID="rptrAlbums" runat="server" OnItemDataBound="rptrAlbums_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-md-4 my-md-3">
                            <div class="card">

                                <asp:HyperLink runat="server" ID="lnkImage" NavigateUrl='<%# Eval("ID", "~/Photos.aspx?ID={0}") %>'><img class="card-img-top" src="img/<%# Eval("Thumbnail") %>"></asp:HyperLink>

                                <div class="card-body">
                                    <h4 class="card-title"><%# Eval("Title") %></h4>
                                    <p class="card-text">
                                        <span class="text-nowrap">No. of photos: <strong>
                                            <asp:Literal ID="ltrlNumOfPhotos" runat="server"> </asp:Literal></strong>  </span>| <span class="text-nowrap">Posted By: <strong><%# Eval("Username") %></strong> </span>| <span class="text-nowrap">Posted On: <strong><%# Eval("Publish_Date", "{0: dd/MM/yyyy}") %></strong></span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>

                <!--	<div class="col-md-4 my-md-3">
				<div class="card">
					<a href="#">
						<img class="card-img-top" src="img/Phews.png"></a>
					<div class="card-body">
						<h4 class="card-title">Album title</h4>
						<p class="card-text"><span class="text-nowrap">No. of photos: <strong>6</strong>  </span>| <span class="text-nowrap">Posted By: <strong>Ahmed</strong> </span>| <span class="text-nowrap">Posted On: <strong>20-1-2021</strong></span>  </p>
					</div>
				</div>
			</div> -->

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
</asp:Content>