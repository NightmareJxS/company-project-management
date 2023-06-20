using Newtonsoft.Json;

namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class OdataCompanyProjectModel
    {
        [JsonProperty("@odata.context")]
        public string? ODataContext { get; set; }
        [JsonProperty("@odata.count")]
        public int? NumberOfRecords { get; set; }
        public List<CompanyProjectModel> Value { get; set; }
    }
}
