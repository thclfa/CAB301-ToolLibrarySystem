using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    public static class StringTools
    {
        /// <summary>
        /// Returns a new string center-aligned with some padding character on the left and right sides.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="width">Width of padding</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string PadSides(this string str, int width, char paddingChar = ' ')
        {
            
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(width, paddingChar);
        }
    }
}
