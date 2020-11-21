using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using LeaveMgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMgmt.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LeaveTypesController : Controller
    {
        private readonly iLeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(iLeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveController
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        // GET: LeaveController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(model);
        }

        // GET: LeaveController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            if(!ModelState.IsValid)
            { return View(model); }
            var leaveType = _mapper.Map <LeaveType>(model);
            leaveType.DateCreated = DateTime.Now;

            var isSuccess = _repo.Create(leaveType);
            if(!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: LeaveController/Edit/5
        public ActionResult Edit(int id)
        {
            if(!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(model);
        }

        // POST: LeaveController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Caught something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var success = _repo.Delete(leaveType);
            if (!success)
            {
                return View(leaveType);
            }
            return RedirectToAction(nameof(Index));

        }
        #region archive Delete Post
        //// POST: LeaveController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, LeaveTypeVM model)
        //{
        //    try
        //    {
        //        if (!_repo.isExists(id))
        //        {
        //            return NotFound();
        //        }
        //        var leaveType = _repo.FindById(id);
        //        if(leaveType == null)
        //        {
        //            return NotFound();
        //        }
        //        var success = _repo.Delete(leaveType);
        //        if (!success)
        //        {
        //            return View(leaveType);
        //        }
        //            return RedirectToAction(nameof(Index));

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion
    }
}
