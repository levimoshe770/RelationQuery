using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationQuery
{
    internal class Name
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    internal class Person
    {
        public Name Name { get; set; } 
        public Address Address { get; set; }

        public static bool operator == (Person pA, Person pB)
        {
            return string.Equals(pA.Name.FirstName, pB.Name.FirstName) && string.Equals(pA.Name.LastName, pB.Name.LastName) &&
                   string.Equals(pA.Address.Street, pB.Address.Street) && string.Equals(pA.Address.City, pB.Address.City);
        }

        public static bool operator != (Person pA, Person pB)
        {
            return !(pA == pB);
        }

        public bool In1LevelRelationWith(Person pB)
        {
            return (string.Equals(Name.FirstName, pB.Name.FirstName) && string.Equals(Name.LastName, pB.Name.LastName)) ||
                   (string.Equals(Address.Street, pB.Address.Street) && string.Equals(Address.City, pB.Address.City));
        }
    }
}
