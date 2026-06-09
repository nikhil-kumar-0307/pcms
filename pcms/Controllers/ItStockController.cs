using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PCMS.DTOs;
using PCMS.Models;
using pcms.Data;

namespace pcms.Controllers
{
    [Authorize]
    public class ItStockController : Controller
    {
        private readonly PcmsDbContext _db;

        public ItStockController()
        {
            _db = new PcmsDbContext();
        }

        // ---------------------------------------------------------------
        // GET: /ItStock/Index
        //      Optional ?lotNo=C25  to pre-load a lot
        //      Optional ?showForm=true  to keep the form open after redirect
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Index(string lotNo, bool showForm = false, string msg = null, bool isError = false)
        {
            var dto = new ItStockDto();

            if (!string.IsNullOrWhiteSpace(lotNo))
            {
                lotNo = lotNo.Trim().ToUpper();
                dto.LotNo = lotNo;
                LoadLotHeader(dto);

                if (dto.LotLoaded)
                {
                    dto.Rows = GetRows(lotNo);
                    dto.ShowForm = showForm;

                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (isError) dto.ErrorMessage = msg;
                        else dto.SuccessMessage = msg;
                    }
                }
                else
                {
                    dto.ErrorMessage = $"Lot No. '{lotNo}' not found.";
                }
            }

            return View(dto);
        }

        // ---------------------------------------------------------------
        // GET: /ItStock/Load?lotNo=C25   — search redirect
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Load(string lotNo)
        {
            if (string.IsNullOrWhiteSpace(lotNo))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { lotNo = lotNo.Trim() });
        }

        // ---------------------------------------------------------------
        // GET: /ItStock/ShowForm?lotNo=C25
        //      Reveals the entry form (Insert/Update record button)
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult ShowForm(string lotNo)
        {
            return RedirectToAction("Index", new { lotNo = lotNo, showForm = true });
        }

        // ---------------------------------------------------------------
        // GET: /ItStock/Edit/{id}?lotNo=C25
        //      Pre-fills the form for an existing record
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int id, string lotNo)
        {
            var record = _db.ItStocks.Find(id);
            if (record == null)
                return RedirectToAction("Index", new
                {
                    lotNo = lotNo,
                    showForm = true,
                    msg = "Record not found.",
                    isError = true
                });

            lotNo = (lotNo ?? record.LotNo).Trim().ToUpper();
            var dto = new ItStockDto { LotNo = lotNo };
            LoadLotHeader(dto);
            MapToDto(record, dto);
            dto.EditId = id;
            dto.Rows = GetRows(lotNo);
            dto.ShowForm = true;

            return View("Index", dto);
        }

        // ---------------------------------------------------------------
        // POST: /ItStock/Upsert   — insert or update
        // ---------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(ItStockDto dto)
        {
            // Re-hydrate lot header & rows (not posted)
            if (!string.IsNullOrWhiteSpace(dto.LotNo))
            {
                dto.LotNo = dto.LotNo.Trim().ToUpper();
                LoadLotHeader(dto);
                dto.Rows = GetRows(dto.LotNo);
                dto.ShowForm = true;
            }

            if (!ModelState.IsValid)
            {
                dto.ErrorMessage = "Please fix the errors highlighted below.";
                return View("Index", dto);
            }

            try
            {
                if (dto.EditId.HasValue && dto.EditId.Value > 0)
                {
                    // ── UPDATE ──────────────────────────────────────
                    var existing = _db.ItStocks.Find(dto.EditId.Value);
                    if (existing == null)
                    {
                        dto.ErrorMessage = "Record not found for update.";
                        return View("Index", dto);
                    }
                    MapFromDto(dto, existing);
                    _db.SaveChanges();

                    return RedirectToAction("Index", new
                    {
                        lotNo = dto.LotNo,
                        showForm = false,
                        msg = $"Record #{existing.ENo} updated successfully.",
                        isError = false
                    });
                }
                else
                {
                    // ── INSERT ──────────────────────────────────────
                    // Auto-assign next ENo within the lot
                    int nextENo = (_db.ItStocks
                        .Where(s => s.LotNo == dto.LotNo)
                        .Select(s => (int?)s.ENo)
                        .Max() ?? 0) + 1;

                    var stock = new ItStock { LotNo = dto.LotNo, ENo = nextENo };
                    MapFromDto(dto, stock);
                    _db.ItStocks.Add(stock);
                    _db.SaveChanges();

                    return RedirectToAction("Index", new
                    {
                        lotNo = dto.LotNo,
                        showForm = false,
                        msg = $"Record #{nextENo} inserted successfully.",
                        isError = false
                    });
                }
            }
            catch (Exception ex)
            {
                dto.ErrorMessage = "An error occurred while saving: " + ex.Message;
                return View("Index", dto);
            }
        }

        // ---------------------------------------------------------------
        // POST: /ItStock/Delete/{id}?lotNo=C25
        // ---------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string lotNo)
        {
            var record = _db.ItStocks.Find(id);
            if (record != null)
            {
                _db.ItStocks.Remove(record);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", new
            {
                lotNo = lotNo,
                showForm = false,
                msg = "Record deleted.",
                isError = false
            });
        }

        // ---------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }

        // ═══════════════════════════ helpers ════════════════════════════

        private void LoadLotHeader(ItStockDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.LotNo)) return;

            var lot = _db.LotMasters.Find(dto.LotNo);
            if (lot == null) { dto.LotLoaded = false; return; }

            dto.LotEquipType = lot.EquipType;
            dto.LotEquipTypeCode = "21";                         // adjust if you store a code
            dto.LotModel = lot.ModelName;
            dto.LotBrandName = lot.BrandName;
            dto.LotBrandCode = "17";                         // adjust if you store a code
            dto.TotalQty = lot.Quantity;
            dto.AllottedQty = _db.ItStocks.Count(s => s.LotNo == dto.LotNo);
            dto.LotLoaded = true;
        }

        private List<ItStockRowDto> GetRows(string lotNo)
        {
            return _db.ItStocks
                .Where(s => s.LotNo == lotNo)
                .OrderBy(s => s.ENo)
                .Select(s => new ItStockRowDto
                {
                    Id = s.Id,
                    ENo = s.ENo,
                    EmpNo = s.EmpNo,
                    EmpName = s.EmpName,
                    Dept = s.Dept,
                    DeptName = s.DeptName,
                    RecDept = s.RecDept,
                    RecDeptName = s.RecDeptName,
                    IssueDt = s.IssueDt,
                    TransferInOutDt = s.TransferInOutDt,
                    Grade = s.Grade,
                    CurrLoc = s.CurrLoc,
                    PrevLoc = s.PrevLoc,
                    StType = s.StType,
                    PTag = s.PTag,
                    AssetCode = s.AssetCode,
                    PvCode = s.PvCode,
                    McNum = s.McNum,
                    SiNum = s.SiNum,        // ← NEW
                    IpNo = s.IpNo,
                    PhoneNo = s.PhoneNo,
                    Remarks = s.Remarks,
                })
                .ToList();
        }

        private static void MapToDto(ItStock src, ItStockDto dto)
        {
            dto.ENo = src.ENo;
            dto.EmpNo = src.EmpNo;
            dto.EmpName = src.EmpName;
            dto.Dept = src.Dept;
            dto.DeptName = src.DeptName;
            dto.RecEmpNo = src.RecEmpNo;
            dto.RecEmpName = src.RecEmpName;
            dto.RecDept = src.RecDept;
            dto.RecDeptName = src.RecDeptName;
            dto.IssueDt = src.IssueDt;
            dto.TransferInOutDt = src.TransferInOutDt;
            dto.Grade = src.Grade;
            dto.CurrLoc = src.CurrLoc;
            dto.PrevLoc = src.PrevLoc;
            dto.StType = src.StType;
            dto.PoolTag = src.PoolTag;
            dto.AssetCode = src.AssetCode;
            dto.PvCode = src.PvCode;
            dto.McNum = src.McNum;
            dto.SiNum = src.SiNum;          // ← NEW
            dto.PTag = src.PTag;
            dto.IpNo = src.IpNo;
            dto.MacAddress = src.MacAddress;
            dto.PhoneNo = src.PhoneNo;
            dto.PhyVerifierCode = src.PhyVerifierCode;
            dto.Remarks = src.Remarks;
        }

        private static void MapFromDto(ItStockDto dto, ItStock dest)
        {
            dest.EmpNo = dto.EmpNo?.Trim();
            dest.EmpName = dto.EmpName?.Trim();
            dest.Dept = dto.Dept?.Trim();
            dest.DeptName = dto.DeptName?.Trim();
            dest.RecEmpNo = dto.RecEmpNo?.Trim();
            dest.RecEmpName = dto.RecEmpName?.Trim();
            dest.RecDept = dto.RecDept?.Trim();
            dest.RecDeptName = dto.RecDeptName?.Trim();
            dest.IssueDt = dto.IssueDt;
            dest.TransferInOutDt = dto.TransferInOutDt;
            dest.Grade = dto.Grade?.Trim();
            dest.CurrLoc = dto.CurrLoc?.Trim();
            dest.PrevLoc = dto.PrevLoc?.Trim();
            dest.StType = dto.StType?.Trim().ToUpper();
            dest.PoolTag = dto.PoolTag?.Trim().ToUpper();
            dest.AssetCode = dto.AssetCode?.Trim();
            dest.PvCode = dto.PvCode?.Trim();
            dest.McNum = dto.McNum?.Trim();
            dest.SiNum = dto.SiNum?.Trim();  // ← NEW
            dest.PTag = dto.PTag?.Trim();
            dest.IpNo = dto.IpNo?.Trim();
            dest.MacAddress = dto.MacAddress?.Trim();
            dest.PhoneNo = dto.PhoneNo?.Trim();
            dest.PhyVerifierCode = dto.PhyVerifierCode?.Trim();
            dest.Remarks = dto.Remarks?.Trim();
        }
    }
}