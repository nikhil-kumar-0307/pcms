using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCMS.DTOs
{
    // ═══════════════════════════════════════════════════════════════════
    //  RegisterServiceRequisitionEmployeeNumberDto
    //  Bound to the landing widget — just holds the employee number
    //  that the user types before clicking "Go".
    //  Lives in the ServiceRequisition folder / feature area.
    // ═══════════════════════════════════════════════════════════════════
    public class RegisterServiceRequisitionEmployeeNumberDto
    {
        // ── Input ──────────────────────────────────────────────────
        [Display(Name = "Emp No")]
        [StringLength(20)]
        public string EmpNo { get; set; }

        // ── Result rows (populated after Go is clicked) ────────────
        public List<AssetHolderRowDto> AssetRows { get; set; }
            = new List<AssetHolderRowDto>();

        // ── UI state ───────────────────────────────────────────────
        public bool ResultsLoaded { get; set; }
        public string ErrorMessage { get; set; }

        // ── Employee summary (shown in result header) ──────────────
        public string EmpName { get; set; }
        public int TotalAssets { get; set; }
    }

    // ═══════════════════════════════════════════════════════════════════
    //  AssetHolderRowDto
    //  One row in the "Asset Holder Details" grid (matches the image).
    // ═══════════════════════════════════════════════════════════════════
    public class AssetHolderRowDto
    {
        public int Id { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }

        /// <summary>Clickable machine / lot number (e.g. R04075, C23014)</summary>
        public string McNum { get; set; }
        public string LotNo { get; set; }

        public string MachineName { get; set; }     // Machine Name column
        public string Grade { get; set; }
        public string Dept { get; set; }
        public string Location { get; set; }     // CurrLoc
        public string IpAdd { get; set; }     // IpNo
        public string SiNum { get; set; }
    }
}