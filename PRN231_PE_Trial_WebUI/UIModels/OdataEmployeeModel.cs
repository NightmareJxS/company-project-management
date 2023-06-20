using Newtonsoft.Json;

namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class OdataEmployeeModel
    {
        [JsonProperty("@odata.context")]
        public string? ODataContext { get; set; }
        [JsonProperty("@odata.count")]
        public int? NumberOfRecords { get; set; }
        public List<EmployeeModel> Value { get; set; }
    }
}
