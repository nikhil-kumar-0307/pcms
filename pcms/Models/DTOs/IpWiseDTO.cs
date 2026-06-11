// pcms/Models/DTOs/IpWiseDTO.cs
namespace pcms.Models.DTOs
{
    public class IpWiseDTO
    {
        public string McNum { get; set; }   // Machine Num
        public string MachineName { get; set; } // Model (McNum used as model name)
        public string Dept { get; set; }
        public string CurrLoc { get; set; }   // Location
        public string EmpName { get; set; }   // Emp. Name
        public string Grade { get; set; }
        public string EmpNo { get; set; }   // Emp Num
        public string MacAddress { get; set; }  // Mac.Add
        public string IpNo { get; set; }   // searched IP
    }
}