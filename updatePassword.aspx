<%@ Page Title="" Language="C#" MasterPageFile="~/phews.Master" AutoEventWireup="true" CodeBehind="updatePassword.aspx.cs" Inherits="Phews.WebForm19" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Update Password</title>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- page body - 1 row 1 column grid -->
    <div class="row" style="margin-top: 200px;">

        <div class="col-lg-3">

        </div>
        
        <div class="col-lg-6 align-self-center text-center" >



            <h1 class="mb-5">Update Password</h1>

            <!-- form structure -->

            <!-- old password --> 
            <div class="form-group form-row">
                <asp:Label ID="lbl_old" runat="server" for="txt_old" class="col-sm-2 col-form-label" Text="Old Password: "/>

                <div class="col-sm-10">
                <asp:TextBox ID="txt_old" runat="server" TextMode="Password" name="txt_old" class="col form-control"   placeholder="Enter old password"  />
                <asp:Label ID="lbl_oldMsg" runat="server" for="txt_old" class="col-sm-2 col-form-label" style="visibility:hidden;"/>
                </div>

            </div>

            <!-- new password -->
            <div class="form-group form-row">
                <asp:Label ID="lbl_new" runat="server" for="txt_new" class="col-sm-2 col-form-label" Text="New Password: "/>

                <div class="col-sm-10">
                <asp:TextBox ID="txt_new" runat="server" TextMode="Password" name="txt_new"  class="col form-control" placeholder="Enter new password" aria-describedby="password_help" />
                <small id="passwordhelp" class="form-text text-muted"> Your password must be 8-20 characters long, contain letters and numbers, and must not contain emojis.</small>
                <asp:Label ID="lbl_newMsg" runat="server" for="txt_new" class="col-sm-2 col-form-label" style="visibility:hidden;"/>
                </div>
            </div>

            <!-- confirm password -->
            <div class="form-group form-row">
                <asp:Label ID="lbl_confirm" runat="server" for="txt_confirm" class="col-sm-2 col-form-label" Text="Confirm Password: " />

                <div class="col-sm-10">
                <asp:TextBox ID="txt_confirm" runat="server" TextMode="Password" name="txt_confirm" class="col form-control" placeholder="Confirm new password" />
                <asp:Label ID="lbl_confirmMsg" runat="server" for="txt_confirm" class="col-sm-2 col-form-label" style="visibility:hidden;"/>
                </div>
            </div>


            <div class="d-block justify-content-center text-center">
                <asp:Label ID="lbl_generalMsg" runat="server" style="visibility:hidden;"/>
            </div> 

            <asp:Button ID="btn_update" runat="server" class="btn btn-primary mt-3" Text="Update My Password" OnClick="btn_update_Click" />

        </div>


        <div class="col-lg-3">

        </div>


    </div>


</asp:Content>
