using Flogging.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoServiceLibrary
{
  public class ToDoDbContext : DbContext
  {
    public ToDoDbContext(string connectionString): base(connectionString)
    {
      DbInterception.Add(new FloggerEFInterceptor());
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
  }
}
