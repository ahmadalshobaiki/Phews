<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="newAlbum.aspx.cs" Inherits="Phews.WebForm16" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>New Album</title>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- first row for header -->
    <div class="row" style="margin-top: 130px;">
        
        <div class="col">
            <h1 runat="server" id="heading">New Album</h1>
        </div>
    </div>

    <!-- second row for content -->
    <div class="row">

        <div class="col">
            <!-- border -->
            <div class="border border-primary p-5">
                
                <!-- form structure -->
                <!-- album title -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_title" runat="server" for="txt_title" class="col-sm-2 col-form-label" Text="Album Title: "></asp:Label>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_title" runat="server" name="title" class="form-control" placeholder="Enter album title"/>
                    </div>
                </div>

                <!-- album description -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_desc" runat="server" for="txt_desc" class="col-sm-2 col-form-label" Text="Album Description: "></asp:Label>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_desc" runat="server" name="desc" class="form-control" placeholder="Enter album description"/>
                    </div>
                </div>

                <!-- thumbnail -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_thumbnail" runat="server" for="file_uploader" class="col-sm-2 col-form-label" Text="Thumbnail: "></asp:Label>

                    <div class="col-sm-10">
                        <asp:FileUpload ID="file_uploader" runat="server" class="form-control-file" placeholder="Upload Image"/>
                        <asp:Label ID="lbl_fileUploadMsg" runat="server" style="visibility:hidden;" for="file_uploadThumbnail"/>
                    </div>
                </div>

                <div class="col text-center">
                        <asp:Button ID="btn_submit" runat="server" class="d-inline btn btn-primary mt-3 w-50" Text="Add new Album " OnClick="btn_submit_Click"></asp:Button>
                </div>

                <!-- hidden label -->
                <div class="d-block justify-content-center text-center">
                    <asp:Label ID="lbl_generalMsg" runat="server" style="visibility:hidden;"/>
                </div> 

            </div>

        </div>

    </div>


</asp:Content>
