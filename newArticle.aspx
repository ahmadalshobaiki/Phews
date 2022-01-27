<%@ Page Title="" Language="C#" MasterPageFile="~/admin.Master" AutoEventWireup="true" CodeBehind="newArticle.aspx.cs" Inherits="Phews.WebForm17" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>New Article</title>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- first row for header -->
    <div class="row" style="margin-top: 100px;">
        
        <div class="col">
            <h1 runat="server" id="heading">New Article</h1>    
        </div>
    </div>

    
    <!-- second row for content -->
    <div class="row">

        <div class="col">
            <!-- border -->
            <div class="border border-primary p-5">
                
                <!-- form structure -->

                <!-- article title -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_title" runat="server" for="txt_title" class="col-sm-2 col-form-label" Text="Title: "/>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_title" runat="server" name="txt_title" class="form-control" placeholder="Enter article title"/>
                    </div>
                </div>

                <!-- article subtitle -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_subtitle" runat="server" for="txt_subtitle" class="col-sm-2 col-form-label" Text="Subtitle: " />

                    <div class="col-sm-10">
                        <asp:TextBox id="txt_subtitle" runat="server" name="txt_subtitle" class="form-control" placeholder="Enter article subtitle"/>
                    </div>
                </div>

                <!-- thumbnail -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_thumbnail" runat="server" for="file_uploadThumbnail" class="col-sm-2 col-form-label" Text="Thumbnail: " />
                    <div class="col-sm-10">
                        <asp:FileUpload id="file_uploadThumbnail" runat="server" name="file_uploadThumbnail" class="form-control-file" placeholder="Upload Image"/>
                        <asp:Label ID="lbl_fileUploadMsg" runat="server" style="visibility:hidden;" for="file_uploadThumbnail"/>
                    </div>
                </div>

                <!-- Category -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_category" runat="server" for="ddl_category" class="col-sm-2 col-form-label" Text="Select Category: " />

                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddl_category" runat="server" class="form-control" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true" >
                            <asp:ListItem Value="">Select a Category</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- text area -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_details" runat="server" for="txt_details" class="col-sm-2 col-form-label" Text="Text: " />

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_details" runat="server" TextMode="MultiLine" class="form-control"  rows="10" placeholder="Enter text here" />
                    </div>
                </div>

                <!-- hidden label -->
                <div class="d-block justify-content-center text-center">
                    <asp:Label ID="lbl_generalMsg" runat="server" style="visibility:hidden;"/>
                </div> 

                <!-- submit button -->
                <div class="col text-center">
                        <asp:Button ID="btn_post" runat="server" class="d-inline btn btn-primary mt-3 w-50" Text="Post " OnClick="btn_post_Click"/>
                    </div>

            </div>

        </div>

    </div>


</asp:Content>
