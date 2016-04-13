using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMVVM.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static async Task<List<Person>> GetAll() // could be separated to a different class
        {
            await Task.Delay(1000); // database load

            return new List<Person>()
            {
                new Person() { PersonID = 1, FirstName = "Bob", LastName = "Smith" },
                new Person() { PersonID = 2, FirstName = "James", LastName = "Jones" }
            };
        }
    }
}
