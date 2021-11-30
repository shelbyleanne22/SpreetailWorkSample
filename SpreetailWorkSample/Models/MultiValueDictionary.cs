using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SpreetailWorkSample.Models
{
    public class MultiValueDictionary<TKey, TValue> : Dictionary<string, HashSet<string>>
    {
        public string Key { get; set; }
        public HashSet<string> Value { get; set; }
    }
}
