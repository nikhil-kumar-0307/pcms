// pcms/Controllers/AssetHolderController.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using pcms.Models.DTOs;

namespace pcms.Controllers
{
    public class AssetHolderController : Controller
    {
        // ── change this to your actual connection string name ──
        private readonly string _conn =
            System.Configuration.ConfigurationManager
                  .ConnectionStrings["PcmsDbContext"].ConnectionString;

        // GET: /AssetHolder/Index
        // Shows the search form (empty on first load)
        public ActionResult Index()
        {
            ViewBag.Query = "";
            ViewBag.Results = new List<AssetHolderDTO>();
            return View();
        }

        // POST: /AssetHolder/Index
        // Accepts empNo OR name, returns matching rows
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchQuery)
        {
            searchQuery = (searchQuery ?? "").Trim();
            ViewBag.Query = searchQuery;

            var results = new List<AssetHolderDTO>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Search by EmpNo (exact) OR EmpName (partial, case-insensitive)
                const string sql = @"
                    SELECT
                        EmpNo, EmpName, McNum, PTag, Grade,
                        Dept, DeptName, CurrLoc, IpNo, SiNum,
                        IssueDt, RecEmpNo, RecEmpName, AssetCode,
                        LotNo, StType, PoolTag, Remarks
                    FROM ItStock
                    WHERE EmpNo = @q
                       OR EmpName LIKE '%' + @q + '%'
                    ORDER BY IssueDt DESC";

                using (var con = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@q", searchQuery);
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            results.Add(new AssetHolderDTO
                            {
                                EmpNo = rdr["EmpNo"]?.ToString(),
                                EmpName = rdr["EmpName"]?.ToString(),
                                McNum = rdr["McNum"]?.ToString(),
                                PTag = rdr["PTag"]?.ToString(),
                                Grade = rdr["Grade"]?.ToString(),
                                Dept = rdr["Dept"]?.ToString(),
                                DeptName = rdr["DeptName"]?.ToString(),
                                CurrLoc = rdr["CurrLoc"]?.ToString(),
                                IpNo = rdr["IpNo"]?.ToString(),
                                SiNum = rdr["SiNum"]?.ToString(),
                                IssueDt = rdr["IssueDt"] as DateTime?,
                                RecEmpNo = rdr["RecEmpNo"]?.ToString(),
                                RecEmpName = rdr["RecEmpName"]?.ToString(),
                                AssetCode = rdr["AssetCode"]?.ToString(),
                                LotNo = rdr["LotNo"]?.ToString(),
                                StType = rdr["StType"]?.ToString(),
                                PoolTag = rdr["PoolTag"]?.ToString(),
                                Remarks = rdr["Remarks"]?.ToString()
                            });
                        }
                    }
                }
            }

            ViewBag.Results = results;
            return View();
        }

        // ── IP Wise ────────────────────────────────────────────────────

        // GET: /AssetHolder/IpWise
        public ActionResult IpWise()
        {
            ViewBag.Query = "";
            ViewBag.Results = new List<IpWiseDTO>();
            return View();
        }

        // POST: /AssetHolder/IpWise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IpWise(string searchQuery)
        {
            searchQuery = (searchQuery ?? "").Trim();
            ViewBag.Query = searchQuery;

            var results = new List<IpWiseDTO>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                const string sql = @"
            SELECT
                McNum, EmpName, Dept, DeptName,
                CurrLoc, Grade, EmpNo, MacAddress, IpNo
            FROM ItStock
            WHERE IpNo LIKE '%' + @q + '%'
            ORDER BY McNum";

                using (var con = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@q", searchQuery);
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            results.Add(new IpWiseDTO
                            {
                                McNum = rdr["McNum"]?.ToString(),
                                MachineName = rdr["McNum"]?.ToString(),  
                                Dept = rdr["Dept"]?.ToString(),
                                CurrLoc = rdr["CurrLoc"]?.ToString(),
                                EmpName = rdr["EmpName"]?.ToString(),
                                Grade = rdr["Grade"]?.ToString(),
                                EmpNo = rdr["EmpNo"]?.ToString(),
                                MacAddress = rdr["MacAddress"]?.ToString(),
                                IpNo = rdr["IpNo"]?.ToString()
                            });
                        }
                    }
                }
            }



            ViewBag.Results = results;
            return View();
        }

        // GET: /AssetHolder/MachineWise
        public ActionResult MachineWise()
        {
            ViewBag.Query = "";
            ViewBag.Results = new List<MachineWiseDTO>();
            return View();
        }

        // POST: /AssetHolder/MachineWise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MachineWise(string searchQuery)
        {
            searchQuery = (searchQuery ?? "").Trim();
            ViewBag.Query = searchQuery;

            var results = new List<MachineWiseDTO>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                const string sql = @"
            SELECT
                McNum, EmpName, Dept, DeptName,
                CurrLoc, Grade, EmpNo, IpNo, MacAddress
            FROM ItStock
            WHERE McNum LIKE '%' + @q + '%'
            ORDER BY McNum";

                using (var con = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@q", searchQuery);
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            results.Add(new MachineWiseDTO
                            {
                                McNum = rdr["McNum"]?.ToString(),
                                MachineName = rdr["McNum"]?.ToString(),
                                Dept = rdr["Dept"]?.ToString(),
                                CurrLoc = rdr["CurrLoc"]?.ToString(),
                                EmpName = rdr["EmpName"]?.ToString(),
                                Grade = rdr["Grade"]?.ToString(),
                                EmpNo = rdr["EmpNo"]?.ToString(),
                                IpNo = rdr["IpNo"]?.ToString(),
                                MacAddress = rdr["MacAddress"]?.ToString()
                            });
                        }
                    }
                }
            }

            ViewBag.Results = results;
            return View();
        }
    }
}