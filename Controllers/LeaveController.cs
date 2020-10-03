using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using LeaveMgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMgmt.Controllers
{
    public class LeaveController : Controller
    {
        private readonly iLeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveController(iLeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveController
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<DetailLeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        // GET: LeaveController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
