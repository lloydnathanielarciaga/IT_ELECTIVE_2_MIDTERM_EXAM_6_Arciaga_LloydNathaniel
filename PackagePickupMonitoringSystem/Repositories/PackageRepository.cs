using PackagePickupMonitoringSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PackagePickupMonitoringSystem.Repositories
{
    public static class PackageRepository
    {
        private static List<Package> packageList = new List<Package>();
        private static int nextPkgId = 1;

        public static List<Package> GetAll()
        {
            return packageList;
        }

        public static Package GetById(int id)
        {
            return packageList.FirstOrDefault(p => p.Id == id);
        }

        public static void Add(Package pkg)
        {
            pkg.Id = nextPkgId;
            nextPkgId++;
            packageList.Add(pkg);
        }

        public static void Update(Package pkg)
        {
            Package oldPkg = GetById(pkg.Id);
            if (oldPkg != null)
            {
                oldPkg.TrackingNumber = pkg.TrackingNumber;
                oldPkg.RecipientName = pkg.RecipientName;
                oldPkg.UnitNumber = pkg.UnitNumber;
                oldPkg.ContactNumber = pkg.ContactNumber;
                oldPkg.CourierCompany = pkg.CourierCompany;
                oldPkg.PackageType = pkg.PackageType;
                oldPkg.ExpectedPickupDate = pkg.ExpectedPickupDate;
                oldPkg.Notes = pkg.Notes;
            }
        }

        public static void MarkAsClaimed(int id, string receiver)
        {
            Package p = GetById(id);
            if (p != null)
            {
                p.Status = "Claimed";
                p.ClaimedDateTime = DateTime.Now;
                p.ReceivedBy = receiver;
            }
        }
    }
}