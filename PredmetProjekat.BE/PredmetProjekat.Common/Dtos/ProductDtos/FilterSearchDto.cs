namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class FilterSearchDto
    {
        public IEnumerable<ReceiptDto> ReceiptDtos { get; set; }
        public OptionParams OptionParameters { get; set; }
    }
}
