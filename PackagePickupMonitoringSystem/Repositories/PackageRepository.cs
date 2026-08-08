using PackagePickupMonitoringSystem.Models;

namespace PackagePickupMonitoringSystem.Repositories
{
    public static class PackageRepository
    {
        private static List<Package> _packages = new List<Package>(); 
        private static int _nextId = 1;

        public static IEnumerable<Package> GetAll() => _packages;

        public static Package GetById(int id) => _packages.FirstOrDefault(p => p.Id == id);

        public static void Add(Package package)
        {
            package.Id = _nextId++;
            _packages.Add(package);
        }

        public static void Update(Package package)
        {
            var existing = GetById(package.Id);
            if (existing != null)
            {
                existing.TrackingNumber = package.TrackingNumber;
                existing.RecipientName = package.RecipientName;
                existing.UnitNumber = package.UnitNumber;
                existing.ContactNumber = package.ContactNumber;
                existing.CourierCompany = package.CourierCompany;
                existing.PackageType = package.PackageType;
                existing.ExpectedPickupDate = package.ExpectedPickupDate;
                existing.Notes = package.Notes;
            }
        }

        public static void MarkAsClaimed(int id, string receivedBy)
        {
            var package = GetById(id);
            if (package != null)
            {
                package.Status = "Claimed"; 
                package.ClaimedDateTime = DateTime.Now;
                package.ReceivedBy = receivedBy;
            }
        }
    }
}