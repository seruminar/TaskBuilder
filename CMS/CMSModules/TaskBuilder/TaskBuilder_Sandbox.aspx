<%@ Page Language="C#" AutoEventWireup="true" Inherits="TaskBuilder_Sandbox"
    Theme="Default" EnableEventValidation="false"  CodeFile="TaskBuilder_Sandbox.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlBody" runat="server">
        <asp:Panel ID="pnlContent" runat="server">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" CssClass="InfoLabel" />     
            <asp:Literal ID="network" runat="server"></asp:Literal>
            <asp:Literal ID="initScript" runat="server"></asp:Literal>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
