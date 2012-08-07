<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Holds_View.ascx.cs" Inherits="CUS.ICS.LoginNotifications.Holds_View" %>

<%@ Register 
    TagPrefix="common"
	assembly="Jenzabar.Common"
	Namespace="Jenzabar.Common.Web.UI.Controls"
%>

<div class="pSection">
	<br />
	<center>
	    <common:Subheader ID="hdrGrad" runat="server" Text="Graduate Survey!!!" />
 	</center>
    <asp:CheckBox ID="CheckBox1" runat="server" Text="Take me to the survey !!!" />
 	<br />
	<center>
	    <common:Subheader ID="hdrHolds" runat="server" Text="You have Registration Holds!" />
 	</center>
    <asp:Label ID="Label1" runat="server" Text="You have holds in the following offices that will prevent you from registering for your classes. Go to the <i>Registration and Advising</i> page on the <i>Student</i> Tab for more information:" />
 	<br />
    <asp:SqlDataSource OnSelected="SqlDataSourceHoldsStatusEventHandler" ID="sqlDataSourceHolds" runat="server" 
        ConnectionString='<%$ ConnectionStrings:JenzabarConnectionString %>' 
        ProviderName="<%$ ConnectionStrings:JenzabarConnectionString.ProviderName %>" /> 
  
    <asp:DataList ID="dataListHolds" runat="server" DataSourceID="sqlDataSourceHolds" Width="100%"> 
        <ItemTemplate>
            <h6><asp:Label ID="Label3" runat="server" text='<%# Eval("HOLD_DESC") %>' /></h6>
        </ItemTemplate>
    </asp:DataList>
	
    <asp:Label ID="lblError" runat="server" />
	 <center>
	    <asp:Button id="btnOK" OnClick="btnOK_Click" Text="OK" runat="server" Width="50px" />&nbsp;&nbsp;
    </center>
</div>