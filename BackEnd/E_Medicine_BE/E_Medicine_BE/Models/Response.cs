namespace E_Medicine_BE.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public List<Users> List_Users { get; set; }

        public Users User { get; set; }

        public List<Medicines> List_Medicines { get; set; }
        public Medicines Medicine { get; set; }

        public List<Cart> List_Cart { get; set; }
        public List<Orders> List_Orders { get; set; }
        public Orders Order { get; set; }

        public List<Order_Items> List_Order_Items{ get; set; }
        public Orders Order_Item { get; set; }


    }
}
