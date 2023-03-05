namespace E_Medicine_BE.Models
{
    public class Medicines
    {
        public int ID { get; set; } 
        public string Name { get; set; }

        public string Manufacturer { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime Exp_Date { get; set; }
        public string Image_Url { get; set; }
        public int Status { get; set; }

        public string Type { get; set; }

    }
}
