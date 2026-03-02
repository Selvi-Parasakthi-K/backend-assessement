**SECTION A**

SOLUTION - Using Greedy Technique

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

**SECTION B**

The Inventory component is implemented using the Singleton Pattern. Because a single shared instance is used across the application.
Since inventory is a shared resource, this approach helps maintain consistency and prevents multiple instances from modifying the same data concurrently.

To handle concurrent access, synchronization is managed using a SemaphoreSlim. This prevent from the race condition.



**SECTION C**

SOLUTION

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
