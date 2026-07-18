using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities (Low=1, High=3, Medium=2)
    // and dequeue them.
    // Expected Result: The item with the highest priority (High) should be
    // removed first. After removal, the queue should have 2 items remaining.
    // Defect(s) Found: 
    // 1. The Dequeue method was using >= instead of >, which broke FIFO for equal priorities
    // 2. The Dequeue method was not removing the item from the queue
    // 3. The loop was excluding the last element in the queue
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 3);
        priorityQueue.Enqueue("Medium", 2);

        // Dequeue should return the item with highest priority (High)
        string result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
        
        // After removing one item, 2 should remain
        Assert.AreEqual(2, priorityQueue.Length);
    }

    [TestMethod]
    // Scenario: Add items with the same priority (First=2, Second=1, Third=2)
    // and dequeue them.
    // Expected Result: The two items with priority 2 should be removed in
    // FIFO order (First, then Third). The item with priority 1 should remain.
    // Defect(s) Found: 
    // 1. The Dequeue method was using >= instead of >, which caused LIFO behavior for ties
    // 2. The Dequeue method was not removing the item from the queue
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("Second", 1);
        priorityQueue.Enqueue("Third", 2);

        // Both "First" and "Third" have priority 2.
        // "First" was added first, so it should be removed first (FIFO).
        string result1 = priorityQueue.Dequeue();
        Assert.AreEqual("First", result1);

        // "Third" should be removed next (same priority, FIFO order).
        string result2 = priorityQueue.Dequeue();
        Assert.AreEqual("Third", result2);

        // After removing two items, 1 should remain ("Second" with priority 1)
        Assert.AreEqual(1, priorityQueue.Length);
    }

    // Add more test cases as needed below.
}