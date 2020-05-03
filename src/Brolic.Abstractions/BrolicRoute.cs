using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public class BrolicRoute
    {
        public string PathTemplate { get; set; }
        public List<string> Methods { get; set; }
        public string Downstream { get; set; }
    }
}