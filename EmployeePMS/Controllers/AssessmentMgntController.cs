using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Model;
using DL_CompetencyPMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompetencyPMS.Controllers
{
    public class AssessmentMgntController : Controller
    {
        IAssessmentRepo _repo = new AssessmentRepo();

        [HttpGet]
        public IActionResult Index()
        {
            List<AssessmentInfo> lst = _repo.GetAllAssessments();

            return View(lst);
        }
    

    [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AssessmentInfo ai)
        {
            string msg = _repo.InsertAssessmentData(ai);
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
            AssessmentInfo a = _repo.GetAssessmentOnID(Convert.ToInt32(id));
            return View(a);
        }
        [HttpPost]
        public IActionResult Edit(AssessmentInfo a)
        {
            string msg = _repo.UpdateAssessmentData(a);
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
        public IActionResult Delete(int id)
        {
            string msg = _repo.DeleteAssessmentData(id);
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
            AssessmentInfo aa_ = _repo.GetAssessmentOnID(Convert.ToInt32(id));
            return View(aa_);
        }

    }
}
