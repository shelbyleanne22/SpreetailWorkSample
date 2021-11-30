using SpreetailWorkSample.Interfaces;
using SpreetailWorkSample.Models;
using System.Collections.Generic;

namespace SpreetailWorkSample.Services
{
    public class MultiValueDictionaryService : IMultiValueDictionaryService
    {
        private MultiValueDictionary<string, HashSet<string>> _multiValueDictionary = new();

        public void AddMember(string key, string value)
        {
            HashSet<string> members = _multiValueDictionary.GetValueOrDefault(key);
            if (members == null)
            {
                HashSet<string> newMembers = new() { value };
                _multiValueDictionary.Add(key, newMembers);
            }
            else
            {
                members.Add(value);
                _multiValueDictionary[key] = members;
            }
        }

        public void Clear()
        {
            if (_multiValueDictionary.Count != 0)
            {
                _multiValueDictionary.Clear();
            }
        }

        public HashSet<KeyValuePair<string, string>> GetAllItems()
        {
            HashSet<KeyValuePair<string,string>> results = new();
            if (_multiValueDictionary.Count != 0)
            {
                results = GetKeyValuePairs();
            }
            return results;
        }

        public IReadOnlyList<string> GetAllMembers()
        {
            List<string> results = new();
            if (_multiValueDictionary.Count != 0)
            {
                results = GetMembersList();
            }
            return results;
        }       

        public HashSet<string> GetAllKeys()
        {
            HashSet<string> results = new();
            foreach (string key in _multiValueDictionary.Keys)
            {
                results.Add(key);
            }
            return results;
        }

        public HashSet<string> GetAllMembersOfKey(string key)
        {
            HashSet<string> results = new();
            foreach (string value in _multiValueDictionary.GetValueOrDefault(key))
            {
                results.Add(value);
            }
            return results;
        }

        public bool KeyExists(string key)
        {
            return _multiValueDictionary.ContainsKey(key);
        }

        public bool MemberExists(string key, string value)
        {
            HashSet<string> existingValues = _multiValueDictionary.GetValueOrDefault(key);
            if (existingValues != null)
            {
                return existingValues.Contains(value);
            }
            return false;
        }

        public void RemoveAllMembers(string key)
        {
            _multiValueDictionary.Remove(key);
        }

        public void RemoveMember(string key, string value)
        {
            HashSet<string> existingValues = _multiValueDictionary.GetValueOrDefault(key);

            existingValues.Remove(value);

            if (_multiValueDictionary.GetValueOrDefault(key).Count == 0)
            {
                _multiValueDictionary.Remove(key);
            }
        }

        public int CountKeys()
        {
            return _multiValueDictionary.Keys.Count;
        }

        public int CountMembers(string key)
        {
            return _multiValueDictionary.GetValueOrDefault(key).Count;
        }

        private HashSet<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            HashSet<KeyValuePair<string, string>> results = new();
            foreach (string key in _multiValueDictionary.Keys)
            {
                foreach (string value in _multiValueDictionary.GetValueOrDefault(key))
                {
                    results.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return results;
        }

        private List<string> GetMembersList()
        {
            List<string> results = new();
            foreach (HashSet<string> value in _multiValueDictionary.Values)
            {
                foreach (string item in value)
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }
}
