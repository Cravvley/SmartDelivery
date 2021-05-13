using System;

namespace SmartDelivery.Infrastructure.Models
{
    public class FinishRequest
    {
        public string Geodata { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int Reason { get; set; }
    }
}
