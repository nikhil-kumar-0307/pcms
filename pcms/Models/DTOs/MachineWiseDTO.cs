// pcms/Models/DTOs/MachineWiseDTO.cs
namespace pcms.Models.DTOs
{
    public class MachineWiseDTO
    {
        public string McNum { get; set; }   // Machine Num
        public string MachineName { get; set; }  // Model
        public string Dept { get; set; }
        public string CurrLoc { get; set; }   // Location
        public string EmpName { get; set; }   // Emp. Name
        public string Grade { get; set; }
        public string EmpNo { get; set; }   // Emp Num
        public string IpNo { get; set; }   // IP Add
        public string MacAddress { get; set; }   // Mac.Add
    }
}