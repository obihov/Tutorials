using System;
using System.Collections;
using System.Collections.Generic;

namespace Chapter2.Example_NETInterfaces
{
    internal class EnumerableExample2
    {
        static public void Run()
        {
            #region Version 1 - Instantiate with House type.
            /*
             * You don't have access to the GetEnumerator. But no worries because all loop types internally will make use of the GetEnumerator. 
             * So any (for/foreach/while etc) will still use the GetEnumerator method you implemented to iterate across items in a collection.
             * Treate the foreach/for/while etc. all these kind of loops as a sytantical sugar of the regular iteration offered when using the enumerator.MoveNext/Current approach.
             * */
            House rooms = new House(new List<Room> {
                new Room { RoomNumber = 10001},
                new Room { RoomNumber = 10011},
                new Room { RoomNumber = 10021},
            });
            foreach (var r in rooms)
            {
                Console.WriteLine(r.RoomNumber);
            }
            //var enumerator = rooms.GetEnumerator() not possible since GetEnumerator method was explicitly implemented. 
            #endregion

            #region Version 2 - Instantiate with IEnumerable<Room>. 
            /*
             * This works fine since House is-a IEnumerable<Room> (i.e. House is collection of rooms / House has one-more rooms in it.)
             * You now have access to the GetEnumerator. Also you may use any loop of your choice. 
             * Both Loop (for/foreach/while etc) and iterating with the enumerator itself, all use the GetEnumerator method.
             * */
            IEnumerable<Room> rooms2 = new House(new List<Room> {
                new Room { RoomNumber = 10001},
                new Room { RoomNumber = 10011},
                new Room { RoomNumber = 10021},
            });
            foreach (var r in rooms2)
            {
                Console.WriteLine(r.RoomNumber);
            }

            //OR

            IEnumerator<Room> enumerator = rooms2.GetEnumerator(); //enumerator is your cursor. Rooms a collection or IEnumerable type is your table.
            while (enumerator.MoveNext()) //MOVE the cursor down through each row in the table
            {
                Console.WriteLine(enumerator.Current.RoomNumber);//fetch value(s) for current row.
            }
            #endregion
        }


        private class House : IEnumerable<Room>
        {
            IEnumerable<Room> rooms;

            public House(IEnumerable<Room> rooms)
            {
                this.rooms = rooms;
            }

            //Generic implementation of GetEnumerator method. System.Collections.Generic
            IEnumerator<Room> IEnumerable<Room>.GetEnumerator()
            {
                foreach (var r in rooms)
                {
                    yield return r;
                }
            }

            //Non-Generic implementation of GetEnumerator method. System.Collections
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (this as IEnumerable<Room>).GetEnumerator();
            }
        }

        private class Room
        {
            public int RoomNumber { get; set; }
        }
    }

    

}
