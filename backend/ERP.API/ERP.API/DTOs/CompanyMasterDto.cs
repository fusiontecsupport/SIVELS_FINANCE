using System.Text.Json.Serialization;

namespace ERP.API.DTOs
{
    public class CompanyMasterDto
    {
        public int CMPYID { get; set; }
        public string CMPYNAME { get; set; }
        public string? CMPYADDR1 { get; set; }
        public string? CMPYADDR2 { get; set; }
        public string? CMPYADDR3 { get; set; }
        public string? CMPYCITY { get; set; }
        public string? CMPYSTATE { get; set; }
        public string? CMPYPINCODE { get; set; }
        public string? CMPYCONTACTPERSON { get; set; }

        [JsonPropertyName("cmpyphone1")]
        public string? CMPYPHONE1 { get; set; }

        [JsonPropertyName("cmpyphone2")]
        public string? CMPYPHONE2 { get; set; }

        public string? CMPYEMAIL { get; set; }
        public string? CMPYGSTNO { get; set; }
        public string? CMPYPANNO { get; set; }
        public short DISPSTATUS { get; set; }
    }
}