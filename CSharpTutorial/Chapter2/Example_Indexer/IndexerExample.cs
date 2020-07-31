using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Indexer
{
    #region Tutorial

    #endregion

    static public class IndexerExample
    {
        static public void Run()
        {
            ClassRoom classRoom = new ClassRoom();
            var studentAt = classRoom[2]?.Name ?? "None";
            var studentAs = classRoom["Obinna"]?.Name ?? "None";

            Console.WriteLine("Student at index location 0: " + studentAt);
            Console.WriteLine("Student with name: " + studentAs);
        }
    }


    public class Student 
    {
        public string Name;
    }

    
    //A classroom object used a collection for students. 
    //Can access a given student using their index or create custom access like using their Name.
    public class ClassRoom
    {
        //I have forced the students object to be private so users of the ClassRoom class can't query it directly.
        //This will make them use my indexer definitions below.
        private readonly ICollection<Student> students = new List<Student>() 
        { 
            new Student { Name = "Obi" } ,
            new Student { Name = "Chisom" } ,
            new Student { Name = "Sonachi" } ,
        };
        
        //Indexer to get student by their index position in the collection
        public Student this[int index]
        {
            get { return students.ElementAt(index); }
        }

        //Indexer to get student by their Name
        public Student this[string name]
        {
            get { return students.FirstOrDefault(s => s.Name == name); }
        }
    }

    


}
