public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN FOR MultiplesOf:
        // 1. Create a double array with size equal to 'length'
        // 2. Loop from i = 0 to length - 1
        // 3. For each iteration, calculate: number * (i + 1)
        //    - When i=0, we get number * 1 = first multiple
        //    - When i=1, we get number * 2 = second multiple
        //    - And so on...
        // 4. Store each result in the array at index i
        // 5. Return the completed array
        
        double[] multiples = new double[length];
        
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }
        
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN FOR RotateListRight:
        // 1. Find the split point: data.Count - amount
        //    This tells us where to cut the list
        //    Example: {1,2,3,4,5,6,7,8,9}, amount=5
        //    splitPoint = 9-5 = 4
        // 
        // 2. Get the last 'amount' elements starting from splitPoint
        //    Using GetRange(splitPoint, amount)
        //    Example: GetRange(4, 5) → {5,6,7,8,9}
        // 
        // 3. Get the first 'splitPoint' elements starting from 0
        //    Using GetRange(0, splitPoint)
        //    Example: GetRange(0, 4) → {1,2,3,4}
        // 
        // 4. Clear the original list
        // 
        // 5. Add the last part first (the elements that should rotate to the front)
        // 
        // 6. Then add the first part (the elements that should move to the back)
        //    Final result: {5,6,7,8,9,1,2,3,4} ✓
        
        // Find where to split the list
        int splitPoint = data.Count - amount;
        
        // Get the two parts
        List<int> lastPart = data.GetRange(splitPoint, amount);
        List<int> firstPart = data.GetRange(0, splitPoint);
        
        // Clear the original list
        data.Clear();
        
        // Add parts in new order (right rotation)
        data.AddRange(lastPart);
        data.AddRange(firstPart);
    }
}