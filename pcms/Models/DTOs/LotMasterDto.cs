using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PCMS.DTOs
{
    public class LotMasterDto
    {
        [Required(ErrorMessage = "Lot No. is required.")]
        [StringLength(50, ErrorMessage = "Lot No. cannot exceed 50 characters.")]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; } 

        [Required(ErrorMessage = "Equipment Type is required.")]
        [Display(Name = "Equip Type")]
        public string EquipType { get; set; }

        [Display(Name = "Equip. Sub Type")]
        public string EquipSubType { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int? Quantity { get; set; }

        [Display(Name = "Warranty Clause")]
        public string WarrantyClause { get; set; }

        [Display(Name = "Warranty Start Date")]
        [DataType(DataType.Date)]
        public DateTime? WarrantyStartDate { get; set; }

        [Display(Name = "Warranty End Date")]
        [DataType(DataType.Date)]
        public DateTime? WarrantyEndDate { get; set; }

        [Display(Name = "AMC Vendor")]
        public string AmcVendor { get; set; }

        [Display(Name = "AMC Start Date")]
        [DataType(DataType.Date)]
        public DateTime? AmcStartDate { get; set; }

        [Display(Name = "AMC End Date")]
        [DataType(DataType.Date)]
        public DateTime? AmcEndDate { get; set; }

        [Display(Name = "PO Vendor")]
        public string PoVendor { get; set; }

        [Display(Name = "PO No")]
        public string PoNo { get; set; }

        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        public DateTime? PoDate { get; set; }

        [Display(Name = "Spec 1")]
        public string Spec1 { get; set; }

        [Display(Name = "Spec 2")]
        public string Spec2 { get; set; }

        [Display(Name = "Spec 3")]
        public string Spec3 { get; set; }

        [Display(Name = "Spec 4")]
        public string Spec4 { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Capitalisation Date")]
        [DataType(DataType.Date)]
        public DateTime? CapitalisationDate { get; set; }

        // Dropdowns populated by controller
        public IEnumerable<SelectListItem> EquipTypeList { get; set; }
        public IEnumerable<SelectListItem> EquipSubTypeList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        public IEnumerable<SelectListItem> WarrantyClauseList { get; set; }

        // Toast/alert messages
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        // Flag: true when we loaded an existing record via the Go button
        public bool IsLoaded { get; set; }
    }
}
