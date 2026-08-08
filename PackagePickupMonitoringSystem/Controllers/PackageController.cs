using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackagePickupMonitoringSystem.Models;
using PackagePickupMonitoringSystem.Repositories;

namespace PackagePickupMonitoringSystem.Controllers
{
    [Authorize] 
    public class PackageController : Controller
    {
        public IActionResult Index(string searchQuery)
        {
            var packages = PackageRepository.GetAll();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                packages = packages.Where(p =>
                    p.TrackingNumber.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.RecipientName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(packages);
        }

        [HttpGet]
        public IActionResult Create() => View(); 

        [HttpPost]
        public IActionResult Create(Package package)
        {
            if (ModelState.IsValid) 
            {
                PackageRepository.Add(package);
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var package = PackageRepository.GetById(id);
            if (package == null) return NotFound();
            return View(package);
        }

        [HttpPost]
        public IActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                PackageRepository.Update(package);
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        public IActionResult Details(int id) 
        {
            var package = PackageRepository.GetById(id);
            if (package == null) return NotFound();
            return View(package);
        }

        [HttpGet]
        public IActionResult Claim(int id) 
        {
            var package = PackageRepository.GetById(id);
            if (package == null || package.Status == "Claimed") return NotFound();
            return View(package);
        }

        [HttpPost]
        public IActionResult Claim(int id, string receivedBy)
        {
            if (string.IsNullOrEmpty(receivedBy))
            {
                ModelState.AddModelError("ReceivedBy", "Please specify who received the package.");
                return View(PackageRepository.GetById(id));
            }

            PackageRepository.MarkAsClaimed(id, receivedBy);
            return RedirectToAction(nameof(Index));
        }
    }
}