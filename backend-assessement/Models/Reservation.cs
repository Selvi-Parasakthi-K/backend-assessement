namespace backend_assessement.Models
{
    public class Reservation
    {
        public string ReservationId { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
