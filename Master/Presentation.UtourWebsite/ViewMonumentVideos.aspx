<%@ Page Title="" Language="C#" MasterPageFile="~/NewMasterPage.master" AutoEventWireup="true" CodeFile="ViewMonumentVideos.aspx.cs" Inherits="ViewMonumentVideos" %>

<%@ Register Assembly="ASPNetFlashVideo.NET3" Namespace="ASPNetFlashVideo" TagPrefix="ASPNetFlashVideo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ASPNetFlashVideo:FlashVideo ID="hotSpotFlashVideo"  runat="server">
    </ASPNetFlashVideo:FlashVideo>   
<%--    
    <aspnetflashvideo:flashvideo ID="hotSpotFlashVideo" runat="server" /> 
    --%>
</asp:Content>
