<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="WebApplicationDotNetFramework.WebPages.Movies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>MOVIES</h1>
        </div>
        <asp:GridView ID="GridViewMovies" runat="server">
        </asp:GridView>
        <div>
            <h1>ORDERS</h1>
            <asp:GridView ID="GridViewOrders" runat="server">
            </asp:GridView>
        </div>
        <p>
            <asp:Button ID="FilterButton" runat="server" OnClick="FilterButton_Click" Text="Open filtering" />
        </p>
        <p>
            &nbsp;</p>
        
            <asp:Button ID="UpdateDeleteButton" runat="server" OnClick="UpdateDeleteButton_Click" Text ="Update or Delete orders" />
        
    </form>
</body>
</html>
