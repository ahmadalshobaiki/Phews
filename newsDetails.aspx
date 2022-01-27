<%@ Page Title="" Language="C#" MasterPageFile="~/phews.Master" AutoEventWireup="true" CodeBehind="newsDetails.aspx.cs" Inherits="Phews.WebForm20" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title></title>

    <style>
        hr { display: block; 
             margin-before: 0.5em; 
             margin-after: 0.5em;
             margin-start: auto; 
             margin-end: auto;
             overflow: hidden; 
             border-style: inset; 
             border-width: 1px;

        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top:130px;">

        <div class="col-md-12">
            <h1 style="color: #085394" runat="server" id="header" ></h1>
            <p class="lead" runat="server" id="subtitle"></p>
           <p class="text-muted">Posted By: <span class="font-weight-bold" runat="server" id="postedBy"></span><br />
            Posted on: <span class="font-weight-bold" runat="server" id="postedOn"></span><br />
            </p>
            <hr />
        </div>
    </div>

    <div class="row mx-auto mt-4">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <img src="" class="img-fluid img-thumbnail d-block mx-auto" runat="server" id="image" height="500" width="500" />
            <br />
             <pre class="text-justify" runat="server" id="articleText" style=" white-space:pre-wrap; font-size:medium;" ></pre>
           
                               
        </div>
         <div class="col-md-2"></div>
    </div>

</asp:Content>
