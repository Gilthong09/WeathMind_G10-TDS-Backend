namespace WealthMind.Core.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string ProductType { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; }
    }
}
