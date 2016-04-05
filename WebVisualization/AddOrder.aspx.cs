using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datebase;
using System.Collections.ObjectModel;
using System.Data;

namespace WebVisualization
{
    public partial class AddOrder : System.Web.UI.Page
    {
        DB db = new DB(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddOrderButtonClick(object sender, EventArgs e)
        {
            Datebase.Order or = new Datebase.Order() { CustomerName = tbCustomerName.Text, CustomerSurname = tbCustomerName.Text, CustomerAdress = tbCustomerAdress.Text, DeliveryType = new DeliveryType() { Id = 1 } };
            
            Status st = db.AddOrder(or);
            if (st.State)
            {
                //dodanie produktów do zamówienia hmmmm

                //foreach (AddProductViewModel p in NewOrderVM.AddProductCollection)
                //{
                //    if (p.AddCount > 0)
                //    {
                //        if (!db.AddProductToOrder(p.Id, st.Id, p.AddCount).State)
                //            MessageBox.Show("Błąd podczas dodawania " + p.Name);
                //        //if(!db.AddProductToOrder(p.)
                //    }
                //}
                Response.Redirect("~/Order.aspx");
            }

        }

 
    }
}