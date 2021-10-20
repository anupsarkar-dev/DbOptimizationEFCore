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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DBOptimizedDotNet.Controllers
{

    //[DryJob]
    //[JsonExporterAttribute.Brief]
    //[JsonExporter("-custom", indentJson: true, excludeMeasurements: true)]
    public class LeadersController : Controller
    {
         AppDbContext _context;
        readonly IServiceScopeFactory _serviceScopeFactory;


        public LeadersController( IServiceScopeFactory serviceScopeFactory)
        {

            _serviceScopeFactory = serviceScopeFactory;

            
            
            _context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();             
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
                
        }

        private CancellationToken[] GetCancellationsTokens()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationTokenSource source2 = new CancellationTokenSource();
            CancellationTokenSource source3 = new CancellationTokenSource();

            CancellationToken CancellationToken = source.Token;
            CancellationToken CancellationToken2 = source2.Token;
            CancellationToken CancellationToken3 = source3.Token;

            return new CancellationToken[] { source.Token, source2.Token, source3.Token };
        }

        //[Benchmark]
        public IActionResult Index()
        {

            var stopwatch = Stopwatch.StartNew();
            Debug.WriteLine($"Started....");

            var Now = DateTime.Now;

            Leader leader = new Leader("Mr XYZ", "ZZ10", "016766343433", "1233434", "test@gmail.com", "dhaka,BD", "n/a", "n/a", "n/a",
                "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a", "n/a",
                "n/a", true, true, true, true, true, true, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, Now, Now, Now, Now, Now, Now, Now, Now, Now, Now, Now);



            var i = 1;

            List<Leader> leaders = new();


            var n = 100;
            while (i <= n)
            {
                var clone = (Leader)leader.Clone();

                clone.Guid = Guid.NewGuid();
                leaders.Add(clone);
                //  context.Leaders.Add(clone);
                i++;
                clone = null;
            }


            _context.Leaders.AddRange(leaders);

            _context.SaveChanges();

            Debug.WriteLine($"Total {n} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");



            return Json($"Total {n} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");
        }

        // COUNT RESULT : 5875,11750,23500  ==== Total 41125 Rows To Process Time Taken  : 17117ms , 17 sec
        public async Task<IActionResult> SequentialQuery()
        {
            var stopwatch = Stopwatch.StartNew();
            Debug.WriteLine($"Started....");


            var n = 100000;


            var tokens = GetCancellationsTokens();

            var p1 = await Proc_GetLeadersCount1Async(tokens[0]);
            var p2 = await Proc_GetLeadersCount2Async(tokens[1]);
            var p3 = await Proc_GetLeadersCount3Async(tokens[2]);


            var result = $"COUNT RESULT : {p1.Count},{p2.Count},{p3.Count} ";


            Debug.WriteLine($"Total {p1.Count + p2.Count + p3.Count} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");

            return Json($"{result} ==== Total {p1.Count + p2.Count + p3.Count} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");

        }



        // COUNT RESULT : 5875,11750,23500  ==== Total 41125 Rows To Process Time Taken  : 7425ms , 7 sec

        public async Task<IActionResult> ParallelQuery()
        {
           
            try
            {
                var stopwatch = Stopwatch.StartNew();
                Debug.WriteLine($"Started....");


                var n = 100000;


                var tokens = GetCancellationsTokens();
                var result="";


                using var context1 = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
                using var context2 = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
                using var context3 = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

                var task1 = Proc_GetLeadersCountWithContextAsync(context1, "[dbo].[Proc_GetLeadersCount1]", tokens[0]);
                var task2 = Proc_GetLeadersCountWithContextAsync(context2, "[dbo].[Proc_GetLeadersCount2]", tokens[0]);
                var task3 = Proc_GetLeadersCountWithContextAsync(context3, "[dbo].[Proc_GetLeadersCount3]", tokens[0]);


                var tasks = new[] { task1, task2, task3 };

                await Task.WhenAll(tasks).WithAggregatedExceptions(); ;


                var result1 = await task1;
                var result2 = await task2;
                var result3 = await task3;

                result = $"COUNT RESULT : {result1.Count},{result2.Count},{result3.Count} ";

                 
                Debug.WriteLine($"Total {result1.Count + result2.Count + result3.Count} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");

                return Json($"{result} ==== Total {result1.Count + result2.Count + result3.Count} Rows To Process Time Taken  : {stopwatch.ElapsedMilliseconds}ms , {stopwatch.ElapsedMilliseconds / 1000} sec");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Canceled");
                return Json(new { error = "OperationCanceledException" });
            }
            catch (AggregateException exception)
            {
                //Console.WriteLine("2 or more exceptions");
                // Now the exception that we caught here is an AggregateException, 
                // with two inner exceptions:
                //var aggregate = exception as AggregateException;

                var errors = new StringBuilder();
                foreach (var ex in exception.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    errors.Append(ex.Message);
                }

                return Json(new {error = errors.ToString() });

            }
            catch (Exception exception)
            {
                Console.WriteLine($"Just a single exception: ${exception.Message}");
                return Json(new { error = exception.Message });
            }



        }


        public virtual async Task<List<Leader>> Proc_GetLeadersCount1Async(CancellationToken cancellationToken = default) =>
             await _context.Set<Leader>().FromSqlRaw("[dbo].[Proc_GetLeadersCount1]").ToListAsync(cancellationToken);


        public virtual async Task<List<Leader>> Proc_GetLeadersCount2Async(CancellationToken cancellationToken = default) =>
             await _context.Set<Leader>().FromSqlRaw("[dbo].[Proc_GetLeadersCount2]").ToListAsync(cancellationToken);

        public virtual async Task<List<Leader>> Proc_GetLeadersCount3Async(CancellationToken cancellationToken = default) =>
             await _context.Set<Leader>().FromSqlRaw("[dbo].[Proc_GetLeadersCount3]").ToListAsync(cancellationToken);



        public virtual async Task<List<Leader>> Proc_GetLeadersCountWithContextAsync(AppDbContext context,String q, CancellationToken token  ) =>
             await context.Set<Leader>().FromSqlRaw(q).ToListAsync(token);

    }

    public static class TaskExt2
    {
        /// <summary>
        /// A workaround for getting all of AggregateException.InnerExceptions with try/await/catch
        /// </summary>
        public static Task WithAggregatedExceptions(this Task @this)
        {
            // using AggregateException.Flatten as a bonus
            return @this.ContinueWith(
                continuationFunction: anteTask =>
                    anteTask.IsFaulted &&
                    anteTask.Exception is AggregateException ex &&
                    (ex.InnerExceptions.Count > 1 || ex.InnerException is AggregateException) ?
                    Task.FromException(ex.Flatten()) : anteTask,
                cancellationToken: CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler: TaskScheduler.Default).Unwrap();
        }
    }


}
