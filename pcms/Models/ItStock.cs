using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMS.Models
{
    [Table("ItStock")]
    public class ItStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // ── Lot reference ──────────────────────────────────────────
        [Required]
        [StringLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }          // FK to LotMaster.LotNo

        // ── Serial / record number within the lot ──────────────────
        [Display(Name = "E No")]
        public int? ENo { get; set; }              // sequential entry no. within lot

        // ── Employee (current holder) ──────────────────────────────
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

        // ── Receiving employee ─────────────────────────────────────
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

        // ── Issue / transfer dates ─────────────────────────────────
        [DataType(DataType.Date)]
        [Display(Name = "Issue Date")]
        public DateTime? IssueDt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transfer In/Out Date")]
        public DateTime? TransferInOutDt { get; set; }

        // ── Grade / classification ─────────────────────────────────
        [StringLength(10)]
        [Display(Name = "Grade")]
        public string Grade { get; set; }

        // ── Location ───────────────────────────────────────────────
        [StringLength(200)]
        [Display(Name = "Current Location")]
        public string CurrLoc { get; set; }

        [StringLength(200)]
        [Display(Name = "Previous Location")]
        public string PrevLoc { get; set; }

        // ── Asset / stock type ─────────────────────────────────────
        /// <summary>'CR' = Computer Room, 'SF' = Staff etc.</summary>
        [StringLength(10)]
        [Display(Name = "St Type")]
        public string StType { get; set; }

        /// <summary>'O' = Old, 'N' = New</summary>
        [StringLength(1)]
        [Display(Name = "Pool Tag")]
        public string PoolTag { get; set; }

        // ── Identifiers ────────────────────────────────────────────
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

        // ── Remarks ────────────────────────────────────────────────
        [StringLength(1000)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        // ── Navigation property (optional) ─────────────────────────
        [ForeignKey("LotNo")]
        public virtual LotMaster LotMaster { get; set; }
    }
}