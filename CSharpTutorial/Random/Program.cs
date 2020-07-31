using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Random
{
    public class Program : IDisposable
    {
        public static void Main(string[] args)
        {

            IStudent student = new Student("Obinna", "Mogbogu");
            bool isStudent = student is IStudent;
            bool isStudent2 = student is Student;
            Student student2 = student as Student;
            Console.WriteLine();



        }

        public void Dispose()
        {
            this.Dispose();
        }
    }

    public interface ISchool
    {

    }

    public interface IStudent
    {
        string GetFullName();
        string GetEmail();
    }

    public class Student : IStudent
    {
        readonly private string FirstName;
        readonly private string LastName;

        

        public Student(string firstname = "N/A", string lastname = "N/A")
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public string GetFullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }

        public string GetEmail()
        {
            var email = $"{this.FirstName.Substring(0, 3)}.{this.LastName.Substring(0, 3)}@gmail.com";
            return email;
        }
    }
}


