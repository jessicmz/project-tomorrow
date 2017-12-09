<%@ Page Async="true" Language="C#" MasterPageFile="~/Speakup.master"  AutoEventWireup="true" CodeFile="SurveyReportNational.aspx.cs" Inherits="SurveyReportNational" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%-- Modified: 01/18/2014 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
    	<asp:Table cellSpacing="0" cellPadding="0" width="700" align="center" bgColor="#ffffff" border="0" runat="server">
			<asp:TableRow>
			    <asp:TableCell horizontalalign="center"   width="800">
			    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label>
                <br /><br />
                <asp:Label ID="lblReportHeading" runat="server" Text="" Font-Size="Large" Font-Bold="true"></asp:Label>
                <br /><br />
                <asp:Label ID="lblReportHeading2" runat="server" Text="" Font-Size="Large" Font-Bold="true"></asp:Label>
                <br /><br />             
                <div style="text-align: left; font-weight: bold;">                                  
                <asp:Label ID="lblName" runat="server"></asp:Label>
                <br /><br />
                <asp:Label ID="lblTotalSurveyStudents" runat="server"></asp:Label>
                <br /><br />
                </div> 
                <asp:Table ID="tblReport" runat="server" CellPadding="5" CellSpacing="0" Width="100%"></asp:Table>
                </asp:TableCell>
            </asp:TableRow>
     	</asp:Table>   
</asp:Content>