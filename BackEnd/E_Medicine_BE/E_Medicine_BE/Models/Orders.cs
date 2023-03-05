namespace E_Medicine_BE.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int User_Id { get; set; }    
        public string Order_No { get; set; }   
        public decimal Order_Total { get; set; }
        public string Order_Status { get; set; } 
    }
}
