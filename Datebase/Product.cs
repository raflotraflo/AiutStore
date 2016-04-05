using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebase
{
    public class Product
    {
        public int Id { get; set; }
        public string Barecode { get; set; }
        public string Name { get; set; }
        public DateTime DateWarianty { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int Count { get; set; }
        public int CountReserved { get; set; }
        public int CountSold { get; set; }
        public int AddCount { get; set; }
    }
}
