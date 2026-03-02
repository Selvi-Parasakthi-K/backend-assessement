namespace backend_assessement.Models
{
    public class Reservaion
    {
        public string UserId { get; set; }
        public string ReservationId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
