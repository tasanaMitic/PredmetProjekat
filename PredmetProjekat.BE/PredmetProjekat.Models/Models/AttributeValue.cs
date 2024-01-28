namespace PredmetProjekat.Models.Models
{
    public class AttributeValue
    {
        public Guid AttributeValueId { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public Guid ProductAttributeId { get; set; }
        public string Value { get; set; }

    }
}
