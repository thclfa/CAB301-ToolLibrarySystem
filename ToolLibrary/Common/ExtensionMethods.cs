using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// Contains numerous extension methods including sorting algorithms
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns instances of some IEnumerable that are not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> sequence)
        {
            return sequence.Where(e => e != null);
        }

        /// <summary>
        /// Returns a new string center-aligned with some padding character on the left and right sides.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="width">Width of padding</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string Pad(this string str, int width, char paddingChar = ' ')
        {
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(width, paddingChar);
        }

        /// <summary>
        /// Returns a new string center-aligned padded with <see cref="Console.WindowWidth"/> number of characters.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string Pad(this string str, char paddingChar = ' ')
        {
            return str.Pad(Console.WindowWidth, paddingChar);
        }

        /// <summary>
        /// Multiplies a char i times to create a longer string.
        /// </summary>
        /// <param name="chr">char to be multiplied</param>
        /// <param name="i">Coefficient</param>
        /// <returns>New string of length i</returns>
        public static string Mul(this char chr, int i)
        {
            return new string(chr, i);
        }

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
             */

            // Set default func if not supplied, int delegate should be 
            // supplied so this janky casting should be sufficient
            if (func == null)
                func = x => (int)(object)x;

            int n = array.Length;

            // Init min/max bounds, O(n)
            int min = 0;
            int max = 0;
            foreach (int v in array.Select(x => func(x)))
                if (v < min) min = v;
                else if (v > max) max = v;

            int k = max - min + 1;
            int[] counts = new int[k];

            // Get Counts for each instance of A[i], O(n)
            foreach (T x in array)
                counts[func(x) - min]++;

            // Stack counts to get proper indices, O(k)
            counts[0]--; // Arrays start at 0, so take 1 from first index
            for (int i = 1; i < k; i++)
                counts[i] += counts[i - 1];

            // Collate Results, O(n)
            T[] result = new T[n];
            foreach (T x in array)
            {
                var v = func(x);
                var i = counts[v] - min;
                result[i] = x;
                counts[v]--;
            }

            // O(n) + O(n) + O(n) + O(k) = O(n + k)
            return result;
        }

        /// <summary>
        /// O(n log n) style divide and conquer sorting algorithm
        /// </summary>
        /// <param name="array">Integer Array to be sorted</param>
        /// <returns>Returns sorted instance of array</returns>
        public static int[] MergeSort(this int[] array)
        {
            return MergeSort(array, x => (int)(object)x);
        }

        /// <summary>
        /// O(n log n) style divide and conquer sorting algorithm
        /// </summary>
        /// <typeparam name="T">Typeof array to be sorted</typeparam>
        /// <param name="array">Array to be sorted</param>
        /// <param name="func">Some integer delegate to order by</param>
        /// <returns>Returns sorted instance of array</returns>
        public static T[] MergeSort<T>(this T[] array, Func<T, int> func)
        {
            /* Time complexity:
             *  Best Case:  O(n log n)
             *  Worst Case: O(n log n)
	         * Space complexity: O(n)
             */

            // Return array if it's a scalar
            if (array.Length <= 1)
                return array;

            // Get midpoint of array
            int n = array.Length;
            int m = n / 2;

            // Division stage of O(log_2 n)
            // Range ('..') notation is new to c# 8.0
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
            int l = 0; // Left index
            int r = 0; // Right index

            for (int i = 0; i < result.Length; i++)
            {
                // While indexes are within range, do comparisons
                if (l < L.Length && r < R.Length)
                {
                    if (func(L[l]) <= func(R[r]))
                        result[i] = L[l++];
                    else
                        result[i] = R[r++];
                }
                // Indexes are no longer in range, no comparisons needed
                else if (l < L.Length)
                    result[i] = L[l++];
                else
                    result[i] = R[r++];
            }

            return result;
        }
    }
}
