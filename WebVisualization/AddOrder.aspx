<%@ Page Title="Dodaj zamówienie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="WebVisualization.AddOrder" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>:</h1>
    </hgroup>



                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="tbCustomerName">Imię</asp:Label>
                                <asp:TextBox runat="server" ID="tbCustomerName" />
                                
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="tbCustomerSurname">Nazwisko</asp:Label>
                                <asp:TextBox runat="server" ID="tbCustomerSurname" />
                                
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="tbCustomerAdress">Adres</asp:Label>
                                <asp:TextBox runat="server" ID="tbCustomerAdress" />
                                
                            </li>
                        </ol>
                        


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="exec GetProduct">       
        </asp:SqlDataSource>


                  <br />
                        (tabela nie działa poprawnie)

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


                <asp:TemplateField HeaderText="Dodaj">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Width="50" Height="20" Text="" />
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>


             <asp:Button runat="server" OnClick="AddOrderButtonClick" Text="Dodaj zamówienie" />
               </fieldset>

</asp:Content>
