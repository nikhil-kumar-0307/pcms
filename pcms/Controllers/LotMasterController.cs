using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PCMS.DTOs;
using PCMS.Models;
using pcms.Data;

namespace pcms.Controllers
{
    [Authorize]
    public class LotMasterController : Controller
    {
        private readonly PcmsDbContext _db;

        public LotMasterController()
        {
            _db = new PcmsDbContext();
        }

        // ---------------------------------------------------------------
        // GET: /LotMaster/Index
        //      Optional ?lotNo=C25 to pre-load an existing record
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Index(string lotNo)
        {
            var dto = new LotMasterDto();
            PopulateDropdowns(dto);

            if (!string.IsNullOrWhiteSpace(lotNo))
            {
                var record = _db.LotMasters.Find(lotNo.Trim().ToUpper());
                if (record != null)
                {
                    MapToDto(record, dto);
                    dto.IsLoaded = true;
                }
                else
                {
                    dto.LotNo = lotNo.Trim().ToUpper();   // keep what user typed
                    dto.ErrorMessage = $"Lot No. '{lotNo}' not found.";
                }
            }

            return View(dto);
        }

        // ---------------------------------------------------------------
        // POST: /LotMaster/Insert  — saves a new lot record
        // ---------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(LotMasterDto dto)
        {
            PopulateDropdowns(dto);

            if (!ModelState.IsValid)
            {
                dto.ErrorMessage = "Please fix the errors highlighted below.";
                return View("Index", dto);
            }

            // Normalise Lot No. to uppercase
            dto.LotNo = dto.LotNo.Trim().ToUpper();

            // Check for duplicate Lot No.
            if (_db.LotMasters.Find(dto.LotNo) != null)
            {
                ModelState.AddModelError("LotNo", $"Lot No. '{dto.LotNo}' already exists.");
                dto.ErrorMessage = $"Lot No. '{dto.LotNo}' already exists. Use a different number.";
                return View("Index", dto);
            }

            try
            {
                var lot = new LotMaster
                {
                    LotNo = dto.LotNo,
                    EquipType = dto.EquipType,
                    EquipSubType = dto.EquipSubType,
                    BrandName = dto.BrandName,
                    ModelName = dto.ModelName,
                    Quantity = dto.Quantity,
                    WarrantyClause = dto.WarrantyClause,
                    WarrantyStartDate = dto.WarrantyStartDate,
                    WarrantyEndDate = dto.WarrantyEndDate,
                    AmcVendor = dto.AmcVendor,
                    AmcStartDate = dto.AmcStartDate,
                    AmcEndDate = dto.AmcEndDate,
                    PoVendor = dto.PoVendor,
                    PoNo = dto.PoNo,
                    PoDate = dto.PoDate,
                    Spec1 = dto.Spec1,
                    Spec2 = dto.Spec2,
                    Spec3 = dto.Spec3,
                    Spec4 = dto.Spec4,
                    Remarks = dto.Remarks,
                    CapitalisationDate = dto.CapitalisationDate
                };

                _db.LotMasters.Add(lot);
                _db.SaveChanges();

                var fresh = new LotMasterDto
                {
                    SuccessMessage = $"Lot '{lot.LotNo}' inserted successfully."
                };
                PopulateDropdowns(fresh);
                return View("Index", fresh);
            }
            catch (Exception ex)
            {
                dto.ErrorMessage = "An error occurred while saving: " + ex.Message;
                return View("Index", dto);
            }
        }

        // ---------------------------------------------------------------
        // GET: /LotMaster/Load?lotNo=C25  — search redirect
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Load(string lotNo)
        {
            if (string.IsNullOrWhiteSpace(lotNo))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { lotNo = lotNo.Trim() });
        }

        // ---------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }

        private void PopulateDropdowns(LotMasterDto dto)
        {
            dto.EquipTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "",          Text = "-Select Equipment-", Selected = true },
                new SelectListItem { Value = "Computer",  Text = "Computer" },
                new SelectListItem { Value = "Laptop",    Text = "Laptop" },
                new SelectListItem { Value = "Printer",   Text = "Printer" },
                new SelectListItem { Value = "Scanner",   Text = "Scanner" },
                new SelectListItem { Value = "UPS",       Text = "UPS" },
                new SelectListItem { Value = "Server",    Text = "Server" },
                new SelectListItem { Value = "Switch",    Text = "Switch" },
                new SelectListItem { Value = "Router",    Text = "Router" },
            };

            dto.EquipSubTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "",           Text = "-Select Sub Type-", Selected = true },
                new SelectListItem { Value = "Desktop",    Text = "Desktop" },
                new SelectListItem { Value = "Tower",      Text = "Tower" },
                new SelectListItem { Value = "All-in-One", Text = "All-in-One" },
                new SelectListItem { Value = "Laser",      Text = "Laser" },
                new SelectListItem { Value = "Inkjet",     Text = "Inkjet" },
                new SelectListItem { Value = "Dot Matrix", Text = "Dot Matrix" },
            };

            dto.BrandList = new List<SelectListItem>
            {
                new SelectListItem { Value = "",       Text = "-Select Brand-", Selected = true },
                new SelectListItem { Value = "HP",     Text = "HP" },
                new SelectListItem { Value = "Dell",   Text = "Dell" },
                new SelectListItem { Value = "Lenovo", Text = "Lenovo" },
                new SelectListItem { Value = "Acer",   Text = "Acer" },
                new SelectListItem { Value = "Asus",   Text = "Asus" },
                new SelectListItem { Value = "Canon",  Text = "Canon" },
                new SelectListItem { Value = "Epson",  Text = "Epson" },
            };

            dto.WarrantyClauseList = new List<SelectListItem>
            {
                new SelectListItem { Value = "",              Text = "Type",          Selected = true },
                new SelectListItem { Value = "Onsite",        Text = "Onsite" },
                new SelectListItem { Value = "Carry-In",      Text = "Carry-In" },
                new SelectListItem { Value = "Comprehensive", Text = "Comprehensive" },
                new SelectListItem { Value = "None",          Text = "None" },
            };
        }

        private static void MapToDto(LotMaster src, LotMasterDto dto)
        {
            dto.LotNo = src.LotNo;
            dto.EquipType = src.EquipType;
            dto.EquipSubType = src.EquipSubType;
            dto.BrandName = src.BrandName;
            dto.ModelName = src.ModelName;
            dto.Quantity = src.Quantity;
            dto.WarrantyClause = src.WarrantyClause;
            dto.WarrantyStartDate = src.WarrantyStartDate;
            dto.WarrantyEndDate = src.WarrantyEndDate;
            dto.AmcVendor = src.AmcVendor;
            dto.AmcStartDate = src.AmcStartDate;
            dto.AmcEndDate = src.AmcEndDate;
            dto.PoVendor = src.PoVendor;
            dto.PoNo = src.PoNo;
            dto.PoDate = src.PoDate;
            dto.Spec1 = src.Spec1;
            dto.Spec2 = src.Spec2;
            dto.Spec3 = src.Spec3;
            dto.Spec4 = src.Spec4;
            dto.Remarks = src.Remarks;
            dto.CapitalisationDate = src.CapitalisationDate;
        }
    }
}
