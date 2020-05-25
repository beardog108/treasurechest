// Taken from https://github.com/Kaynn-Cahya/Shannon-Entropy with no license given.
// Since this was published on nuget, i am assuming the author is ok with it being used in open source
using System;
using System.Collections.Generic;
using System.Linq;

// Test: testEntropy.cs
namespace ShannonEntropyCal
{
    public class EntropyCal
    {

        public static double EntropyValue(string message)
        {
            Dictionary<char, int> K = message.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double EntropyValue = 0;
            foreach (var character in K)
            {
                double PR = character.Value / (double) message.Length;
                EntropyValue -= PR * Math.Log(PR, 2);
            }
            return EntropyValue;
        }

        public static double EntropyBits(string message)
        {
            Dictionary<char, int> K = message.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double EntropyValue = 0;
            foreach (var character in K)
            {
                double PR = character.Value / (double) message.Length;
                EntropyValue -= PR * Math.Log(PR, 2);
            }
            return Math.Ceiling(EntropyValue) * message.Length;
        }
    }
}
