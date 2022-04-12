using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User()
            {
                Id = 1,
                Name = "James",
                Email = "Sample@sampime.ie",
                Address = new Address()
                { Street = "34 Main St", City = "Galway", ZipCode = "121" }
            },
            new User()
            {
                Id = 2,
                Name = "Stephen",
                Email = "Sonic@sampime.ie",
                Address = new Address()
                { Street = "72 Dunkellon Main St", City = "Cavan", ZipCode = "H856WD" }
            },
            new User()
            {
                Id = 3,
                Name = "Myary",
                Email = "oldasa@sampime.ie",
                Address = new Address()
                { Street = "34 Hola St.", City = "Cavan", ZipCode = "H856WD" }
            }
        };

    }
}
