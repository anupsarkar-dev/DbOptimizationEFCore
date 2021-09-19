using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DBOptimizedDotNet.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBOptimizedDotNet
{
    public class Program
    {
        //private class Config : ManualConfig
        //{
        //    public Config()
        //    {
        //        AddJob(Job.Dry);
        //        AddLogger(DefaultConfig.Instance.GetLoggers().ToArray());
        //        AddColumn(TargetMethodColumn.Method, StatisticColumn.Max);
        //        AddExporter(RPlotExporter.Default, CsvExporter.Default);
        //        AddAnalyser(EnvironmentAnalyser.Default);
        //        UnionRule = ConfigUnionRule.AlwaysUseLocal;
        //    }
        //}


        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            
            CreateHostBuilder(args).Build().Run();
            //BenchmarkRunner.Run<LeadersController>();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
