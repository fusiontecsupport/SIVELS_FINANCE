using System.Text.Json.Serialization;

namespace ERP.API.DTOs
{
    public class CustomerMasterDto
    {
        [JsonPropertyName("cateid")] public int CATEID { get; set; }
        [JsonPropertyName("catecode")] public string? CATECODE { get; set; }
        [JsonPropertyName("catename")] public string? CATENAME { get; set; }
        [JsonPropertyName("catedname")] public string? CATEDNAME { get; set; }
        [JsonPropertyName("cate_GST_NO")] public string? CATE_GST_NO { get; set; }
        [JsonPropertyName("cate_PAN_NO")] public string? CATE_PAN_NO { get; set; }
        [JsonPropertyName("cate_TAN_NO")] public string? CATE_TAN_NO { get; set; }
        [JsonPropertyName("catebankname")] public string? CATEBANKNAME { get; set; }
        [JsonPropertyName("catebankbrnchname")] public string? CATEBANKBRNCHNAME { get; set; }
        [JsonPropertyName("catebankaddr")] public string? CATEBANKADDR { get; set; }
        [JsonPropertyName("catebank_ACTYPE")] public string? CATEBANK_ACTYPE { get; set; }
        [JsonPropertyName("catebank_ACNO")] public string? CATEBANK_ACNO { get; set; }
        [JsonPropertyName("catebank_IFCS_CODE")] public string? CATEBANK_IFCS_CODE { get; set; }
        [JsonPropertyName("catebank_IBAN_CODE")] public string? CATEBANK_IBAN_CODE { get; set; }
        [JsonPropertyName("catebank_SWIFT_CODE")] public string? CATEBANK_SWIFT_CODE { get; set; }
        [JsonPropertyName("cate_TALLY_NAME")] public string? CATE_TALLY_NAME { get; set; }
        [JsonPropertyName("dispstatus")] public short? DISPSTATUS { get; set; }

        [JsonPropertyName("cateaAddr")] public string? CATEAADDR { get; set; }
        [JsonPropertyName("cateaAddr1")] public string? CATEAADDR1 { get; set; }
        [JsonPropertyName("cateaAddr2")] public string? CATEAADDR2 { get; set; }
        [JsonPropertyName("cateA_CITY")] public string? CATEA_CITY { get; set; }
        [JsonPropertyName("cateA_COUNTRY")] public string? CATEA_COUNTRY { get; set; }
        [JsonPropertyName("cateA_PINCODE")] public string? CATEA_PINCODE { get; set; }
        [JsonPropertyName("loctid")] public int? LOCTID { get; set; }
        [JsonPropertyName("stateid")] public int? STATEID { get; set; }
        [JsonPropertyName("locationName")] public string? LocationName { get; set; }
        [JsonPropertyName("stateName")] public string? StateName { get; set; }
    }

    public class CreateCustomerDto
    {
        public string CATECODE { get; set; }
        public string CATENAME { get; set; }
        public string CATEDNAME { get; set; }
        public string? CATE_GST_NO { get; set; }
        public string? CATE_PAN_NO { get; set; }
        public string? CATE_TAN_NO { get; set; }
        public string? CATEBANKNAME { get; set; }
        public string? CATEBANKBRNCHNAME { get; set; }
        public string? CATEBANKADDR { get; set; }
        public string? CATEBANK_ACTYPE { get; set; }
        public string? CATEBANK_ACNO { get; set; }
        public string? CATEBANK_IFCS_CODE { get; set; }
        public string? CATEBANK_IBAN_CODE { get; set; }
        public string? CATEBANK_SWIFT_CODE { get; set; }
        public string? CATE_TALLY_NAME { get; set; }
        public short DISPSTATUS { get; set; }

        public List<AddressDto> Addresses { get; set; } = new();
    }

    public class AddressDto
    {
        public string CATEAADDR { get; set; }
        public string? CATEAADDR1 { get; set; }
        public string? CATEAADDR2 { get; set; }
        public string? CATEA_CITY { get; set; }
        public string? CATEA_COUNTRY { get; set; }
        public string? CATEA_PINCODE { get; set; }
        public int LOCTID { get; set; }
        public int STATEID { get; set; }
    }
}