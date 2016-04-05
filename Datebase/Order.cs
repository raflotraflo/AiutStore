using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebase
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerAdress { get; set; }
        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime? DateRealization { get; set; }
        public bool Realization { get; set; }
    }
}
