using backend_assessement.Models;
using System.Collections.Concurrent;

namespace backend_assessement.Services
{
    public class SalesApiServices
    {
        private int _inventory = 0;

        private ConcurrentDictionary<string, Reservaion> _reservations = new();

        private Queue<string> _waitList = new();

        private SemaphoreSlim _lock = new(1, 1);

        public void InitInventory(int count)
        {
            _inventory = count;
        }

        public async Task<object> Reserve(string userId)
        {
            await _lock.WaitAsync();

            try
            {
                if (_inventory > 0)
                {
                    _inventory--;
                    var reservationId = Guid.NewGuid().ToString();

                    var reservation = new Reservaion
                    {
                        UserId = userId,
                        ReservationId = reservationId,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                    };

                    _reservations[reservationId] = reservation;

                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(TimeSpan.FromMinutes(5));
                        await ExpireReservation(reservationId);
                    });

                    return new
                    {
                        Status = 201,
                        Data = new
                        {
                            reservation,
                            expiresAt = reservation.ExpiresAt,
                        }
                    };
                }
                else
                {
                    _waitList.Enqueue(userId);

                    return new
                    {
                        Status = 200,
                        Date = new
                        {
                            message = "Added to waitlist",
                            waitListPosition = _waitList.Count,
                        }
                    };
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task ExpireReservation(string reservationId)
        {
            await _lock.WaitAsync();

            try
            {
                if (_reservations.TryGetValue(reservationId, out var reservation))
                {
                    _reservations.TryRemove(reservationId, out _);
                    _inventory++;
                    if (_waitList.Count > 0)
                    {
                        var nextUser = _waitList.Dequeue();
                        await Reserve(nextUser);
                    }
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public object getStatus()
        {
            return new
            {
                availableInventory = _inventory,
                waitListCount = _waitList.Count,
            };
        }
    }
}
