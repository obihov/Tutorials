using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_NETInterfaces
{
    internal static class ComparableExample
    {
        static public void Run()
        {
            BasicCompareExample();
            AdvancedCompareExample();
        }


        /* .NET automatically associates the IComparable type to all object.
         * Thus, all valueTypes and several .NET library classes inherit the IComparable interface. 
         * Note: Mismatch types cannot be compared.
         * */
        static private void BasicCompareExample()
        {
            var obiAge = 20;
            var obiName = "Obinna";
            var kinisAge = 10;
            var kinisName = "Kinis";

            var ageCompareResult = obiAge.CompareTo(kinisAge);
            var nameComapreResult = obiName.CompareTo(kinisName);
            switch (nameComapreResult)
            {
                case 1: { Console.WriteLine($"{obiName} comes after {kinisName}"); } break;
                case 0: { Console.WriteLine($"{obiName} comes before {kinisName}"); } break;
                case -1: { Console.WriteLine($"{obiName} is same position as {kinisName}"); } break;                
            }
            switch (ageCompareResult)
            {
                case 1: { Console.WriteLine($"{obiAge} comes after {kinisAge}"); } break;
                case 0: { Console.WriteLine($"{obiAge} is same position as {kinisAge}"); } break;
                case -1: { Console.WriteLine($"{obiAge}  comes before  {kinisAge}"); } break;
            }

            //Mismatch types cannot be compared. A ArgumentException is thrown.
            //var compareResult1 = true.CompareTo(0);
            //var compareResult2 = 1.CompareTo("market");            
        }



         /* .NET automatically associates the IComparable type to all object.
         * Thus, all valueTypes and several .NET library classes inherit the IComparable interface. 
         * Note: Mismatch types cannot be compared.
         * Note: In this example, different instance of the Order class can be compared with each other.
         * So a order1 can be compared to a order2 to determine its sort order (1: after, 0: same, -1: before).
         * Note: To compare a custom class created by you, you class must implement either or both of the generic/non-generic IComparable interface.
         * Note: Just note that the generic version of the IComparable interface offers better performance than the non-generic one which requires boxing and unboxing via casting.
         * */
        static private void AdvancedCompareExample()
        {
            #region OrderV1 - Using generic IComparable version
            OrderV1 obiOrder = new OrderV1
            {
                OrderId = 1,
                OrderBy = "Obi",
                OrderItemName = "PS4",
                OrderDate = new DateTime(year: 2020, month: 07, day: 16, hour: 15, minute: 00, second: 00) //07/16/2020 @ 03:00 pm
            };
            OrderV1 kinisOrder = new OrderV1
            {
                OrderId = 1,
                OrderBy = "Kinis",
                OrderItemName = "Necklace",
                OrderDate = new DateTime(year: 2020, month: 07, day: 16, hour: 20, minute: 30, second: 00)//07/16/2020 @ 08:30 pm
            };

            //An overload for CompareTo exist - one accepts Order, another accepts object. Because the Order class implemented both the generic and non-generic version of the IComparable interface.
            var compareResult = obiOrder.CompareTo(kinisOrder);
            switch (compareResult)
            {
                case 1: { Console.WriteLine("obiOrder was ordered after kinisOrder."); } break;
                case 0: { Console.WriteLine("obiOrder was ordered same time as kinisOrder."); } break;
                case -1: { Console.WriteLine("obiOrder was ordered before kinisOrder."); } break;
            }
            #endregion

            #region OrderV2 - Using non-generic IComparable version
            OrderV2 obiOrderV2 = new OrderV2
            {
                OrderId = 1,
                OrderBy = "Obi",
                OrderItemName = "PS4",
                OrderDate = new DateTime(year: 2020, month: 07, day: 16, hour: 15, minute: 00, second: 00) //07/16/2020 @ 03:00 pm
            };
            OrderV2 kinisOrderV2 = new OrderV2
            {
                OrderId = 1,
                OrderBy = "Kinis",
                OrderItemName = "Necklace",
                OrderDate = new DateTime(year: 2020, month: 07, day: 16, hour: 20, minute: 30, second: 00)//07/16/2020 @ 08:30 pm
            };

            //An overload for CompareTo exist - one accepts Order, another accepts object. Because the Order class implemented both the generic and non-generic version of the IComparable interface.
            var compareResultV2 = obiOrderV2.CompareTo(kinisOrderV2);
            switch (compareResultV2)
            {
                case 1: { Console.WriteLine("obiOrder was ordered after kinisOrder."); } break;
                case 0: { Console.WriteLine("obiOrder was ordered same time as kinisOrder."); } break;
                case -1: { Console.WriteLine("obiOrder was ordered before kinisOrder."); } break;
            }
            #endregion

        }

        
    }

    //OrderV1 uses the generic IComparable interface, which has performance advantage over the non-generic IComparable interface, since no issue with boxing and unboxing.
    internal class OrderV1 : IComparable<OrderV1>
    {
        public int OrderId { get; set; }
        public string OrderBy { get; set; }
        public string OrderItemName { get; set; }
        public DateTime OrderDate { get; set; }

        /*
         * This approach use the generic version of the IComparable<Order> interface which offers better performance as opposed to the non-generic version.
         * Good when you know exactly what the Type is you want to be comparing with or against.
         * Note: Do Not compare between the objects itself as this will force a recursive operation and a never ending execution. So avoid doing order1.CompareTo(order2)
         * Note: Compare with properties in both objects to determine how the sort against each other. So order1.OrderDate.CompareTo(order2.OrderDate)
         * */
        public int CompareTo(OrderV1 order2)
        {
            if(order2 is null)
            {
                return 1;
            }

            switch (this.OrderDate.CompareTo(order2.OrderDate))
            {
                case 1: { return 1; }       //order1 comes after order2
                case 0: { return 0; }       //order1 is same position as order2
                case -1: { return -1; }     //order1 comes before order2
            }

            throw new ArgumentException("Order cannot be sorted.");

        }
    }


    //OrderV2 uses the non-generic IComparable interface, which has performance gaps due to its boxing and unboxing.
    internal class OrderV2 : IComparable
    {
        public int OrderId { get; set; }
        public string OrderBy { get; set; }
        public string OrderItemName { get; set; }
        public DateTime OrderDate { get; set; }

        /*
         * This approach use the non-generic version of the IComparable interface. Consider using the non-generic version for better performance to avoid boxing and unboxing issues.
         * Good when you don't know exactly what the comparing Type will be.
         * Note: Do Not compare between the objects itself as this will force a recursive operation and a never ending execution. So avoid doing order1.CompareTo(order2)
         * Note: Compare with properties in both objects to determine how the sort against each other. So order1.OrderDate.CompareTo(order2.OrderDate)
         * */
        public int CompareTo(object obj)
        {
            OrderV2 order2 = obj as OrderV2;
            if (order2 is null)//if order to compare against is empty then return 1 to indicate order1 (Left hand) comes after or greater than the empty order2 (Right hand)
            {
                return 1;   //meaning order1 comes after order2 (even though there wasn't an actual order2)
            }

            switch (this.OrderDate.CompareTo(order2.OrderDate))
            {
                case 1: { return 1; }   //order1 comes after order2
                case 0: { return 0; }   //order1 is same position as order2
                case -1: { return -1; } //order1 comes before order2
            }

            throw new ArgumentException("Order cannot be sorted.");
        }
    }



}
