using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TodoServiceLibrary.WcfLogging;

namespace TodoServiceLibrary
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
  [FloggingErrorHandler]
  public class Service : IService
  {
    string _connStr;

    public Service()
    {
      _connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"]
        .ConnectionString;
    }

    public List<ToDoItem> GetToDoList()
    {
      using (var db = new ToDoDbContext(_connStr))
          return db.ToDoItems.ToList();
    }

    public void UpdateItem(ToDoItem todo)
    {
      using (var db = new ToDoDbContext(_connStr))
      {
        var todoItem = db.ToDoItems.First(p => p.Id == todo.Id);
        todoItem.Completed = todo.Completed;
        todoItem.Item = todo.Item;
        db.SaveChanges();
      }
    }
  }
}
