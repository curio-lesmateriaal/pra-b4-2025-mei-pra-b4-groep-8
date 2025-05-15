using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    internal class OrderedProduct
    {
        public int photoId { get; set; }
        public string productName { get; set; }
        public int amount { get; set; }
        public double totalPrice { get; set; }

        public OrderedProduct(int photoId, string productName, int amount, double totalPrice)
        {
            this.photoId = photoId;
            this.productName = productName;
            this.amount = amount;
            this.totalPrice = totalPrice;
        }

    }
}
