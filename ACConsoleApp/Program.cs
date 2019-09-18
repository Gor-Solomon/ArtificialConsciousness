using AC.DAL;
using AC.DAL.EF;
using AC.Environment;
using AC.Environment.DataLayer.Nodes.Implementations;
using AC.Environment.DataLayer.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.DAL.Repositories.Interfaces;
using AC.DAL.Repositories.Implementations;
using AC.BLL.Models;
using System.Data.Entity;

namespace AC.ConsoleApp
{


    class Program
    {
        static void Main(string[] args)
        {


            using (ACDatabaseEntities dbContext = new ACDatabaseEntities())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Configuration.LazyLoadingEnabled = false;

                var xxx = dbContext.Graphs.Include(p => p.EnteryNode).Include(p => p.Nodes).FirstOrDefault();

                if (xxx != null)
                {
                    xxx.EnteryNodeId = 2;
                    dbContext.SaveChanges();
                }

             //   Console.WriteLine(414);
                //var x = dbContext.Nodes.Find(1);
                //var y = dbContext.Nodes.Find(10);

                //var a = x.NodeAttributes.Select(fx => fx.Attribute);
                //var b = y.NodeAttributes.Select(fx => fx.Attribute);

                //var x = dbContext.Graphs.Add(new Graph() { Name = "YEHOVA" });
                //var g = new Graph() { Name = "YEHOVA", Id = 1 };
                //dbContext.Graphs.Attach(g);

                //dbContext.Nodes.Add(new DAL.EF.Node() { Name = "MICHAEL", GraphId = 1, Data = "FAFA", });

                //INodeRepository nodeRepository = new NodeRepository(dbContext);
                //nodeRepository.EnsureTransaction();
                //nodeRepository.Add(new DAL.EF.Node() { Name = "MICHAEL", GraphId = 1, Data = "FAFA", });
                //Console.WriteLine(nodeRepository.SaveChangesAsync().Result);
                //nodeRepository.Dispose();
            }
        }
    }
}
