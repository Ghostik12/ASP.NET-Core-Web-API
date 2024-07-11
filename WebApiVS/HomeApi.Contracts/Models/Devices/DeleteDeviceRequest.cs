using System.ComponentModel.DataAnnotations;

namespace HomeApi.Contracts.Models.Devices
{
    public class DeleteDeviceRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
