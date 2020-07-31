using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Encapsulation
{
    internal static class UsingPropertiesExample
    {
        static public void Run()
        {
            try
            {
                UsingPropertiesEx user = new UsingPropertiesEx("Kinis Kopea Netonda", "17");

                //display user - info
                Console.WriteLine($"Name: {user.UserName}");
                Console.WriteLine($"Email: {user.UserEmail}");
                Console.WriteLine($"Authorized to work: {user.isAuthorized}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    internal class UsingPropertiesEx
    {
        public UsingPropertiesEx(string fullanme, string age)
        {
            UserName = fullanme;
            UserAge = age;
        }


        #region Tutorial 1
        /*
         * Fields are used as data to your types.
         * A good practice is to restrict access to them by outside users. Inside, it is fine to use them.
         * In Java, you will have to create accessor (Get) and mutator (Set) methods to retrieve or change a field's data.
         * In C#, in place of accessor and mutator methods, you have properties, which have a get; and set;. Use this properties instead to manipuate data for your types.
         * 
         * Note:
         * =====
         * You can use the set properties to support validation. Example.
         * private bool Legal;
         * private int Age;
         * public string UserAge {get; set { Age = int.Parse(value); if(Age > 18){ Legal = true; } }
         * In the above, when you write UserAge = 20, then automatically, Age becomes 20 and Legal becomes true.
         * 
         * The get;set; of a property can also have their own access modifiers.
         * If you want, you can turn a property to a read-only property, where data is set only once, and you are allowed only a read operation afterwards. Simply do,
         * - public get; private set; (like read-only fields, you will need a constructor to assign value to set part of the property, but the get part is publicly accessible.)
         * - public get; only without any setters. (this is a full fledge read-only property, use it to get field data only without caring about setting any value to a field)
         * 
         * if you want a property for a fire and forget kind of operation (which is useful when you just want set a field's value without caring about ever accessing or checking the value), then do,
         * - public set; only without any setters.
         * 
         * Also keep in mind that you can simply just write
         * public string MyProperty {get;set;} or
         * public string MyProperty {public get; private set;} (for readonly with one-time data set)
         * public string MyProperty {public get;} (no setter, use only for reading from a field)
         * public string MyProperty {public set;} (no getter, use only for fire and forget kind of operation to set value to a field.)
         * */
        #endregion

        private string firstName;
        private string middleInitial;
        private string lastName;
        private string email;
        private int Age;
        private bool Legal;

        #region Tutorial 2
        /*
         * your get and set must have access modifiers that are more restrictive than the property itself.
         * So if your (most restrictive) private -> protected internal -> protected -> internal -> public (least restrictive).
         * Now if your property is Public, then your get; and set; can only have internal, protected, protected internal, or private.
         * if property is private, then your get and set can't have anything other than just private.
         * 
         * Also, get; and set; in properties cannot both have access modifiers at the same time. If get has, then set won't and vice-versa. 
         * You can create actual accessor and mutator methods instead of using properties if you want situation where your Getter and Setter can both have access modifiers.
         * */
        #endregion

        public string UserName 
        {
            get { return $"{this.firstName} {this.middleInitial} {this.lastName}"; }
            set 
            { 
                string[] names = value.Split(new string[]{ " " }, StringSplitOptions.None);
                if(names.Length == 3)
                {
                    this.firstName = names[0];
                    this.lastName = names[2];
                    this.middleInitial = names[1].ElementAt(0).ToString();
                }
                else if(names.Length == 2)
                {
                    this.firstName = names[0];
                    this.lastName = names[2];
                }
                else
                {
                    throw new ArgumentException(message: "Please enter fullname as first middle last. If you have more than one middle name, please only use one of your middle name or ignore any use of a middle name.");
                }
            }
        }

        public string UserEmail 
        { 
            get 
            {
                if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                {
                    this.email = $"{firstName}.{lastName}@gmail.com";
                }
                return this.email;
            }
        }

        public string UserAge 
        {
            get { return this.Age.ToString(); }
            set
            {
                bool isValid = int.TryParse(value, out int result);
                if (isValid)
                {
                    this.Age = result;
                    this.Legal = (this.Age >= 18) ? true : false;
                }
                    
                else
                    throw new ArgumentException(message: "Please enter a valid Age. Numerical values only.");
            } 
        }

        public string isAuthorized
        {
            get 
            {
                return (this.Legal)? "Yes" : "No";
            }
        }


    }
}
