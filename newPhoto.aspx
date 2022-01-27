<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="newPhoto.aspx.cs" Inherits="Phews.WebForm18" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     
    <title>New Photo</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link type="text/css" rel="stylesheet" href="http://example.com/image-uploader.min.css">

    

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- first row for header -->
    <div class="row" style="margin-top: 130px;">
        
        <div class="col">
            <h1>Upload New Photos</h1>
        </div>
    </div>

    <!-- second row for content -->
    <div class="row">

        <div class="col">
            <%--<!-- border -->--%>
            <div class="border border-primary p-5">
                
                <!-- form structure -->

                <!-- photo title -->
                <!-- <div class="form-group form-row">
                    <asp:Label ID="lbl_title" runat="server" for="txt_title" class="col-sm-2 col-form-label" Text="Title: "/>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_title" runat="server" name="txt_title"  class="form-control" placeholder="Enter photo title"/>
                    </div>
                </div> -->

                

                <!-- thumbnail -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_thumbnail" runat="server" for="file_uploadPhotos" class="col-sm-2 col-form-label" Text="Photo: "/>

                    <div class="col-sm-10 input-images">
                        <asp:FileUpload ID="file_uploadPhotos" runat="server" name="file_uploadPhotos" AllowMultiple="true" class="form-control-file" placeholder="Upload Image"/>
                        <asp:Label ID="lbl_photosInfo" runat="server" style="visibility:hidden;" for="file_uploadPhotos"/>
                    </div>
                </div>

                <!-- album  -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_album" runat="server" for="ddl_album" class="col-sm-2 col-form-label" Text="Select Album: " />

                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddl_album" runat="server" class="form-control" DataTextField="Title" DataValueField="ID" AppendDataBoundItems="true">
                            <asp:ListItem value="">Choose an album</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- text area -->
               <!-- <div class="form-group form-row">
                    <asp:Label ID="lbl_details" runat="server" for="txt_details" class="col-sm-2 col-form-label" Text="Photo Description: " />

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_details" runat="server" TextMode="MultiLine" class="form-control" rows="3" placeholder="Enter description here"/>
                    </div>
                </div> -->

                <!-- upload button -->
                <div class="col text-center">
                        <asp:Button ID="btn_upload" runat="server" class="d-inline btn btn-dark mt-3 w-25" Text="Upload pictures" OnClick="btn_upload_Click" />
                </div>

                
                <asp:Repeater ID="rptr_photos" runat="server">
		            <HeaderTemplate>
                        <h3>Image preview</h3>                       
                        <br />
		            </HeaderTemplate>

		            <ItemTemplate>                       
                            <div class="col-md-3 d-inline">

                                <asp:Label ID="lbl_title" runat="server" Visible="false" Text='<%# Eval("Img_Path") %>' /> <!-- scope identity here -->
                                <asp:Image ID="img_preview" runat="server" ImageUrl='<%# "~/img/" + Eval("Img_Path") %>' Height="200" Width="300" CssClass="img-thumbnail mt-2" />
                                <asp:TextBox ID="txt_imgTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="form-control mt-2"/>
                                
                            </div>                   
		            </ItemTemplate>

		            <FooterTemplate>
                        <!-- save button -->
                        <div class="col text-center">
                                <asp:Button ID="btn_save" runat="server" class="d-inline btn btn-primary mt-3 w-50" Text="Save" OnClick="btn_save_Click" />
                            </div>
		           </FooterTemplate>
	            </asp:Repeater>





                

                

                <br />
                <div class="col text-center">
                        <asp:Label ID="lbl_generalMsg" runat="server" style="visibility:hidden;"/>
                </div>

            </div>

        </div>

    <script type="text/javascript" src="http://example.com/jquery.min.js"></script>
    <script type="text/javascript" src="http://example.com/image-uploader.min.js"></script>
    <script>$('.input-images').imageUploader();</script>

    </div>

    


</asp:Content>
