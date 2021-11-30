

using System.Collections.Generic;

namespace SpreetailWorkSample.Interfaces
{
    public interface IMultiValueDictionaryService
    {
        public HashSet<string> GetAllKeys();
        public HashSet<string> GetAllMembersOfKey(string key);
        public void AddMember(string key, string value);
        public void RemoveMember(string key, string value);
        public void RemoveAllMembers(string key);
        public void Clear();
        public bool KeyExists(string key);
        public bool MemberExists(string key, string value);
        public IReadOnlyList<string> GetAllMembers();
        public HashSet<KeyValuePair<string,string>> GetAllItems();
        public int CountKeys();
        public int CountMembers(string key);
    }
}
