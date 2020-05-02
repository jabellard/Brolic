using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public class Downstream
    {
        public string Handler { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
    }
}