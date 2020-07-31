using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup stream and create object to be serialized
            string path = Environment.CurrentDirectory + @"\newobi.txt";
            
            Car car1 = new Car("Honda", "Accord", "2006", new Owner("Zorya", "Ken"));
            Car car2 = new Car("BMW", "M3", "2016", new Owner("Obinna", "Mogbogu"));
            Car car3 = new Car("Ford", "XL", "2020", new Owner("Sonachi", "Mogbogu"));
            Car car4 = new Car("Mercedes", "SLS", "2018", new Owner("Kinis", "Kin"));
            Car car5 = new Car("Porshe", "911", "2010", new Owner("Ralu", "Mogbogu"));
                       

            #region BinaryFormatter to serialize/deserialize using either Streams (file, memory)
            var binaryPath = Environment.CurrentDirectory + $@"\binaryFile.txt";
            
            //BinaryFormatter using FileStream to Serialize
            using (FileStream fileStream = new FileStream(binaryPath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, car1);
                formatter.Serialize(fileStream, car2);
                formatter.Serialize(fileStream, car3);
                formatter.Serialize(fileStream, car4);
                formatter.Serialize(fileStream, car5);
                //fileStream.Flush();
                //fileStream?.Close();
            }

            //BinaryFormatter using FileStream to Deserialize
            using (FileStream fileStream = new FileStream(binaryPath, FileMode.Open))
            {
                fileStream.Position = 0;
                object desObj = null;
                while (fileStream.CanSeek)
                {
                    try
                    {
                        desObj = new BinaryFormatter().Deserialize(fileStream);
                        Car car = (Car)desObj;
                        Console.WriteLine(
                            $@"Deserialized Car:
                            Make: {car.Make}
                            Model: {car.Model}
                            Year: {car.Year}
                            Owner: {car.Owner._firstName} {car.Owner._lastName}");
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
            }

            //BinaryFormatter using MemoryStream to Serialize
            byte[] dataInMemory1 = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            
            using (MemoryStream stream = new MemoryStream(7000))
            {
                binaryFormatter.Serialize(stream, car1);
                dataInMemory1 = stream.GetBuffer();
            }

            //BinaryFormatter using MemoryStream to Deserialize
            using (MemoryStream stream = new MemoryStream(dataInMemory1))
            {
                stream.Position = 0;
                Car car = (Car)binaryFormatter.Deserialize(stream);                
            }
            #endregion


            #region JsonSerializer to serialize/deserialize using either Streams (file, memory). Works with Text/StreamWriter, Text/StreamReader, JsonReader/Writer.
            var jsonPath = Environment.CurrentDirectory + $@"\jsonFile.txt";

            //JsonSerializer using FileStream to Serialize
            using (System.IO.FileStream stream = new FileStream(jsonPath, FileMode.OpenOrCreate))
            {
                TextWriter textWriter = new StreamWriter(stream);
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Formatting = Formatting.Indented; //force proper identing of data
                jsonSerializer.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); //force camel case types - firstName, lastName
                jsonSerializer.Serialize(textWriter, car1);
                textWriter.Flush();
                textWriter.Close();
            }

            //JsonSerializer using FileStream to Deserialize
            using (System.IO.FileStream stream = new FileStream(jsonPath, FileMode.OpenOrCreate))
            {
                TextReader textReader = new StreamReader(stream);
                JsonSerializer jsonSerializer = new JsonSerializer();
                Car car = (Car) jsonSerializer.Deserialize(textReader, typeof(Car));                
                textReader.Close();
            }

            //JsonSerializer using MemoryStream to Serialize
            byte[] dataInMemory2 = null;
            using (MemoryStream stream = new MemoryStream(7000))
            {
                TextWriter textWriter = new StreamWriter(stream);
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(textWriter, car1);
                dataInMemory2 = stream.GetBuffer();
                textWriter.Flush();
                textWriter.Close();
            }

            //JsonSerializer using MemoryStream to Deserialize
            using (MemoryStream stream = new MemoryStream(dataInMemory2))
            {
                TextReader textReader = new StreamReader(stream);
                JsonSerializer jsonSerializer = new JsonSerializer();
                Car car = (Car)jsonSerializer.Deserialize(textReader, typeof(Car));
                textReader.Close();
            }
            #endregion


            #region XmlSerializer to serialize/deserialize using either Streams (file, memory). Works with Text/StreamWriter, Text/StreamReader, JsonReader/Writer.
            var xmlPath = Environment.CurrentDirectory + $@"\xmlFile.txt";

            //XmlSerializer using FileStream to Serialize
            using (System.IO.FileStream stream = new FileStream(xmlPath, FileMode.OpenOrCreate))
            {
                TextWriter textWriter = new StreamWriter(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Car), new Type[] { typeof(Owner) } );
                xmlSerializer.Serialize(stream, car1);
                textWriter.Flush();
                textWriter.Close();
            }

            //XmlSerializer using FileStream to Deserialize
            using (System.IO.FileStream stream = new FileStream(xmlPath, FileMode.OpenOrCreate))
            {
                TextReader textReader = new StreamReader(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Car), new Type[] { typeof(Owner) });
                Car car = (Car)xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            //XmlSerializer using MemoryStream to Serialize
            byte[] dataInMemory3 = null;
            using (MemoryStream stream = new MemoryStream(7000))
            {
                TextWriter textWriter = new StreamWriter(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Car), new Type[] { typeof(Owner) });
                xmlSerializer.Serialize(textWriter, car1);
                dataInMemory3 = stream.GetBuffer();
                textWriter.Flush();
                textWriter.Close();
            }

            //XmlSerializer using MemoryStream to Deserialize
            using (MemoryStream stream = new MemoryStream(dataInMemory3))
            {
                TextReader textReader = new StreamReader(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Car), new Type[] { typeof(Owner) });
                Car car = (Car)xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            //--------------------------------------------------------------------------------------


            #endregion

        }
    }


    /*
     * We want to implement the ISerializable interface so that we have control over
     * how and what gets serialized; this is refered to as "Custom Serialization".
     * if we did not implement the ISerializable interface, 
     * then all of our public and private data members are serialized behind the scenes and we would need a default constructor
     * -------------
     * ISerializable requires implementation of the GetObjectData method.
     * If Car is a base class then the GetObjectData method is virtual to allow overriding by child objects. The GetObjectData method can be public/protected. The constructors can be protected, public, or private.
     * If Car is not a base class, then we make Car a sealed class. The GetObjectData method can be public. The constructors can be public or private.
     * -------------
     * In a sealed class, the GetObjectData is automatically called during serialization.
     * In a child class, the base.GetObjectData() is called first within the overriden public override void GetObjectData method in the child class.
     * -------------
     * Because we are supporting Custom Serialization, we can store other information, such as version information 
     * that we could retrieve during the deserialization process and use this information to determine which name\value pair(s) should exist or 
     * if a value was stored as a different data type. 
     * This is a very powerful way to handle changes you make to the Serialization process, but still want to support older versions.
     * 
     * */
    [Serializable]
    public sealed class Car : ISerializable
    {
        //with the ISerializable, we can decide what and how we wish to serialize any of the private and public data members
        readonly public string Make;
        readonly public string Model;
        readonly public string Year;
        readonly public Owner Owner;

        private Car() { }   //default constructor is for internal use only and does not guarantee proper initialization.

        public Car(string make, string model, string year, Owner owner)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.Owner = owner ?? new Owner();
        }

        //When you do formatter.Deserialize(stream), this constructor is automatically called.
        //It can be private or protected
        //This allows you to Deserialize from bytes of data to the object Car.
        private Car(SerializationInfo info, StreamingContext context)
        {
            //int len = info.GetInt32("")
            this.Make = info.GetString("Make");
            this.Model = info.GetString("Model");
            this.Year = info.GetString("Year");
            this.Owner = (Owner) info.GetValue("Owner", typeof(Owner)); //Owner must be serializable. Just know that the Owner.Ctor(info, context) constructor must be available. All custom types you try to deserialize must have the special constructor in them, else deserialization will not work.
        }

        //When you do formatter.Serialize(stream, car), this method is automatically called.
        //This allows you to serialize the object Car to bytes of data and store on a defined stream (i.e. disk, file, database, memory etc)
        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Make", this.Make);
            info.AddValue("Model", this.Model);
            info.AddValue("Year", this.Year);
            info.AddValue("Owner", this.Owner, typeof(Owner));  //Owner must be serializable. Just know that the Owner.GetObjectData method is also invoked behind the scense to serialize the Owner type. All custom types you try to serialize must have the GetObjectData method. Without it, serialization won't work.

        }
    }

    [Serializable]
    public class Owner
    {
        //without the ISerializable, we cannot decide what and how we wish to serialize any of the private and public data members
        public string _firstName = string.Empty;
        public string _lastName = string.Empty;

        public Owner()
        {
        }
        public Owner(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        //note: this is private to control access; the serializer can still access this constructor.
        //also, if class is not sealed class, you can mark the ctor as protected as well if you want its child classes to access it.
        private Owner(SerializationInfo info, StreamingContext ctxt)
        {
            this._firstName = info.GetString("FirstName");
            this._lastName = info.GetString("LastName");
        }

        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("FirstName", this._firstName);
            info.AddValue("LastName", this._lastName);
        }

    }
}
