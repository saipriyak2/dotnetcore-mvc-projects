using CompetencyPMS.Models;
using DL_CompetencyPMS.Abstract;
using DL_CompetencyPMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;



namespace CompetencyPMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleManagementController : Controller
    {

        IRoleRepository _repo = new RoleRepository();

        [HttpGet]
        public IActionResult Index()
        {
            List<RoleInfo> lst = _repo.GetAllRoleDetails();

            return View(lst);
        }
        [HttpGet]
        public IActionResult Create()
        {
            RoleRepository rolerepo = new RoleRepository();
            var roles = rolerepo.GetAllRoleDetails()
                .Select(r => new
                {
                    RoleId = r.RoleID,
                    DisplayText = $"{r.RoleID} - {r.RoleName}"
                })
                .ToList();
            ViewBag.RoleId = new SelectList(roles, "RoleId", "DisplayText");
                
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleInfo rl)
        {
            string msg = _repo.InsertRoleData(rl);
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
            RoleInfo rl = _repo.GetRoleOnRoleID(Convert.ToInt32(id));
            return View(rl);
        }

        [HttpPost]
        public IActionResult Edit(RoleInfo ri)
        {
            string msg = _repo.UpdateRoleData(ri);
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
            RoleInfo r_ = _repo.GetRoleOnRoleID(Convert.ToInt32(id));
            return View(r_);
        }
       


    }
    }

