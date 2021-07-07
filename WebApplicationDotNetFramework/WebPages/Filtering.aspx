<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Filtering.aspx.cs" Inherits="WebApplicationDotNetFramework.WebPages.Filtering" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filtering</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Filtering by different conditions</h1>
        <p><asp:Button ID="mainPageButton" runat="server" OnClick="mainPageButton_Click" Text="Home Page"/></p>
        Release date filtering (Note: It will work only in case both dates are specified)<br />
        <br />
        Start date(in format YYYY-MM-DD):<p>
        <asp:TextBox id="StartDateInput" type="text" name="StartDateInput" runat="server"/></p>
    <p>
        End date (in format YYYY-MM-DD):</p>
    <p>
        <asp:TextBox id="EndDateInput" type="text" name="EndDateInput" runat="server"/></p>
    <p>
        &nbsp;</p>
    <p>
        Only movies with specific keyword in genre (e.g Drama):</p>
    <p>
        <asp:TextBox id="specKeyword" type="text" name="specKeyword" runat="server"/></p>
    <p>
        &nbsp;</p>
    <p>
        Minimum rating for the movie (Rank from 0.00 up to 9.99):</p>
    <p>
        <asp:TextBox id="minRating" type="text" name="minRating" runat="server"/></p>
        <asp:Button ID="SubmitFiilteringButton" runat="server" OnClick="SubmitFilteringButton_Click" Text="Submit" />
        <p>Last filtered table:</p>
        <asp:GridView ID="filteredTable" runat="server">
        </asp:GridView>
        <p>Please choose id of the film you want to order, then press "Order film".</p>
       <p> <asp:DropDownList ID="movieIds" runat="server">
        </asp:DropDownList> </p>
        <asp:Button ID="OrderButton" runat="server" Text="Order film" OnClick="OrderButton_Click"/>
        <asp:Label ID="insertResult" runat="server"></asp:Label>
    </form>
    </body>
</html>
