namespace E_Medicine_BE.Models
{
    public class Order_Items
    {
        public int ID { get; set; }
        public int Order_Id { get; set; }

        public int  Medicine_Id{ get; set; }

        public decimal Unit_Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Total_Price { get; set; }
    }
}
