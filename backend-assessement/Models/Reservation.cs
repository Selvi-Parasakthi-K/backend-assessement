using System;
using System.ComponentModel.DataAnnotations;

namespace backend_assessement.Models
{
    public class Reservation
    {
        [Key]
        public Guid ReservationId { get; set; } = Guid.NewGuid();
        public string UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
