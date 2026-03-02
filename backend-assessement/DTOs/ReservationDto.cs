namespace backend_assessement.DTOs
{
    public class ReservationDto
    {
        public string UserId { get; set; }
        public string ReservationId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
