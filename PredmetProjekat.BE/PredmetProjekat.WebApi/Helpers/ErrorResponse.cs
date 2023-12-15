using Newtonsoft.Json;

namespace PredmetProjekat.WebApi.Helpers
{
    public class ErrorResponse  //todo expand fields if needed
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
