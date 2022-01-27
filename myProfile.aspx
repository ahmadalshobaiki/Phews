<%@ Page Title="" Language="C#" MasterPageFile="~/phews.Master" AutoEventWireup="true" CodeBehind="myProfile.aspx.cs" Inherits="Phews.WebForm14" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Profile</title>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- row container --> 

    <div class="row" style="margin-top:130px;">

        <!-- 2 column grid 6/6 -->


        <!-- column 1 -->
        <div class="col-lg-4 align-self-center text-center">
            <img src="img/artworks-000015086198-nqimp4-t500x500.jpg" alt="Logo" class="img-fluid animate__animated animate__fadeInDown" />
        </div>


        <!-- column 2 -->
        <div class="col-lg-8">
            
            <!-- border -->
            <div class="border border-dark p-3">

                <!-- form inside the border -->

                <!-- form heading -->
                <h1 style="text-align:center;">Personal Information</h1>

                <!-- form structure -->
                <!-- username --> 
                <div class="form-group form-row">
                    <asp:Label ID="lbl_username" runat="server" for="txt_username" Text="Username: " class="col-sm-2 col-form-label" />

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_username" runat="server" name="txt_username" ReadOnly="true" class="form-control-plaintext"/>
                    </div>

                </div>

                <!-- first name -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_fname" runat="server" for="txt_fname" Text="First Name: " class="col-sm-2 col-form-label" />

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_fname" runat="server" name="txt_fname" class="form-control"/>
                        <asp:Label ID="lbl_fnameMsg" runat="server" for="txt_fname" style="visibility:hidden;"/>
                    </div>
                </div>


                <!-- last name -->
                <div class="form-group form-row">
                    <asp:Label  ID="lbl_lname" runat="server" for="txt_lname" Text="Last Name:" class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_lname" runat="server" name="txt_lname" class="form-control"/>
                        <asp:Label ID="lbl_lnameMsg" runat="server" for="txt_lname" style="visibility:hidden;"/>
                    </div>
                </div>

                <!-- email -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_email" runat="server" for="txt_email" Text="Email: " class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_email" runat="server" name="txt_email" class="form-control"/>
                        <asp:Label ID="lbl_emailMsg" runat="server" for="txt_email" style="visibility:hidden;"/>
                    </div>
                </div>

                <!-- password -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_pass" runat="server" for="txt_pass" Text="Password: " class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <asp:TextBox  ID="txt_pass" runat="server" name="txt_pass" TextMode="Password" ReadOnly="true" class="form-control"/>
                        <a href="updatePassword.aspx">Update Password</a>
                    </div>
                </div>

                <!-- phone number -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_phone" runat="server" for="txt_phone" Text="Phone No: " class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_phone" runat="server" name="txt_phone" TextMode="Phone" class="form-control"/>
                        <asp:Label ID="lbl_phoneMsg" runat="server" for="txt_phone" style="visibility:hidden;"/>
                    </div>
                </div>

                <!-- Gender -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_gender" runat="server" for="drp_gender" Text="Gender: " class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <select class="form-control" id="drp_gender" runat="server">
                            <option value="none" selected disabled hidden>Gender</option>
                            <option value="m">Male</option>
                            <option value="f">Female</option>
                        </select>
                        <asp:Label ID="lbl_genderMsg" runat="server" for="drp_gender" style="visibility:hidden;"/>
                    </div>
                </div>

                <!-- DOB -->
                <div class="form-group form-row">
                    <asp:Label ID="lbl_dob" runat="server" for="txt_dob" Text="DOB: " class="col-sm-2 col-form-label"/>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txt_dob" runat="server" TextMode="Date" name="txt_dob" class="form-control"/>
                        <asp:Label ID="lbl_dobMsg" runat="server" for="txt_dob" style="visibility:hidden;"/>
                    </div>
                </div>

                <!-- checkboxes --> 
                  <div class="form-group form-row">
                <asp:Label ID="lblPreference" runat="server" for="txt_dob" Text="Preference: " class="col-sm-2 col-form-label"/>
                      <div class="col-sm-10">
                           <asp:CheckBoxList runat="server" id="cblPreference" RepeatColumns="2" CellPadding="5">
                                <asp:ListItem >Photos</asp:ListItem>
                                <asp:ListItem >News</asp:ListItem>
                           </asp:CheckBoxList>
                            <asp:Label ID="lbl_cb_msg" runat="server" Visible="false"/>
                        </div>
             
                    </div>

                <!-- buttons --> 
                <div class="row">

                    <div class="col text-center">
                        <asp:Button ID="btn_clear" runat="server" Text="Clear Changes" OnClick="Btn_Clear_Click" class="d-inline btn btn-secondary mt-3 w-50"/>
                    </div>

                    <div class="col text-center">
                        <asp:Button ID="btn_save" runat="server" Text="Save Changes" OnClick="Btn_Save_Click" class="d-inline btn btn-primary mt-3 w-100"/>




                        <asp:Label ID="lbl_test" runat="server" style="visibility: hidden"  class="col-sm-2 col-form-label"/>
                    </div>

                </div>

            </div>

        </div>

    </div>


</asp:Content>
