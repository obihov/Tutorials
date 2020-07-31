using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_NETInterfaces
{
    internal class EnumerableExample
    {
        static public void Run()
        {
            Person person1 = new Person { FirstName = "Obi", LastName = "Mog" };
            Person person2 = new Person { FirstName = "Kin", LastName = "Net" };
            Person[] persons = new Person[] { person1, person2 };
            
            
            //the enumerator or cursor uses a yield return single statement internally to generate a stateful code
            //this generated code returns one item at a time to a collection (i.e. bucket) until it eventually returns everything.
            //Yield grants you method like MoveNext and property like Current to get current value.
            //think of MoveNext as the row currently been accessed by the compiler.
            //think of Current as the selection of the entire value for the row.
            //IEnumerable is like a table in database. IEnumerator is like a cursor in database.
            

            //So you can combine indexer and enumerator mechanism in your custom type, allowing you to find a single item or get all items as you wish.
            //below gets you all items using the enumerator
            IEnumerable<Person> people = new EnumerableInterfaceExample(persons);
            foreach(var p in people)
            {
                Console.WriteLine(p.ToString());
            }
            //OR
            var enumerator = people.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.ToString());
            }
            Console.WriteLine();

            //below gets you a single item using the indexer
            Person person = (people as EnumerableInterfaceExample)[0];  //note its because we instantiated people earlier as IEnumerable<Person>. If we had done so with EnumerableInterfaceExample instead, then we won't have had to cast.
            Console.WriteLine(person.ToString());
        }

        private class IndexerExample
        {
            Person[] people;
            
            //So indexers are quite different than using a IEumerable to create a custom collection type.
            //Indexers don't return you a list of items, rather it helps return a single item from a collection.
            public Person this[int i] => people[i];
            public Person this[string firstName]
            {
                get
                {
                    return people.FirstOrDefault(p => p.FirstName == firstName);
                }
            }
            

            public IndexerExample(Person[] people)
            {
                this.people = people;
            }


        }

        private class EnumerableInterfaceExample : IEnumerable<Person>
        {
            private Person[] people;

            public EnumerableInterfaceExample(Person[] people)
            {
                this.people = people;
            }


            public Person this[int i] => people[i];

            public IEnumerable<Person> GetPeople()
            {
                for (int i = 0; i < people.Length; i++)
                {
                    //below line of code returns one item at a time to a list. Eventually returning all items.
                    //C# converts the code below to a state machine, allowing your machine to process one item at a time rather than all the items at once.
                    //using yield helps with performance. Note you must always combine a yield and return of just a single item within your loop body.
                    //can use in a while, do-while, even foreach, parallelLoop etc.
                    yield return people[i];
                }

                //below line of code returns a list. 
                //This approach wont convert your code to a state machine because it requires all items to be
                //fetched at once as opposed to one at a time which the yield offers for much improved looping.
                //return people;
            }

            public IEnumerator<Person> GetEnumerator()
            {

                for(int i =0; i < people.Length; i++)
                {
                    yield return people[i]; 
                    //Yield are like pointers in a table/collection. They are used in enumerables and enumerators, just anything involving looping a collection. 
                    //Yield gets you one item at a time till it eventaully gets you all the items. With yeild your code is converted to a state machine, and C# will remember at what point you were in fetching an item.
                }
                //return people.GetEnumerator() as IEnumerator<Person>;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return $"{FirstName} {LastName}";
            }
        }
    }

    
}
