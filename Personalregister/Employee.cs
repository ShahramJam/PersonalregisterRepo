using System;

namespace Personalregister
{
    internal class Employee : IEquatable<Employee>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Salary:C}";
        }

        public bool Equals(Employee? other)
        {
            if (other is null) return false;
            return string.Equals(Name?.Trim(), other.Name?.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as Employee);

        public override int GetHashCode()
        {
            return (Name ?? string.Empty).Trim().ToLowerInvariant().GetHashCode();
        }
    }
}
