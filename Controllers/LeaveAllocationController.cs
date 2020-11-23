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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LeaveMgmt.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly iLeaveAllocationRepository _repo;
        private readonly iLeaveTypeRepository _repoLeaveType;
        private readonly IMapper _mapper;
        private readonly UserManager<Person> _people;

        public LeaveAllocationController(iLeaveAllocationRepository repo, iLeaveTypeRepository repoLeaveType, IMapper mapper, UserManager<Person> userManager)
        {
            _repo = repo;
            _repoLeaveType = repoLeaveType;
            _mapper = mapper;
            _people = userManager;
        }

        public ActionResult Index()
        {
            var leaveTypes = _repoLeaveType.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }
        // GET: LeaveAllocationController
        public ActionResult IndexList()
        {
            var leaveAllocations = _repo.FindAll().ToList();
            var leaveTypes = _repoLeaveType.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
        };
            return View(model);
        }

        public ActionResult SetLeave(int id)
        {
            var leaveType = _repoLeaveType.FindById(id);
            var people = _people.GetUsersInRoleAsync("Member").Result;
            //int numUpdated = 0;

                foreach(var p in people)
            {
                if (_repo.CheckAllocation(id, p.Id))
                    continue;   //continue means skip this record 
                // else create a new record
                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    PersonId = p.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year
                };
                var leaveAllocation = _mapper.Map <LeaveAllocation>(allocation);
                _repo.Create(leaveAllocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListPeople()
        {
            var people = _people.GetUsersInRoleAsync("Member").Result;
            var model = _mapper.Map<List<PersonVM>>(people);
            return View(model);
        }
        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(string id)
        {
            var person = _mapper.Map<PersonVM>( _people.FindByIdAsync(id).Result);
            var period = DateTime.Now.Year;
            var allocations = _mapper.Map<List<LeaveAllocationVM>>(_repo.GetLeaveAllocationsByPerson(id));
            var model = new ViewAllocationVM
            {
                Person = person,
                LeaveAllocations = allocations
            };
            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
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

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveAllocation = _repo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveAllocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _repo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
               // var allocation = _mapper.Map<LeaveAllocation>(record);
                var isSuccess = _repo.Update(record);

                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }


                return RedirectToAction(nameof(Details), new { id = model.PersonId});
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
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
