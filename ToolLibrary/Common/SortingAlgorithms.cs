﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// Contains numerous extension methods including sorting algorithms
    /// </summary>
    public static class SortingAlgorithms
    {
        /// <summary>
        /// O(n + k) style search algorithm which is suited for arrays with possible duplicate values.
        /// </summary>
        /// <typeparam name="T">Typeof array to be sorted</typeparam>
        /// <param name="array">Array to be sorted</param>
        /// <param name="func">Some integer delegate to order by</param>
        /// <returns>Returns sorted instance of A</returns>
        public static T[] CountingSort<T>(this T[] array, Func<T, int> func = null)
        {
            /* Time complexity:
             *  Best Case:  O(n+k)
             *  Worst Case: O(n+k)
	         * Space complexity: O(n+k)
             */

            // Set default func if not supplied, int delegate should be 
            // supplied so this janky casting should be sufficient
            if (func == null)
                func = x => (int)(object)x;

            int n = array.Length;

            // Init min/max bounds, O(n)
            int min = array.Min(func);
            int max = array.Max(func);

            int k = max - min + 1;
            int[] counts = new int[k];

            // Get Counts for each instance of A[i], O(n)
            foreach (T a in array)
                counts[func(a) - min]++;

            // Stack counts to get proper indices, O(k)
            counts[0]--; // Arrays start at 0, so take 1 from first index
            for (int i = 1; i < k; i++)
                counts[i] += counts[i - 1];
                

            // Collate Results, O(n)
            T[] result = new T[n];
            foreach (T a in array)
            {
                var v = func(a);
                var i = counts[v - min];
                result[i] = a;
                counts[v - min]--;
            }

            // O(n) + O(n) + O(n) + O(k) = O(n + k)
            return result;
        }

        /// <summary>
        /// O(w*n) style sorting algorithm where n is the number of values and w is the number of digits. Starting at Most Significant Digit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="func"></param>
        /// <returns>Returns sorted instance of array</returns>
        public static void RadixSort<T>(this T[] array, Func<T, int> func = null)
        {
            /* Time complexity:
             *  Best Case:  O(w*n)
             *  Worst Case: O(w*n)
	         * Space complexity: O(w+n)
             */

            // Return if it's a scalar
            if (array.Length <= 1)
                return;

            // Set default func if not supplied, int delegate should be 
            // supplied so this janky casting should be sufficient
            if (func == null)
                func = x => (int)(object)x;

            int i, j;
            T[] tmp = new T[array.Length];

            // Loop through each bit of a 32 bit integer
            for (int bit = 32; bit > -1; --bit)
            {
                j = 0;
                // Loop a bucket for that bit
                for (i = 0; i < array.Length; ++i)
                {
                    // move the bit if it's in the bucket and needs to be moved
                    bool move = (func(array[i]) << bit) >= 0;
                    if (bit == 0 ? !move : move)
                        array[i - j] = array[i];
                    else
                        tmp[j++] = array[i];
                }
                Array.Copy(tmp, 0, array, array.Length - j, j);
            }
        }

        /// <summary>
        /// O(n log n) style divide and conquer sorting algorithm
        /// </summary>
        /// <typeparam name="T">Typeof array to be sorted</typeparam>
        /// <param name="array">Array to be sorted</param>
        /// <param name="func">Some integer delegate to order by</param>
        /// <returns>Returns sorted instance of array</returns>
        public static T[] MergeSort<T>(this T[] array, Func<T, int> func = null)
        {
            /* Time complexity:
             *  Best Case:  O(n log n)
             *  Worst Case: O(n log n)
	         * Space complexity: O(n)
             */

            if (func == null)
                func = x => (int)(object)x;

            // Return array if it's a scalar
            if (array.Length <= 1)
                return array;

            // Get midpoint of array
            int n = array.Length;
            int m = n / 2;

            // Division stage of O(log_2 n)
            // Range ('..') notation is new to c# 8.0, similar to slicing in python
            T[] L = MergeSort(array[0..m], func);
            T[] R = MergeSort(array[m..n], func);

            // Merge stage of O(n)
            return Merge(L, R, func);
        }

        /// <summary>
        /// Merges two arrays together in ascending order by result of supplied integer delegate
        /// </summary>
        /// <typeparam name="T">Type of array to be merged</typeparam>
        /// <param name="L">Left array of objects</param>
        /// <param name="R">Right array of objects</param>
        /// <param name="func">Some integer delegate to order by</param>
        /// <returns>Returns an ordered array merged from L and R</returns>
        private static T[] Merge<T>(T[] L, T[] R, Func<T, int> func)
        {
            /* Time complexity:
             *  Best Case:  O(n)
             *  Worst Case: O(n)
             * Space complexity: O(n)
             */
            T[] result = new T[L.Length + R.Length];
            int j = 0; // Left index
            int k = 0; // Right index

            for (int i = 0; i < result.Length; i++)
            {
                // While indexes are within range, do comparisons
                if (j < L.Length && k < R.Length)
                {
                    if (func(L[j]) <= func(R[k]))
                        result[i] = L[j++];
                    else
                        result[i] = R[k++];
                }
                // Fill result with remaining values
                else if (j < L.Length)
                    result[i] = L[j++];
                else
                    result[i] = R[k++];
            }

            return result;
        }
    }
}
