using DBOptimizedDotNet.Context;
using DBOptimizedDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBOptimizedDotNet.Controllers
{
    public class LeadersController : Controller
    {
        AppDbContext context;

        public LeadersController()
        {
            context = new AppDbContext();
            context.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public IActionResult Index()
        {
            var stopwatch = Stopwatch.StartNew();
            Debug.WriteLine($"Started....");

            var Now = DateTime.Now;

            Leader leader=new Leader( "Mr XYZ","ZZ10","016766343433","1233434","test@gmail.com","dhaka,BD","n/a", "n/a", "n/a", 
                "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", 
                "n/a",true, true, true, true, true,true,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1 ,1, 1,1 ,1,Now, Now, Now, Now, Now, Now, Now, Now, Now, Now, Now);



            var i = 1;

            List<Leader> leaders = new();

            while(i < 1000)
            {
                var clone = (Leader)leader.Clone();
                
                clone.Guid = Guid.NewGuid();
                leaders.Add(clone);
                
                i++;
                clone = null;
            }

            context.Leaders.AddRange(leaders);


            Debug.WriteLine($"Total Time Taken To Process Request : {stopwatch.ElapsedMilliseconds}ms");


            context.SaveChanges();


            return Json($"Total Time Taken To Process Request : {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
