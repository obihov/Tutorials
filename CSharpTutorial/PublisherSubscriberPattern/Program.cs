using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherSubscriberPattern
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public bool IsEnrolled { get; set; }
    }

    //Additional information you will want to send to subscriber after student is registered
    public class StudentEventArgs : EventArgs
    {
        public Student RegisteredStudent { get; set; }
    }

    public delegate void StudentRegisteredEventHandler(object source, StudentEventArgs args);

    public class PublisherStudent
    {
        public event StudentRegisteredEventHandler studentRegistered = delegate (object src, StudentEventArgs args) { }; //or do delegate{}

        public void Register(Student student)
        {
            Console.WriteLine($"Registering new student: {student.FirstName} {student.LastName}");
            this.OnRegistered(student); //raise the event after registering Student
        }

        //should be protected virtual. So only child classes can access and override it.
        protected virtual void OnRegistered(Student registeredStudent)
        {
            //Triggers subscriber methods attached (like send email and text after student is registered)
            studentRegistered(this, new StudentEventArgs() { RegisteredStudent = registeredStudent });
        }
    }

    public class SubscriberEmail
    {
        public void OnRegistered(object source, StudentEventArgs args)
        {
            //sends an email after a student is registered
            var firstname = args.RegisteredStudent.FirstName;
            var lastname = args.RegisteredStudent.LastName;
            var email = $"{firstname.Substring(0, 3)}.{lastname.Substring(0, 3)}@alamo.edu";

            Console.WriteLine($"Sent to Email: Your student registration details has been sent to {email}");
        }
    }

    public class SubscriberText
    {
        public void OnRegistered(object source, StudentEventArgs args)
        {
            //sends an email after a student is registered
            var firstname = args.RegisteredStudent.FirstName;
            var lastname = args.RegisteredStudent.LastName;
            var email = $"{firstname.Substring(0, 3)}.{lastname.Substring(0, 3)}@alamo.edu";

            Console.WriteLine($"Sent via Text: {firstname} {lastname}, your student registration was successful.");
        }
    }



    public class Program
    {
        public static void Main(string[] args)
        {
            //Get all Subscribers
            SubscriberEmail subscriberEmail = new SubscriberEmail();
            SubscriberText subscriberText = new SubscriberText();

            //Get the publisher
            PublisherStudent publisherStudent = new PublisherStudent();

            /*
             * Subscribe to the publisher's event. 
             * Always Do this before running publisherStudent.Register(student)
             * because internally the OnRegistered method, which executes all subscriber methods is called last within the publisherStudent.Register method.
             * The Observer pattern can allow us to subscribe/attach more methods that will be triggered automatically after an event occurs.
             * So we can add a subscriber that can send a tweet, or share our new student registration, or create a gym account for our new student after they are registered.
             * You can have all subcriber in One class. So we can have instead - OnRegisteredEmail, OnRegisteredText, OnRegisteredGym, OnRegisteredTweet etc in a class.
             * */
            publisherStudent.studentRegistered += subscriberEmail.OnRegistered;
            publisherStudent.studentRegistered += subscriberText.OnRegistered;

            //Register a new Student
            Student student = new Student
            {
                ID = 1,
                FirstName = "Obinna",
                LastName = "Mogbogu",
                Department = "IT",
                IsEnrolled = true
            };
            publisherStudent.Register(student);
        }
    }
}
