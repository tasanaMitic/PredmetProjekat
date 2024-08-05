
namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class FilterParams
    {
        public string? RegisterCodes { get; set; }
        public string? Locations { get; set; }
        public string? EmployeeUsernames { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public decimal? Price { get; set; }
    }
}
