using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CompetencyPMS.Controllers
{
    public class DesignationMgntController : Controller
    {
            IDesignationRepo _repo = new DesignationRepo();

            [HttpGet]
            public IActionResult Index()
            {
            List<DesignationInfo> lst = _repo.GetAllDesignations();

                return View(lst);
            }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DesignationInfo di)
        {
            string msg = _repo.InsertDesignationData(di);
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
            DesignationInfo d = _repo.GetDesignationOnID(Convert.ToInt32(id));
            return View(d);
        }
        [HttpPost]
        public IActionResult Edit(DesignationInfo di)
        {
            string msg = _repo.UpdateDesignationData(di);
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
            DesignationInfo d_ = _repo.GetDesignationOnID(Convert.ToInt32(id));
            return View(d_);
        }
    }
}
