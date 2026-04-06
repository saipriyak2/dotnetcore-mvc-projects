using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompetencyPMS.Controllers
{
    public class CaseStudyController : Controller
    {
        ICaseStudySolutionRepo _repo = new CaseStudySolutionRepo();
        [HttpGet]
        public IActionResult Index()
        {
            List<CaseStudySolutionInfo> lst = _repo.GetAllCaseStudyDetails();
            return View(lst);
        }

        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public IActionResult Create(CaseStudySolutionInfo ci)
        {
            string msg = _repo.InsertCaseStudyData(ci);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                string info = "Data not saved, try again latter";
            }

            return View();

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            CaseStudySolutionInfo cs = _repo.GetCSOnCSID(Convert.ToInt32(id));
            return View(cs);
        }

        [HttpPost]
        public IActionResult Edit(CaseStudySolutionInfo ci)
        {
            string msg = _repo.UpdateCSData(ci);
            if (msg == "success")
            {
                return RedirectToAction("Index");
            }
            else
            {
                string info = "Data not saved, try again latter";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            CaseStudySolutionInfo cc_ = _repo.GetCSOnCSID(Convert.ToInt32(id));
            return View(cc_);
        }
    }
}

    //[HttpGet]
    //public IActionResult Create()
    //{
    //    RoleRepository rolerepo = new RoleRepository();
    //    var roles = rolerepo.GetAllRoleDetails()
    //        .Select(r => new
    //        {
    //            RoleId = r.RoleID,
    //            DisplayText = $"{r.RoleID} - {r.RoleName}"
    //        })
    //        .ToList();
    //    ViewBag.RoleId = new SelectList(roles, "RoleId", "DisplayText");

    //    return View();
    //}
    //[HttpPost]
    //public IActionResult Create(RoleInfo rl)
    //{
    //    string msg = _repo.InsertRoleData(rl);
    //    if (msg == "success")
    //    {
    //        return RedirectToAction("Index");
    //    }
    //    else
    //    {
    //        string info = "Data not saved, try again latter";
    //    }

    //    return View();

    //}
