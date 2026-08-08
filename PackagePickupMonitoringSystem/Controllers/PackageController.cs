using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackagePickupMonitoringSystem.Models;
using PackagePickupMonitoringSystem.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PackagePickupMonitoringSystem.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        public IActionResult Index(string searchQuery)
        {
            List<Package> packages = PackageRepository.GetAll();

            if (string.IsNullOrEmpty(searchQuery) == false)
            {
                packages = packages.Where(p =>
                    p.TrackingNumber.ToLower().Contains(searchQuery.ToLower()) ||
                    p.RecipientName.ToLower().Contains(searchQuery.ToLower())).ToList();
            }

            return View(packages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Package/Create
        [HttpPost]
        public IActionResult Create(Package p)
        {
            if (ModelState.IsValid)
            {
                PackageRepository.Add(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Package p = PackageRepository.GetById(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Package p)
        {
            if (ModelState.IsValid)
            {
                PackageRepository.Update(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public IActionResult Details(int id)
        {
            Package p = PackageRepository.GetById(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Claim(int id)
        {
            Package p = PackageRepository.GetById(id);

            if (p == null || p.Status == "Claimed")
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost]
        public IActionResult Claim(int id, string receivedBy)
        {
            if (string.IsNullOrEmpty(receivedBy))
            {
                ModelState.AddModelError("ReceivedBy", "Please type the name of the person receiving it.");
                Package p = PackageRepository.GetById(id);
                return View(p);
            }

            PackageRepository.MarkAsClaimed(id, receivedBy);
            return RedirectToAction("Index");
        }
    }
}