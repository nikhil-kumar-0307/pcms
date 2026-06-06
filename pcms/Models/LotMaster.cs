using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMS.Models
{
    [Table("LotMaster")]
    public class LotMaster
    {
        [Key]
        [Required]
        [StringLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }   // e.g. C25, C26, L01

        [Required]
        [StringLength(100)]
        [Display(Name = "Equip Type")]
        public string EquipType { get; set; }

        [StringLength(100)]
        [Display(Name = "Equip. Sub Type")]
        public string EquipSubType { get; set; }

        [StringLength(100)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [StringLength(200)]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [StringLength(50)]
        [Display(Name = "Warranty Clause")]
        public string WarrantyClause { get; set; }

        [Display(Name = "Warranty Start Date")]
        [DataType(DataType.Date)]
        public DateTime? WarrantyStartDate { get; set; }

        [Display(Name = "Warranty End Date")]
        [DataType(DataType.Date)]
        public DateTime? WarrantyEndDate { get; set; }

        [StringLength(200)]
        [Display(Name = "AMC Vendor")]
        public string AmcVendor { get; set; }

        [Display(Name = "AMC Start Date")]
        [DataType(DataType.Date)]
        public DateTime? AmcStartDate { get; set; }

        [Display(Name = "AMC End Date")]
        [DataType(DataType.Date)]
        public DateTime? AmcEndDate { get; set; }

        [StringLength(200)]
        [Display(Name = "PO Vendor")]
        public string PoVendor { get; set; }

        [StringLength(100)]
        [Display(Name = "PO No")]
        public string PoNo { get; set; }

        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        public DateTime? PoDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Spec 1")]
        public string Spec1 { get; set; }

        [StringLength(500)]
        [Display(Name = "Spec 2")]
        public string Spec2 { get; set; }

        [StringLength(500)]
        [Display(Name = "Spec 3")]
        public string Spec3 { get; set; }

        [StringLength(500)]
        [Display(Name = "Spec 4")]
        public string Spec4 { get; set; }

        [StringLength(1000)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Capitalisation Date")]
        [DataType(DataType.Date)]
        public DateTime? CapitalisationDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }
    }
}
