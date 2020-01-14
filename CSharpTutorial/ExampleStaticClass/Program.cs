using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStaticClass
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person 
            { 
                FirstName = "Obi", 
                LastName = "Mogbogu", 
                Age = 30 
            };

            var updatePersonDelegate = SetUpdatePerson(person);
            var p = updatePersonDelegate(null);
            Console.WriteLine(p.FirstName + " " + p.LastName);
            Console.WriteLine(p.Age);
            
            //UPDATE Person
            person.Age = 35;
            person.LastName = "Olajide";
            var p1 = updatePersonDelegate(person);
            Console.WriteLine(p.FirstName + " " + p.LastName);
            Console.WriteLine(p1.Age);
            Console.ReadKey();
        }

        public static Func<Person, Person> SetUpdatePerson(Person person)
        {
            Person person1 = new Person();
            person1.FirstName = person.FirstName;
            person1.LastName = person.LastName;
            person1.Age = person.Age;

           
            //return a function that will be used to be updating the Person via closure mechanism.
            Func<Person, Person> updatePersonFunc = (p) =>
            {
                if(p != null)
                {
                    person1.FirstName = p.FirstName;
                    person1.LastName = p.LastName;
                    person1.Age = p.Age;
                }
                return p ?? person1;
            };

            return updatePersonFunc;
        }
    }
}
