using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] people = CreatePeopleArray(Size);
            ClosestNodeSearcher searcher = new ClosestNodeSearcher(people);

            DisplayFullList(people);

            while (true)
            {
                int personAIdx, personBIdx;
                while (!ReadIndex("Insert index for person A", out personAIdx)) ;
                if (personAIdx == -1)
                    break;

                while (!ReadIndex("Insert index for person B", out personBIdx)) ;
                if (personBIdx == -1)
                    break;

                int distance = searcher.FindMinRelationLevel(personAIdx, personBIdx);
                if (distance < 0)
                {
                    Console.WriteLine("Persons are not connected");
                }
                else
                {
                    Console.WriteLine("The distance is: {0}", distance);
                }
            }
        }

        private static bool ReadIndex (string pComment, out int pIdx)
        {
            Console.Write(pComment + " ");
            string idString = Console.ReadLine();
            int id;
            if (int.TryParse(idString, out id) && IdIsLegal(id))
            {
                pIdx = id;
                return true;
            }

            pIdx = int.MinValue;
            return false;

        }

        private static bool IdIsLegal(int pId)
        {
            return (pId == -1) || (pId >= 0 && pId < Size);
        }

        private static void DisplayFullList(Person[] pPeople)
        {
            for (int i=0; i<pPeople.Length; i++)
            {
                Console.WriteLine("{0}. {1} {2}, {3}, {4}",
                        i,
                        pPeople[i].Name.FirstName,
                        pPeople[i].Name.LastName,
                        pPeople[i].Address.Street,
                        pPeople[i].Address.City
                    );
            }
        }

        private static Person[] CreatePeopleArray(int pSize)
        {
            string[] names = new string[]
            {
                "Kimberley Mckenzie",
                "Mitchell Freeman",
                "Cornelius Casey",
                "Marc Mahoney",
                "Violet Hernandez",
                "Dennis Boone",
                "Rosario Alexander",
                "Tommy Buchanan",
                "Sanford Potter",
                "Forest Dawson",
                "Raymundo Koch",
                "Jodi Jimenez",
                "Bridget Conner",
                "Noah Hodges",
                "Cruz Bowers",
                "Shirley Cortez",
                "Rena Mason",
                "Stephan Fry",
                "Willie Vaughn"
            };

            string[] cities = new string[]
            {
                "Sruuton",
                "Civing",
                "Owraanford",
                "Blaidon",
                "Cruurland",
                "Ustrose",
                "Hada",
                "Grence",
                "Inegas",
                "Eahstin",
                "Asranding",
                "Vorsall",
                "Praiyrith",
                "Sturith",
                "Vruling",
                "Plard",
                "Vrans",
                "Hine",
                "Amsea",
                "Esterdon"
            };

            string[] streets = new string[]
            {
                "Ferguson Court",
                "Norbury Drive",
                "Upper Green",
                "Cowdray Road",
                "Cornmill Gardens",
                "Queens Mews",
                "Briardene",
                "Wrexham Close",
                "Woodcote Road",
                "Cranworth Gardens",
                "The Hill",
                "Oakford Close",
                "Saddington Road",
                "The Stream",
                "Hollis Close",
                "Trinity Walk",
                "Northampton Road",
                "Silvermere",
                "Emerald Street",
                "Thornfield Grove"
            };

            Person[] res = new Person[Size];

            Random r = new Random(DateTime.Now.Millisecond);

            for (int i=0; i<Size; i++)
            {
                string[] name = names[r.Next(/*names.Length*/4)].Split(' ');
                res[i] = new Person()
                {
                    Name = new Name() { FirstName = name[0], LastName = name[1] },
                    Address = new Address { City = cities[r.Next(/*cities.Length*/4)], Street = streets[r.Next(/*streets.Length*/4)] }
                };
            }

            return res;
        }

        private const int Size = 20;
    }
}
