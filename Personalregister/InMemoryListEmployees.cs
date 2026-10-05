using System;
using System.Collections.Generic;

namespace Personalregister
{
    internal class InMemoryListEmployees : IRepository<Employee>
    {
        private readonly List<Employee> _items = new();
        private readonly object _sync = new();

        public void Add(Employee item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            lock (_sync)
            {
                if (_items.Contains(item))
                {
                    throw new InvalidOperationException("An employee with the same name already exists.");
                }

                _items.Add(item);
            }
        }

        public void Update(Employee item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            lock (_sync)
            {
                var idx = _items.FindIndex(e => e.Equals(item));
                if (idx == -1)
                {
                    throw new KeyNotFoundException("Employee not found.");
                }

                _items[idx] = item;
            }
        }

        public void Delete(Employee item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            lock (_sync)
            {
                var removed = _items.Remove(item);
                if (!removed)
                {
                    throw new KeyNotFoundException("Employee not found.");
                }
            }
        }

        public IReadOnlyList<Employee> List()
        {
            lock (_sync)
            {
                return _items.AsReadOnly();
            }
        }
    }
}
