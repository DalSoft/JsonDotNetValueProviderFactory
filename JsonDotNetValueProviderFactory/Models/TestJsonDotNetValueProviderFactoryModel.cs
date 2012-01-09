using System;

namespace JsonDotNetValueProviderFactoryTestHarness.Models
{
    public class TestJsonDotNetValueProviderFactoryModel
    {
        public bool BoolProperty { get; set; }
        public char CharProperty { get; set; }
        public string DateTimeProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public dynamic DynamicProperty { get; set; }
        public float FloatProperty { get; set; }
        public int IntProperty { get; set; }
        public long LongProperty { get; set; }
        public short ShortProperty { get; set; }
        public string StringProperty { get; set; }
        public uint UintProperty { get; set; }
        public ulong UlongProperty { get; set; }
        public ushort UshortProperty { get; set; }

    }
    public static class TestJsonDotNetValueProviderFactoryModelExtensions
    {
        public static TestJsonDotNetValueProviderFactoryModel LoadTestData(this TestJsonDotNetValueProviderFactoryModel model)
        {
            model.BoolProperty = false;
            model.CharProperty = char.Parse("A");
            model.DateTimeProperty = DateTime.MaxValue.ToString("yyyy/MM/dd");
            model.DecimalProperty = decimal.MaxValue;
            model.FloatProperty = float.MaxValue;
            model.IntProperty = int.MaxValue;
            model.LongProperty = long.MaxValue;
            model.ShortProperty = short.MaxValue;
            model.StringProperty = "StringProperty value";
            model.UintProperty = uint.MaxValue;
            model.UlongProperty = ulong.MaxValue;
            model.UshortProperty = ushort.MaxValue;

            return model;
        }
    
    }
}