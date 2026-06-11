// pcms/Models/DTOs/AssetHolderDTO.cs
using System;

namespace pcms.Models.DTOs
{
    public class AssetHolderDTO
    {
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string McNum { get; set; }   // Mcn_No
        public string PTag { get; set; }   // Tag
        public string MachineName => McNum;      // display alias
        public string Grade { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
        public string CurrLoc { get; set; }   // Location
        public string IpNo { get; set; }   // IP Add
        public string SiNum { get; set; }   // Si Num
        public DateTime? IssueDt { get; set; }
        public string RecEmpNo { get; set; }   // Renum
        public string RecEmpName { get; set; }   // Rename
        public string AssetCode { get; set; }
        public string LotNo { get; set; }
        public string StType { get; set; }
        public string PoolTag { get; set; }
        public string Remarks { get; set; }
    }
}