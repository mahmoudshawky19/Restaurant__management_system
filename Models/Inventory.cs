namespace Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Supplier Supplier {  get; set; }
        public int SupplierId {  get; set; }
    }
}
