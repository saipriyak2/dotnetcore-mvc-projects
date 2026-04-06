
using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CompetencyPMS.Controllers
{
    public class CompetencyMgmtController : Controller
    {
        ICompetencyRepo _repo = new CompetencyRepo();

        [HttpGet]
        public IActionResult Index()
        {
            List<CompetencyInfo> lst = _repo.GetAllCompetensies();

            return View(lst);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CompetencyInfo ci)
        {
            string msg = _repo.InsertCompetencyData(ci);
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
            CompetencyInfo c = _repo.GetCompetencyOnID(Convert.ToInt32(id));
            return View(c);
        }
        [HttpPost]
        public IActionResult Edit(CompetencyInfo ci)
        {
            string msg = _repo.UpdateCompetencyData(ci);
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
            CompetencyInfo c_ = _repo.GetCompetencyOnID(Convert.ToInt32(id));
            return View(c_);
        }
    }
}
