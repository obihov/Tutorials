using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Interface
{
    static internal class BasicInterfaceExample
    {
        public static void Run()
        {
            StudentRepository studentRepo = new StudentRepository();
            IStudentRepository studentRepo2 = new StudentRepository();
            
            /*
             * The issue with the above is that you will have to continously define more interface for every Repo you create.
             * With Generics, you can have just one Generic Interface and a Generic implemented type that implements the interface.
             * So the Generic implemented type can take <Student>, <Product>, <University>.
             * */
        }
    }

    interface IStudentRepository
    {
        //fields, properties, indexers (can include delegates, events etc)
        int StudentID { get; }
        IEnumerable<Student> students { get; set; }
        Student this[int index] { get; }

        //methods
        Student GetById(int id);
        void Save(Student student);
    }

    class StudentRepository : IStudentRepository
    {
        //properties
        public int StudentID { get; set; }  //notice that you can define
        public IEnumerable<Student> students { get; set; }
        public Student this[int index]
        {
            get
            {
                return students.ElementAt<Student>(index);
            }
        }

        //methods
        public Student GetById(int id)
        {
            return students.ElementAt<Student>(id); //in real-life this will be like context.Get(id)
        }

        public void Save(Student student)
        {
            Console.WriteLine("Student is saved.");
        }
    }

    class Student
    {

    }
}
