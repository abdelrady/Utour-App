<%@ Page Title="" Language="C#" MasterPageFile="~/NewMasterPage.master" AutoEventWireup="true" CodeFile="viewMonumentPhotos.aspx.cs" Inherits="Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
       <asp:DataList ID="hotSpotsDataList" runat="server" DataKeyField="hotSpotID" 
           RepeatColumns="3" cellspacing="2" cellpadding="4">
           <FooterStyle BackColor="#CCCCCC" />
           <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
           <ItemStyle BackColor="White" />
        <itemtemplate>
        <table id="Table1" cellpadding="1" cellspacing="1" visible ="true">
                                   <tr>
                                            <td>
                                            <p align="left">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' 
                                                Width="200" Height="200" 
                                                onclick = "LoadDiv(this.src, this)" style ="cursor:pointer"/>
                                            </p>
                                            </td>
                                            <td valign="top">
                                            <p align="left" style="width:150px; margin-left:4px" >
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Description") %>' Font-Bold="True" ForeColor="#660033"></asp:HyperLink>    
                                             </p>
                                             </td>
                                    </tr>
                            </table>
        </itemtemplate>
           <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
    </asp:DataList>

       <asp:Panel ID="pnlPager" runat="server" Height="20px" Width="153px">
    <asp:LinkButton ID="lnkPrev" runat="server" CommandName = "Previous" 
        Text = "<< Previous" OnClick = "Pager_Click"></asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="lnkNext" runat="server" CommandName = "Next" 
        Text = "Next >>"  OnClick = "Pager_Click"></asp:LinkButton>
</asp:Panel>
      </div> 

      <div id="divBackground" class="modal">
      </div>

<div id="divImage" >
    <table style="height: 100%; width: 100%">
        <tr>
            <td valign="middle" align="center" colspan = "3" style ="height:500px;">
                <img id="imgLoader" name="imgLoader" runat="server" alt=""
                 src="~/images/loader.gif" />
                <img id="imgFull" alt="" src="" 
                 style="display: none;
                height: 500px;width: 600px" />
            </td>
        </tr>
        <tr>
            <td align = "left" style="padding:10px;width:200px">
              <a id = "Previous" href = "javascript:" onclick = "doPaging(this)" style ="display:none">Previous</a>
                <span id = "lblPrevious">Previous</span>
            </td>
             <td align="center" valign="middle" style ="width:200px">
                <input id="btnClose" type="button" value="close" style="width:55px; height:20px"
                 onclick="HideDiv()"/>
            </td>
            <td align = "right" style ="padding:10px;width:200px">
                <a id = "Next" href = "javascript:" onclick = "doPaging(this)">Next</a>
                <span id = "lblNext" style ="display:none">Next</span>
            </td>
        </tr>
        
    </table>
</div>
</asp:Content>

