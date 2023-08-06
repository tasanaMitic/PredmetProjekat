namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class StockedProductDto 
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public CategoryDtoId Category { get; set; }
        public BrandDtoId Brand { get; set; }
        public bool IsInStock { get; set; }
    }
}
