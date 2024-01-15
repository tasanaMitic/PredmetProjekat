namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class ProductTypeDto
    {
        public string Name { get; set; }
        public IEnumerable<AttributeDto> Attributes { get; set; }
    }
}
