using System.ComponentModel.DataAnnotations;
using System;

namespace PackagePickupMonitoringSystem.Models
{
    public class Package
    {
        public int Id { get; set; } 

        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; } 

        [Required]
        [Display(Name = "Recipient Name")]
        public string RecipientName { get; set; } 

        [Display(Name = "Unit/Office Number")]
        public string UnitNumber { get; set; } 

        [Required]
        [Phone] 
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } 

        [Required]
        [Display(Name = "Courier Company")]
        public string CourierCompany { get; set; } 

        [Required]
        [Display(Name = "Package Type")]
        public string PackageType { get; set; } 

        [Required]
        [DataType(DataType.DateTime)] 
        [Display(Name = "Arrival Date & Time")]
        public DateTime ArrivalDateTime { get; set; } = DateTime.Now; 

        [DataType(DataType.Date)]
        [Display(Name = "Expected Pickup Date")]
        public DateTime? ExpectedPickupDate { get; set; } 

        [DataType(DataType.DateTime)]
        [Display(Name = "Claimed Date & Time")]
        public DateTime? ClaimedDateTime { get; set; } 

        [Display(Name = "Received By")]
        public string? ReceivedBy { get; set; } 

        public string Status { get; set; } = "Waiting for Pickup";

        public string? Notes { get; set; }
    }
}