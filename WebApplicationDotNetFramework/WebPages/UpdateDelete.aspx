<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateDelete.aspx.cs" Inherits="WebApplicationDotNetFramework.WebPages.UpdateDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Updating and Deleting specific orders</h1>
        <div>
            <p><asp:Button ID="homePageButton" runat="server" Text="Home Page" OnClick="homePageButton_Click" /></p>
            <p>Current Orders table</p>
            <p>
                <asp:GridView ID="ordersGrid" runat="server"></asp:GridView> </p>
            <p>Choose Order ID you want to UPDATE:</p>
            <asp:DropDownList ID="orderIds" runat="server"></asp:DropDownList>
            <p>Be careful! If you enter something in incorrect format nothing will happen</p>
            <p>Change Rental Date(in format YYYY-MM-DD): </p>

            <asp:TextBox ID="rentalDate" runat="server"></asp:TextBox>

            <p>Change Return Date(in format YYYY-MM-DD): </p>
            <asp:TextBox ID="returnDate" runat="server"></asp:TextBox>

            <p>Change movie ID: </p>
            <asp:TextBox ID="movieID" runat="server"></asp:TextBox>

            <p>Change Net Amount(e.g 999.999): </p>
            <asp:TextBox ID="netAmount" runat="server"></asp:TextBox>

            <p>Change Discount(interval from 0 to 1, e.g 0.25 = 25%): </p>
            <asp:TextBox ID="discount" runat="server"></asp:TextBox>

            <p>Change Gross Amount: </p>
            <asp:TextBox ID="grossAmount" runat="server"></asp:TextBox>
            
            <p> <asp:Button ID="updateButton" runat="server" Text="Save Changes" OnClick="updateButton_Click"/></p>
            <asp:Label ID="updateResultMessage" runat="server"></asp:Label>
            <p>To DELETE specific rows, enter their IDs separated with blank spaces (e.g 2 4 5 18) and press "Delete" button</p>
            <asp:TextBox ID="rowsToDelete" runat="server"></asp:TextBox>
            <p><asp:Button ID="deleteButton" runat="server" Text="Delete" OnClick="deleteButton_Click"/></p>
            <asp:Label ID="deleteResultMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
