using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.API
{
    public class DiffResult
    {
        public bool AreEqual { get; set; }

        public bool AreSameSize { get; set; }

        public List<string> Differences { get; set; }

        public DiffResult()
        {
            AreEqual = false;
            AreSameSize = false;
            Differences = new List<string>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DiffResult);
        }

        public bool Equals(DiffResult other)
        {
            return other != null &&
                    this.AreEqual == other.AreEqual &&
                    this.AreSameSize == other.AreSameSize &&
                    this.Differences.SequenceEqual(other.Differences);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AreEqual, AreSameSize, Differences);
        }
    }
}