using System.Collections.Generic;

namespace Assignment.API.Resources
{
    public class PeopleDifferenceResource
    { 
        public bool AreEqual { get; set; }

        public bool AreSameSize { get; set; }

        public List<string> Differences { get; set; }
    }
}