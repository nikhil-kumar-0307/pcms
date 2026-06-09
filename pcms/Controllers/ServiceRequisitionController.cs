using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PCMS.DTOs;
using PCMS.Models;
using pcms.Data;

namespace pcms.Controllers
{
    // ═══════════════════════════════════════════════════════════════════
    //  ServiceRequisitionController
    //  Handles all Service Requisition feature actions.
    //  Route prefix: /ServiceRequisition/
    //
    //  Actions in this file:
    //    • Index               — landing page (shows the Emp No input widget)
    //    • AssetHolderDetails  — GET: load asset rows for a given EmpNo
    //
    //  More actions (Register SR, View SR list, etc.) will be added later
    //  in separate partial files or appended here.
    // ═══════════════════════════════════════════════════════════════════
    [Authorize]
    public class ServiceRequisitionController : Controller
    {
        private readonly PcmsDbContext _db;

        public ServiceRequisitionController()
        {
            _db = new PcmsDbContext();
        }

        // ---------------------------------------------------------------
        // GET  /ServiceRequisition/Index
        //      Landing page — shows the "Register Service Requisition" widget
        //      with the Emp No. input + Go button.
        //      If ?empNo= is supplied the results are pre-loaded.
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult Index(string empNo = null)
        {
            var dto = new RegisterServiceRequisitionEmployeeNumberDto
            {
                EmpNo = empNo?.Trim().ToUpper()
            };

            if (!string.IsNullOrWhiteSpace(dto.EmpNo))
                PopulateAssets(dto);

            return View("RegisterServiceRequisitionEmployeeNumber", dto);
        }

        // ---------------------------------------------------------------
        // GET  /ServiceRequisition/AssetHolderDetails?empNo=090983
        //      Called when the user clicks "Go" on the widget.
        //      Redirects back to Index with the empNo query-string so the
        //      URL is bookmarkable and the Back button works correctly.
        // ---------------------------------------------------------------
        [HttpGet]
        public ActionResult AssetHolderDetails(string empNo)
        {
            if (string.IsNullOrWhiteSpace(empNo))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { empNo = empNo.Trim().ToUpper() });
        }

        // ---------------------------------------------------------------
        // POST /ServiceRequisition/AssetHolderDetails
        //      Handles the <form> submission from the Go button.
        // ---------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssetHolderDetails(RegisterServiceRequisitionEmployeeNumberDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EmpNo))
                return RedirectToAction("Index");

            return RedirectToAction("Index", new { empNo = dto.EmpNo.Trim().ToUpper() });
        }

        // ---------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }

        // ═══════════════════════ private helpers ════════════════════════

        /// <summary>
        /// Queries ItStock joined with LotMaster to build the asset-holder
        /// grid rows for the given employee number.
        /// </summary>
        private void PopulateAssets(RegisterServiceRequisitionEmployeeNumberDto dto)
        {
            // Normalise
            var empNo = dto.EmpNo.Trim().ToUpper();

            // Pull matching stock rows; join LotMaster for machine name
            var rows = (from s in _db.ItStocks
                        join l in _db.LotMasters on s.LotNo equals l.LotNo into lj
                        from lot in lj.DefaultIfEmpty()
                        where s.EmpNo == empNo
                        orderby s.ENo
                        select new AssetHolderRowDto
                        {
                            Id = s.Id,
                            EmpNo = s.EmpNo,
                            EmpName = s.EmpName,
                            McNum = s.McNum,
                            LotNo = s.LotNo,
                            // Machine Name = Equipment Type + Model from LotMaster
                            MachineName = (lot != null)
                                          ? (lot.EquipType + " " + lot.ModelName).Trim()
                                          : string.Empty,
                            Grade = s.Grade,
                            Dept = s.Dept,
                            Location = s.CurrLoc,
                            IpAdd = s.IpNo,
                            SiNum = s.SiNum
                        }).ToList();

            if (rows.Count == 0)
            {
                dto.ErrorMessage = $"No asset records found for Employee No. '{empNo}'.";
                dto.ResultsLoaded = false;
                return;
            }

            dto.AssetRows = rows;
            dto.EmpName = rows.First().EmpName;
            dto.TotalAssets = rows.Count;
            dto.ResultsLoaded = true;
        }
    }
}