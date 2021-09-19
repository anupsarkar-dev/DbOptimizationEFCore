using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
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

    //[DryJob]
    //[JsonExporterAttribute.Brief]
    //[JsonExporter("-custom", indentJson: true, excludeMeasurements: true)]
    public class LeadersController : Controller
    {
        AppDbContext context;

       

        public LeadersController()
        {
            context = new AppDbContext();
            context.ChangeTracker.AutoDetectChangesEnabled = false;
      
        }

        //[Benchmark]
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


            var n = 100000;
            while (i <= n)
            {
                var clone = (Leader)leader.Clone();
                
                clone.Guid = Guid.NewGuid();
                leaders.Add(clone);
                //  context.Leaders.Add(clone);
                i++;
                clone = null;
            }


             context.Leaders.AddRange(leaders);

             context.SaveChanges();

             Debug.WriteLine($"Total {n} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds/1000} sec");



            return Json($"Total {n} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");
        }
    }
}
