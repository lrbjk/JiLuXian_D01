using System.Collections.Generic;

namespace Helper
{
    public class BiDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> forward = new Dictionary<TKey, TValue>();
        private Dictionary<TValue, TKey> reverse = new Dictionary<TValue, TKey>();

        public void Add(TKey key, TValue value)
        {
            forward.Add(key, value);
            reverse.Add(value, key);
        }

        public TValue GetByKey(TKey key) => forward[key];
        public TKey GetByValue(TValue value) => reverse[value];

        public bool TryGetKey(TValue value, out TKey key) => reverse.TryGetValue(value, out key);
        public bool TryGetValue(TKey key, out TValue value) => forward.TryGetValue(key, out value);
    }

}
