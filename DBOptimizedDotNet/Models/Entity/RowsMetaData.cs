using System;

namespace DBOptimizedDotNet.Models.Entity
{
    public record RowMetaData
    {
        public RowMetaData(string key,   object value)
        {
            Key = key;
            //Type = type;
            Value = value;
        }

        public string Key { get; set; }
        //public Type Type { get; set; }
        public object Value { get; set; }
    }
}
