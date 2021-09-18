using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassGenerator;

namespace KoopDB.Extensions
{
    
    public static class KoopDbQueryExtensions
    {
        /*********************** SYNC METHODS ***********************/
        public static List<dynamic> ExecuteQuery(this DbCommand cmd, string sql)
        {
            return cmd.ExecuteQueryAsync(sql, null).Result;
        }
        public static List<dynamic> ExecuteQuery(this DbCommand cmd, string sql, params object[] parameters)
        {
            return cmd.ExecuteQueryAsync(sql, parameters).Result;
        }
        public static List<T> ExecuteQuery<T>(this DbCommand cmd, string sql) where T : new()
        {
            return cmd.ExecuteQueryAsync<T>(sql, null).Result;
        }
        public static List<T> ExecuteQuery<T>(this DbCommand cmd, string sql, params object[] parameters) where T : new()
        {
            return cmd.ExecuteQueryAsync<T>(sql, parameters).Result;
        }

 

        /*********************** ASYNC METHODS ***********************/
        public static async ValueTask<List<dynamic>> ExecuteQueryAsync(this DbCommand cmd, string sql)
        {

            return await cmd.ExecuteQueryAsync(sql, null);
        }
        public static async ValueTask<List<dynamic>> ExecuteQueryAsync(this DbCommand cmd, string sql, params object[] parameters)
        {

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                await cmd.Connection.OpenAsync();

                if (parameters != null)
                {
                    var matches = Regex.Matches(sql, @"[@#]\w+");
                    for (var i = 0; i < matches.Count; i++)
                    {
                        var newParam = cmd.CreateParameter();
                        newParam.ParameterName = "Id";//matches[i].Value;
                        newParam.Value = Convert.ChangeType(parameters[i], parameters[i].GetType(), CultureInfo.InvariantCulture);

                        cmd.Parameters.Add(newParam);
                    }
                }

                var reader = await cmd.ExecuteReaderAsync();


                if (reader is null)
                {
                    cmd.Connection.Close();
                    return new List<dynamic>();
                }

                var dataList = reader.Cast<IDataRecord>().ToList();

                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                //Bu kodu kaldırma
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once HeuristicUnreachableCode
                if (dataList is not { } drl)
                {
                    cmd.Connection.Close();
                    return new List<dynamic>();
                }

                if (drl.Count == 0)
                {
                    cmd.Connection.Close();
                    return new List<dynamic>();
                }


                var dynamicObjectPrint = new Dictionary<string, object>();

                foreach (var name in names)
                {
                    dynamicObjectPrint[name] = drl.First()[name];
                }

                var classBuilder = new MyClassBuilder("KoopObject");
                var createdClass = classBuilder.CreateObject(dynamicObjectPrint);

                var typeToCreate = createdClass?.GetType().GetConstructor(Type.EmptyTypes);

                List<dynamic> result = createdClass.ToDynamicList();

                foreach (var kv in dynamicObjectPrint)
                {
                    createdClass.SetPropertyValue(kv.Key, kv.Value);

                }

                result.Add(createdClass);

                for (var i = 1; i < drl.Count; i++)
                {
                    var newClass = typeToCreate?.Invoke(new object[] { });

                    foreach (var name in names)
                    {
                        newClass.SetPropertyValue(name, drl[i][name]);

                    }

                    result.Add(newClass);
                }

                cmd.Connection.Close();


                return result;



            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                throw new Exception($"{e.Message} \nQuery: {sql}");
            }
        }
        public static async ValueTask<List<T>> ExecuteQueryAsync<T>(this DbCommand cmd, string sql) where T : new()
        {
            return await cmd.ExecuteQueryAsync<T>(sql, null);
        }
        public static async ValueTask<List<T>> ExecuteQueryAsync<T>(this DbCommand cmd, string sql, params object[] parameters) where T : new()
        {

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                await cmd.Connection.OpenAsync();

                if (parameters != null)
                {
                    var matches = Regex.Matches(sql, @"[@#]\w+");
                    for (var i = 0; i < matches.Count; i++)
                    {
                        var newParam = cmd.CreateParameter();
                        newParam.ParameterName = matches[i].Value;
                        newParam.Value = Convert.ChangeType(parameters[i], parameters[i].GetType(), CultureInfo.InvariantCulture);

                        cmd.Parameters.Add(newParam);
                    }
                }



                var reader = await cmd.ExecuteReaderAsync();

                

                if (reader is null)
                {
                    cmd.Connection.Close();
                    return new List<T>();  
                }

                var dataList = reader.Cast<IDataRecord>().ToList();

                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                // Bu kodu kaldırma
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once HeuristicUnreachableCode
                if (dataList is not { } drl)
                {
                    cmd.Connection.Close();
                    return new List<T>();
                }

                if (drl.Count == 0)
                {
                    cmd.Connection.Close();
                    return new List<T>();
                }

                var result = new List<T>();

                foreach (var t in drl)
                {
                    var newClass = new T();

                    foreach (var name in names)
                    {
                        newClass.SetPropertyValue(name, t[name]);

                    }

                    result.Add(newClass);
                }

                cmd.Connection.Close();

                return result;

            }
            catch (Exception e)
            {
                cmd.Connection.Close();

                throw new Exception($"{e.Message} \nQuery: {sql}");
            }
        }
        public static async IAsyncEnumerable<List<T>> StreamQueryAsync<T>(this DbCommand cmd, string sql) where T : new()
        {
            await foreach (var streamData in  cmd.StreamQueryAsync<T>(sql, 100, null))
            {
                yield return streamData;
            }
        }
        public static async IAsyncEnumerable<List<T>> StreamQueryAsync<T>(this DbCommand cmd, string sql, int resultSetSize, params object[] parameters) where T : new()
        {

            var pageIndex = 0;
            bool hasMoreRows;

            var loQuery = $"SELECT * FROM ({sql}) DynamicQuery ORDER BY 1 OFFSET {pageIndex * resultSetSize} ROWS FETCH NEXT {resultSetSize} ROWS ONLY";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = loQuery;

            await cmd.Connection.OpenAsync();

            if (parameters != null)
            {
                var matches = Regex.Matches(sql, @"[@#]\w+");
                for (var i = 0; i < matches.Count; i++)
                {
                    var newParam = cmd.CreateParameter();
                    newParam.ParameterName = matches[i].Value;
                    newParam.Value = Convert.ChangeType(parameters[i], parameters[i].GetType(), CultureInfo.InvariantCulture);

                    cmd.Parameters.Add(newParam);
                }
            }

            do
            {
                var reader = await cmd.ExecuteReaderAsync();
                if (reader is null) yield break;

                var dataList = reader.Cast<IDataRecord>().ToList();
                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                // Bu kodu kaldırma
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once HeuristicUnreachableCode
                if (dataList is not { } drl)
                {
                    cmd.Connection.Close();
                    yield break;
                }

                if (drl.Count == 0)
                {
                    cmd.Connection.Close();
                    yield break;
                }

                var result = new List<T>();

                foreach (var t in drl)
                {
                    var newClass = new T();

                    foreach (var name in names)
                    {
                        newClass.SetPropertyValue(name, t[name]);

                    }

                    result.Add(newClass);
                }
                
                yield return result;

                pageIndex++;
                hasMoreRows = reader.HasRows;
            } while (hasMoreRows);

            cmd.Connection.Close();
        }

       
    }

}