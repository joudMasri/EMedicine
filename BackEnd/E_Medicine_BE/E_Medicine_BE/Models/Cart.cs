namespace E_Medicine_BE.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int User_Id { get; set; }
        public int Medicine_Id { get; set; }
        public decimal Unit_price { get; set; }
        public decimal Discount { get; set; }   
        public int Quantity { get; set; }   
        public decimal Total_Price { get; set; }    

    }
}
