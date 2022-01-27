<%@ Page Title="" Language="C#" MasterPageFile="~/LoginHeader.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Phews.WebForm15" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="server">

    
    <title> Login</title>


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- page body 2 column grid 5/6-->


            <div class="row" style="margin-top:130px;">

                 <!-- column 1 header -->
                <div class="col-md-5 justify-content-center align-self-center">
                    <h1 style="text-align:center; font-size:100px; font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; color:#085394;" class="animate__animated animate__fadeInDown">Phews</h1>
                    <p style="text-align:center; font-size: 30px; font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;" class="animate__animated animate__fadeInUp">Your #1 destination for viewing photos and news</p>
                </div>


             

                <!-- column 2 sign up form -->
                <div class="col-md-6">
                    

                    <div class=" border border-dark p-5">

                        <h1 style="text-align:center;margin-bottom:40px;">Create an Account</h1>

                        <div class="d-block form-group justify-content-center mb-0">
                            <label for="username_signup" style="display:none;"></label>
                            <asp:TextBox runat="server" ID="username_signup" placeholder="Enter username" CssClass="form-control w-100" />

                            <div>
                                <asp:Label ID="error_username" runat="server" for="username_signup" Text="*Username is taken" style="visibility:hidden; color:#ff0000;"/>
                            </div>
                        </div>
                        

                        <div class="d-block form-group justify-content-center mb-0">
                            <label for="email_signup" style="display:none;"></label>
                            <asp:TextBox runat="server" ID="email_signup" placeholder="Enter email" CssClass="form-control w-100" />
                            
                            <div>
                                <asp:Label ID="error_email" runat="server" for="email_signup" Text="*Email is taken" Visible="false" style=" color:#ff0000;"/>
                            </div>
                        </div>

                        <div class="d-block form-group justify-content-center mb-0">
                            <label for="password_signup" style="display:none;"></label>
                            <asp:TextBox TextMode="Password" runat="server" ID="password_signup" CssClass="form-control w-100" placeholder="Enter password" aria-describedby="password_help"/>
                            <small id="passwordhelp" class="form-text text-muted"> Your password must be 8-20 characters long, contain letters and numbers, and must not contain emojis.</small>

                            <div>
                                <asp:Label ID="error_pass" runat="server" for="password_signup" Text="*Password does not meet criteria" style="visibility:hidden; color:#ff0000;"/>
                            </div>
                        </div>
                        
                        
                        <div class="d-block form-group justify-content-center mb-0">
                            <label for="confirm_pass" style="display:none;"></label>
                            <asp:TextBox TextMode="Password" runat="server" ID="confirm_pass" CssClass="form-control w-100" placeholder="Confirm password" aria-describedby="mismatch"/>  
                            <br />
                            <p>Choose your preference</p>
                            <asp:CheckBoxList runat="server" id="cblPreference" RepeatColumns="2" CellPadding="5">
                                <asp:ListItem >Photos</asp:ListItem>
                                <asp:ListItem >News</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:Label ID="err_role" runat="server" style="visibility:hidden; color:#ff0000;"/>
                            <div>
                                <asp:Label ID="error_confirm" runat="server" for="confirm_pass" style="visibility:hidden; color:#ff0000;"/>
                            </div>

                        </div>
                        
                        <div class="d-block justify-content-center text-center">
                            <!--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="password_signup" 
                                ControlToValidate="email_signup" ErrorMessage="Password does not match" ForeColor="Red" 
                                ValidateRequestMode="Enabled"></asp:CompareValidator>-->
                            <asp:Label ID="error_empty" runat="server" style="visibility:hidden; color:#ff0000;"/>


                        </div> 
                        

                        <br/>

                        <div class="d-flex justify-content-center">
                            <asp:Button ID="signup" runat="server" type="button" CssClass="btn btn-primary justify-content-center w-50" 
                            Text="Signup" OnClick="Signup"/>
                        </div>
                        
                         </div>
                    </div>

              

            </div>


</asp:Content>
