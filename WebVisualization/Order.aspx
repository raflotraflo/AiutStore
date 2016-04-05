<%@ Page Title="Zamówienia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="WebVisualization.Order" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>:</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<%--    <hgroup class="title">
        <h1><%: Title %>:</h1>
    </hgroup>--%>


        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="exec GetOrder">       
        </asp:SqlDataSource>

  
        <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" 
         DataKeyNames="or_Id">  
            <Columns>

          <asp:TemplateField HeaderText="Lp.">
            <ItemTemplate>
                <%# (((GridView)((GridViewRow)Container).Parent.Parent).PageIndex) * ((GridView)((GridViewRow)Container).Parent.Parent).PageSize + Container.DataItemIndex + 1 %>.
            </ItemTemplate>
             <HeaderStyle BorderColor="#81D6B4" Width="3%" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" Width="3%" HorizontalAlign="Left"  Font-Size="Small"/>
        </asp:TemplateField>

            <asp:BoundField DataField="or_DateCreate" 
                SortExpression="or_DateCreate" HeaderText="Dodano" dataformatstring="{0:dd.MM.yyyy}">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 

            <asp:BoundField DataField="or_CustomerName" 
                SortExpression="or_CustomerName" HeaderText="Imię">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 
           
                <asp:BoundField DataField="or_CustomerSurname" 
                SortExpression="or_CustomerSurname" HeaderText="Nazwisko">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField>   

                                <asp:BoundField DataField="or_CustomerAdress" 
                SortExpression="or_CustomerAdress" HeaderText="Adres">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 
                            
                <asp:BoundField DataField="or_DateRealization" 
                SortExpression="or_DateRealization" HeaderText="Data realizacji" dataformatstring="{0:dd.MM.yyyy}">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 


                              <asp:CheckBoxField DataField="or_Realization" 
                HeaderText="Zrealizowano" 
                SortExpression="or_Realization" ReadOnly="True" >

            <HeaderStyle BorderColor="#81D6B4" Height="10px" />
            <ItemStyle BorderColor="#81D6B4" ForeColor="Black" Font-Bold="True" />
            </asp:CheckBoxField>

                           

                </Columns>

    </asp:GridView>

</asp:Content>
