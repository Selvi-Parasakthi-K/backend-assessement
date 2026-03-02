## **SECTION A**

### **SOLUTION - Using Greedy Technique**

```
using System;
using System.Collections.Generic;

class Scheduler
{
    public int MaintenanceScheduler(List<int[]> intervals)
    {
        intervals.Sort((a, b) => a[1].CompareTo(b[1]));

        int count = 0;
        int currentEnd = 0;

        for (int i = 0; i < intervals.Count; i++)
        {
            int start = intervals[i][0];
            int end = intervals[i][1];

            if (start >= currentEnd)
            {
                count++;
                currentEnd = end;
            }
        }

        return count;
    }
}

class Program
{
    static void Main()
    {
        List<int[]> intervals = new List<int[]>();

        Console.Write("Enter number of intervals: ");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter Start and End time for interval {i + 1}: ");
            
            string[] input = Console.ReadLine().Split(' ');

            int start = Convert.ToInt32(input[0]);
            int end = Convert.ToInt32(input[1]);

            intervals.Add(new int[] { start, end });
        }

        Scheduler obj = new Scheduler();
        int result = obj.MaintenanceScheduler(intervals);
        
        Console.WriteLine();
        Console.WriteLine("Maximum Non-Overlapping Tasks: " + result);
    }
}
```

## **SECTION B**

### **Inventory Reservation API**

This API is used to manage product inventory and allow users to reserve items.  
If inventory is not available, users will be added to a waitlist automatically.

The systme maintain a shared inventory count.  
When a user requests a reservation:  
    If inventory is available -> reservation is created.  
    If inventory is not available -> user is added to the waitlist.  

Each reservation is valid for 5 minutes.  

After 5 minutes:
- The reservation expires automatically.
- Inventory is returned back.
- Next user from the waitlist is moved to reservation (if available).

---

### **Concurrency Handling - Race Condition**

Inventory is shared resource and this can be accessed by multiple users at the time.

To avoid this:
- Singleton Service -> to maintain one shared inventory instance across the application.
- SemaphoreSlim Lock -> ensures only one thread can reserve inventory at a time.

---

### **Reservation Expiry**

Each reservation:
- Has an expiry time of 5 minutes
- Is automatically removed after expiry
- Restores 1 item back to inventory
- Assigns inventory to next user in the waitlist (if any)

---

### **API Endpoints**

#### 1. Initialize Inventory

```
POST /init?count=10
```

For initializing the available Inventory.

---

#### 2. Reserve Inventory

```
POST /reserve?userId=123
```

What this will do is:
- Reserves an item if inventory is available.
- If not available, adds the user to the waitlist.

**Success Response**

```json
{
  "reservation": {
    "reservationId": "unique-id",
    "userId": "123",
    "expiresAt": "timestamp"
  }
}
```

**Waitlist Response**

```json
{
  "message": "Added to waitlist",
  "waitListPosition": 2
}
```

---

#### 3. For Checking inventory Status

```
GET /status
```

**Response**

```json
{
  "availableInventory": 5,
  "waitListCount": 3
}
```




## **SECTION A**

### **SOLUTION**

The `success_rate` for each driver is calculated using the following formula:
```
Success Rate (%) = 
(Number of Completed Deliveries / Total Delivery Attempts) * 100
```
In SQL Implementation,
```
SELECT driver_id, COUNT(*) AS total_deliveries, 
	SUM(CASE 
			WHEN delivery_status = 'COMPLETED' THEN 1 ELSE 0
		END) * 100 / COUNT(*)
FROM deliveries-tracking
WHERE attempt_timestamp >= '2024-09-01' AND attempt_timestamp < '2024-10-01'
GROUP BY driver_id
HAVING (SUM(CASE 
			WHEN delivery_status = 'COMPLETED' THEN 1 ELSE 0
		END)) >= 5 
		AND
		(SUM(CASE 
			WHEN delivery_status = 'COMPLETED' THEN 1 ELSE 0
		END) * 100 / COUNT(*)) >= 90
ORDER BY DESC;

```
