using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCMS.DTOs
{
    // ────────────────────────────────────────────────────────────────
    //  ItStockDto  –  bound to the IT Stock view
    //  Holds both the header (lot summary) and the entry-form fields
    // ────────────────────────────────────────────────────────────────
    public class ItStockDto
    {
        // ── Lot lookup ─────────────────────────────────────────────
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }

        // ── Read-only lot header info (populated from LotMaster) ───
        public string LotEquipType { get; set; }
        public string LotEquipTypeCode { get; set; }
        public string LotModel { get; set; }
        public string LotBrandName { get; set; }
        public string LotBrandCode { get; set; }
        public int? TotalQty { get; set; }
        public int AllottedQty { get; set; }   // count of ItStock rows for this lot
        public bool LotLoaded { get; set; }

        // ── Entry form ─────────────────────────────────────────────
        public int? EditId { get; set; }            // null → insert, >0 → update

        [Display(Name = "E No")]
        public int? ENo { get; set; }

        [StringLength(20)]
        [Display(Name = "Emp No")]
        public string EmpNo { get; set; }

        [StringLength(200)]
        [Display(Name = "Name")]
        public string EmpName { get; set; }

        [StringLength(20)]
        [Display(Name = "Dept")]
        public string Dept { get; set; }

        [StringLength(200)]
        [Display(Name = "Dept Name")]
        public string DeptName { get; set; }

        [StringLength(20)]
        [Display(Name = "Rec Emp No")]
        public string RecEmpNo { get; set; }

        [StringLength(200)]
        [Display(Name = "Rec Emp Name")]
        public string RecEmpName { get; set; }

        [StringLength(20)]
        [Display(Name = "Rec Dept")]
        public string RecDept { get; set; }

        [StringLength(200)]
        [Display(Name = "Rec Dept Name")]
        public string RecDeptName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Issue Date")]
        public DateTime? IssueDt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transfer In/Out Date")]
        public DateTime? TransferInOutDt { get; set; }

        [StringLength(10)]
        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [StringLength(200)]
        [Display(Name = "Current Location")]
        public string CurrLoc { get; set; }

        [StringLength(200)]
        [Display(Name = "Previous Location")]
        public string PrevLoc { get; set; }

        /// <summary>'CR', 'SF', etc.</summary>
        [StringLength(10)]
        [Display(Name = "St Type")]
        public string StType { get; set; }

        /// <summary>'O' = Old, 'N' = New</summary>
        [StringLength(1)]
        [Display(Name = "Pool Tag (O/N)")]
        public string PoolTag { get; set; }

        [StringLength(100)]
        [Display(Name = "Asset Code")]
        public string AssetCode { get; set; }

        [StringLength(100)]
        [Display(Name = "PV Code")]
        public string PvCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Mc Num")]
        public string McNum { get; set; }

        [StringLength(50)]
        [Display(Name = "P TAG")]
        public string PTag { get; set; }

        [StringLength(100)]
        [Display(Name = "IP No")]
        public string IpNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Mac Address")]
        public string MacAddress { get; set; }

        [StringLength(20)]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [StringLength(20)]
        [Display(Name = "Phy Verifier Code")]
        public string PhyVerifierCode { get; set; }

        [StringLength(1000)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        // ── Table rows ─────────────────────────────────────────────
        public List<ItStockRowDto> Rows { get; set; } = new List<ItStockRowDto>();

        // ── UI state ───────────────────────────────────────────────
        public bool ShowForm { get; set; }   // true after "Insert/Update record" clicked
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    // ────────────────────────────────────────────────────────────────
    //  ItStockRowDto  –  one row in the table grid
    // ────────────────────────────────────────────────────────────────
    public class ItStockRowDto
    {
        public int Id { get; set; }
        public int? ENo { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
        public string RecDept { get; set; }
        public string RecDeptName { get; set; }
        public DateTime? IssueDt { get; set; }
        public DateTime? TransferInOutDt { get; set; }
        public string Grade { get; set; }
        public string CurrLoc { get; set; }
        public string PrevLoc { get; set; }
        public string StType { get; set; }
        public string PTag { get; set; }
        public string AssetCode { get; set; }
        public string PvCode { get; set; }
        public string McNum { get; set; }
        public string IpNo { get; set; }
        public string PhoneNo { get; set; }
        public string Remarks { get; set; }
    }
}