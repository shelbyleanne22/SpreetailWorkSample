using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreetailWorkSample.Interfaces
{
    public interface IPrintService
    {
        public void Print(HashSet<string> results);
        public void Print(IReadOnlyList<string> results);
        public void Print(HashSet<KeyValuePair<string, string>> results);
        public void Print(string result);
    }
}
