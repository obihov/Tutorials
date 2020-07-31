using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Tutorial
/*
 * Two versions of GenericInterfaceExample (2 and 3) have been done.
 * This version (2) here, is simple. 
 * However, with this approach all your Repository will be equipped with the same approach used to interacting with the database.
 * If you intend down the road later to have some of your repo talk to a file, a oracle server or a microsoft sql server, 
 * then this approach will fail you.
 * 
 * internal class Repository<T> : IRepository<T> where T : class, new() {  }
 * IRepository<Product> productRepository = new Repository<Product>();
 * Product product = productRepository.GetById(10);
 * IRepository<Inventory> inventoryRepository = new Repository<Inventory>();
 * Inventory inventory = inventoryRepository.GetById(1211);
 * 
 * So from above, both productRepository and inventoryRepository will interact the same way with the GetById() method.
 * If that method connects to Oracle, then both repo will always connect to Oracle.
 * If a situation arise where Product data have been moved Mircrosoft SQL Server, then you will be forced to re-write your GetById().
 * This can hurt code mentainability, and what if tomorrow management ask you to include MySQL, File, Access etc. Now this becomes a problem.
 * 
 * Version (3) shows a better approach where you have
 * dbos implement a IEntity interface. Making dbos is-a IEntity.
 * You create a IRepository that accepts only dbo types that is-a IEntity.
 * You create concrete Repository for each of your Dbo. The concrete Repository implements the IRepository.
 * Now you can have unique Repository (ProductRepo, InventoryRepo, StudentRepo, UniversityRepo etc). 
 * All repos will have the same contract defined in the IRepository, but each repository will provide its own unique implementation of these contracts.
 * This way, your GetById method in ProductRepo can be implemented differently than that of the StudentRepo.
 * ProductRepo.GetById(10) could communicate with Oracle.
 * StudentRepo.GetById(200) could communicate with Microsoft SQL Server. etc
 * 
 * */
#endregion

namespace Chapter2.Example_Interface
{
    internal abstract class SomeAbs
    {
        internal abstract int Age { get; set; }
        internal abstract GenericInterfaceExample3 GenericInterfaceExample { get; }
        internal abstract event Action RunSomething; /*{ add { value += value; } remove { value -= value; } }*/
        internal string Name;
    }
    internal class GenericInterfaceExample3
    {
        static public void Run()
        {

            IRepository2<StudentDbo> studentRepository = new StudentRepo2();
            IRepository2<UniversityDbo> universityRepository = new UniversityRepo2();

            //Note how version 3 offers great flexibility. 
            //At anytime you can change the SaveNew method for any of the repository to point to Oracle, MSSQL, MySQL, a file etc.
            //Doing that won't affect the way other repos' SaveNew() implementation works.
            //This is a better approach than Version 2
            studentRepository.SaveNew(new StudentDbo());
            universityRepository.SaveNew(new UniversityDbo());
        }
    }

    #region Step Summary
    /*
     * Step 1
     * =======
     * To access data from the database, you need database objects. 
     * You want your database objects to respect certain contracts.
     * For this you create an interface called IEntity, which all your database object must implement to respect the contract.
     * 
     * Step 2
     * =======
     * You can create multiple database object that maps to SQL tables in your database.
     * So you can create StudentDbo, UniveristyDbo which maps to Student and University tables respectively in your database.
     * More importantly is, the database objects you create implements the IEntity interface.
     * In other words, every database objects establishes a "is-a" relationship with the IEntity interface.
     * Therefore, wherever IEntity is required, you may subsitute with StudentDbo or UniversityDbo.
     * 
     * Step 3
     * ======
     * You need an interface that will serve as a contract for building the operations needed to interact with the database.
     * This interface is going to be called IRepository and it will associate itself to database objects that have a "is-a" relationship with IEntity.
     * Therefore you create a generic IRepository<T> interface, where T is a type of IEntity (i.e. StudentRepo, UniversityRepo etc).
     * The contracts contained in the IRepository will outline necessary CRUD operations to be performed using the entity types.
     * 
     * Step 4
     * =======
     * Now you create multiple repos.
     * Each repo created will be used to communicating with the database and performing CRUD operations that directly affect a specific entity type.
     * As an example, you will have a StudentRepo. 
     * This repo will implement the IRepository and provide implementation for all CRUD operations required to communicate to the database.
     * The implementation of the CRUD operations can be done differently that CRUD operations implemented in other repo.
     * CRUD operations in StudentRepo may interact with MicrosoftSQL Server to impact StudentDbo
     * CRUD operations in UniversityRepo may interact with Oracle Server to impact UniversityDbo etc.
     * */
    #endregion

    #region Step 1
    internal interface IEntity
    {
        int Id { get; set; }
    }
    #endregion

    #region Step 2
    internal class StudentDbo : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }

    internal class UniversityDbo : IEntity
    {
        public int Id { get; set; }
        public string UniversityName { get; set; }
        public int Year { get; set; }
    }
    #endregion

    #region Step 3
    //You need a repository that accepts only types that implemented the IEntity interface
    internal interface IRepository2<T> where T : IEntity
    {
        void SaveNew(T t);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
    #endregion

    #region Step 4
    //Define concrete repos for each IEntity type, that you wish to access its data from the database
    internal class StudentRepo2 : IRepository2<StudentDbo>
    {
        public void SaveNew(StudentDbo student) { Console.WriteLine("New Student Saved to Microsoft SQL Server."); }
        public StudentDbo GetById(int id) { return new StudentDbo(); }
        public IEnumerable<StudentDbo> GetAll() { return new List<StudentDbo> { }; }
    }

    internal class UniversityRepo2 : IRepository2<UniversityDbo>
    {
        public void SaveNew(UniversityDbo university) { Console.WriteLine("New University Saved to Oracle Server."); }
        public UniversityDbo GetById(int id) { return new UniversityDbo(); }
        public IEnumerable<UniversityDbo> GetAll() { return new List<UniversityDbo> { }; }
    }
    #endregion


}
