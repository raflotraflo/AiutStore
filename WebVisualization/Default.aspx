<%@ Page Title="Lista produktów" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebVisualization._Default" %>

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


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="exec GetProduct">       
        </asp:SqlDataSource>



        <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" 
         DataKeyNames="pr_Id">  
            <Columns>

          <asp:TemplateField HeaderText="Lp.">
            <ItemTemplate>
                <%# (((GridView)((GridViewRow)Container).Parent.Parent).PageIndex) * ((GridView)((GridViewRow)Container).Parent.Parent).PageSize + Container.DataItemIndex + 1 %>.
            </ItemTemplate>
             <HeaderStyle BorderColor="#81D6B4" Width="3%" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" Width="3%" HorizontalAlign="Left"  Font-Size="Small"/>
        </asp:TemplateField>

            <asp:BoundField DataField="pr_Name" 
                SortExpression="pr_Name" HeaderText="Nazwa">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 
           
                <asp:BoundField DataField="pr_Barecode" 
                SortExpression="pr_Barecode" HeaderText="Kod">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField>   
                            
                <asp:BoundField DataField="pr_DateWarranty" 
                SortExpression="pr_DateWarranty" HeaderText="Data ważności" dataformatstring="{0:dd.MM.yyyy}">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 

                                <asp:BoundField DataField="CountFree" 
                SortExpression="CountFree" HeaderText="Dostępne">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField>   

                                                <asp:BoundField DataField="CountReserved" 
                SortExpression="CountReserved" HeaderText="Zarezerwowane">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 

                                                                <asp:BoundField DataField="CountSold" 
                SortExpression="CountSold" HeaderText="Sprzedane">
       
            <HeaderStyle BorderColor="#81D6B4" Height="10px"  />
            <ItemStyle BorderColor="#81D6B4" HorizontalAlign="Left"   Font-Size="Small"/>
         
            </asp:BoundField> 
                            

                </Columns>

    </asp:GridView>

</asp:Content>
