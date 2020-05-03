using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public class BrolicRoute
    {
        public string BasePath { get; set; }
        public List<string> PathSegments { get; set; }
        public bool CatchAll { get; set; }
        public List<string> Methods { get; set; }
        public string Downstream { get; set; }
    }
}