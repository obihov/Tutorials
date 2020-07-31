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
    static internal class GenericInterfaceExample2
    {
        static public void Run()
        {
            //With Generic interface, you can 
            IRepository<ProductDbo> productRepository = new Repository<ProductDbo>();
            IRepository<InventoryDbo> inventoryRepository = new Repository<InventoryDbo>();
            IRepository<CustomerDbo> customerRepository = new Repository<CustomerDbo>();

            //Note all the SaveNew operation for product,inventory,and customer repository behave the same way. Not a flexible code.
            //Version 3, offers you more flexible approach. But if you feel this is how you want it, then this version is fine.
            productRepository.SaveNew(new ProductDbo());
            inventoryRepository.SaveNew(new InventoryDbo());
            customerRepository.SaveNew(new CustomerDbo());
        }
    }


    #region Step 1 - Create dbos.
    internal class ProductDbo { }
    internal class InventoryDbo { }
    internal class CustomerDbo { }
    #endregion

    #region Step 2 - Create IRepository that accepts dbos of type class, define contract
    internal interface IRepository<T> where T : class, new()
    {
        void SaveNew(T t);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
    #endregion

    #region Step 3 - Create Repository that accepts a dbo of type class. Repository should provide implementation for all the IRepository contracts.
    internal class Repository<T> : IRepository<T> where T : class, new() //class and class has a constructor
    {
        public void SaveNew(T t) { Console.WriteLine("New entry Saved to Microsoft SQL Server."); }

        public IEnumerable<T> GetAll() { return new List<T>(); }

        public T GetById(int id) { return default(T); }
    }
    #endregion

    
}
