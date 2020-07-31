using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2.Example_Event
{

    static public class EventExample
    {
        static public void Run()
        {
            //Get listeners
            StudentRegistrationListeners listeners = new StudentRegistrationListeners();

            Student student = new Student(10, "Obi");

            //Register Event with listeners
            student.registered += listeners.SendText;
            student.registered += listeners.SendEmail;

            //Register new student
            student.Register();

        }
    }


    public delegate void RegisterDel(object source, EventArgs args);

    public class Student
    {
        public event RegisterDel registered = delegate { };
        readonly private int StudentID;
        readonly private string StudentName;

        //Constructors
        private Student(){}
        public Student(int id, string name)
        {
            this.StudentID = id;
            this.StudentName = name;
        }
        

        public void Register()
        {
            OnRegister(EventArgs.Empty);
        }

        private void OnRegister(EventArgs args)
        {
            registered.Invoke(this, args);
        }
    }


    public class StudentRegistrationListeners
    {
        public void SendText(object source, EventArgs args)
        {
            Console.WriteLine("Text: Student is registered.");
        }

        public void SendEmail(object source, EventArgs args)
        {
            Console.WriteLine("Email: Student is registered.");
        }
    }    
}
