namespace Climate_Watch.Models
{
    public class EditAddProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public IFormFile Image { get; set; }
    }
}
