<%@ Page Title="" Language="C#" MasterPageFile="~/loginheader.Master" AutoEventWireup="true" CodeBehind="forgotPassword.aspx.cs" Inherits="Phews.WebForm13" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="server">

    <title>Forgot Password</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top:200px;">

        <div class="col-md-12 justify-content-center align-self-center text-center">

            <p style="font-size:30px;">Enter your email address to receive a new password</p>

            <div class="form-group justify-content-center">
                <label for="password-recovery"></label>

                <input type="email" class="form-control w-50 m-auto" id="password-recovery" placeholder="Enter email"  aria-describedby="emailDesc"/>

                <small id="emailDesc" class="form-text text-muted">
                Change your password in three easy steps. This will help you to secure your password!<br />
                1. Enter your email address below. <br />
                2. Our system will send you a temporary link to your email <br />
                3. Use the link to reset your password<br /></small>

            </div>

            <div class="d-flex justify-content-center mt-3">
                <button type="button" class="btn btn-primary justify-content-center w-25">Reset my password</button>
            </div>

        </div>

    </div>

</asp:Content>
