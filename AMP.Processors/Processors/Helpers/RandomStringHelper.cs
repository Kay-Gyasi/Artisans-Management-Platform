using System;
using System.Linq;
using System.Text;

namespace AMP.Processors.Processors.Helpers;

public static class RandomStringHelper
{
    public static string Generate(int size, bool lowerCase = false)
    {
        var rand = new Random();
        var builder = new StringBuilder(size);  
  
        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.  

        // char is a single Unicode character  
        var offset = lowerCase ? 'a' : 'A';  
        const int lettersOffset = 26; // A...Z or a..z: length=26  
  
        for (var i = 0; i < size; i++)  
        {  
            var @char = (char)rand.Next(offset, offset + lettersOffset);  
            builder.Append(@char);  
        }  
  
        return lowerCase ? builder.ToString().ToLower() : builder.ToString();  
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str.Where(c => c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z'))
        {
            sb.Append(c);
        }
        return sb.ToString();
    }
}