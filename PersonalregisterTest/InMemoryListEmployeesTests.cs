using System;
using System.Linq;
using Personalregister;

namespace PersonalregisterTest
{
    public class InMemoryListEmployeesTests
    {
        [Fact]
        public void Add_ShouldStoreEmployee()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "Alice", Salary = 30000m };

            repo.Add(emp);

            var list = repo.List();
            Assert.Single(list);
            Assert.Equal("Alice", list[0].Name);
            Assert.Equal(30000m, list[0].Salary);
        }

        [Fact]
        public void Add_DuplicateName_ThrowsInvalidOperationException()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "Bob", Salary = 20000m };

            repo.Add(emp);

            var dup = new Employee { Name = "bob", Salary = 25000m };
            Assert.Throws<InvalidOperationException>(() => repo.Add(dup));
        }

        [Fact]
        public void Update_Existing_ReplacesEmployee()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "Carol", Salary = 22000m };
            repo.Add(emp);

            var updated = new Employee { Name = "Carol", Salary = 24000m };
            repo.Update(updated);

            var list = repo.List();
            Assert.Single(list);
            Assert.Equal(24000m, list[0].Salary);
        }

        [Fact]
        public void Update_NonExisting_ThrowsKeyNotFoundException()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "NonExist", Salary = 1000m };
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => repo.Update(emp));
        }

        [Fact]
        public void Delete_RemovesEmployee()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "Dave", Salary = 18000m };
            repo.Add(emp);

            repo.Delete(emp);

            var list = repo.List();
            Assert.Empty(list);
        }

        [Fact]
        public void Delete_NonExisting_ThrowsKeyNotFoundException()
        {
            var repo = new InMemoryListEmployees();
            var emp = new Employee { Name = "Nobody", Salary = 0m };
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => repo.Delete(emp));
        }
    }
}
