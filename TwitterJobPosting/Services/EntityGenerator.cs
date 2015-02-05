using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using TwitterJobPosting.Entities;

namespace TwitterJobPosting.Services
{
    public static class EntityGenerator<TEntity> where TEntity: new()
    {
        private static string _chars;
        private static Random _random;

        static EntityGenerator()
        {
            _chars = "        abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789      ";
            _random = new Random();
        }

        public static TEntity GenerateEntity()
        {
            var entity = new TEntity();

            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                var value = GeneratePropertyValue(propertyType);

                property.SetValue(entity, value);
            }

            return entity;
        }

        private static object GeneratePropertyValue(Type type)
        {
            var value = new object();

            if (type == typeof(string))
            {
                value = GenerateString(10);
            }

            if (type == typeof(int) || type == typeof(int?))
            {
                value = GenerateInt();
            }

            if (type == typeof(double) || type == typeof(double?))
            {
                value = GenerateDouble();
            }

            if (type == typeof(bool) || type == typeof(bool?))
            {
                value = GenerateBool();
            }

            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                value = GenerateDateTime();
            }

            if (type == typeof(Guid))
            {
                value = GenerateGuid();
            }

            return value;
        }

        private static string GenerateString(int limit = 255)
        {
            var s = new StringBuilder();
            limit = _random.Next(1, limit);

            for (int i = 0; i < limit; i++)
            {
                s.Append(_chars[_random.Next(_chars.Length)]);
            }

            return s.ToString();
        }

        private static int GenerateInt(int minValue = 1, int maxValue = int.MaxValue)
        {
            var result = _random.Next(minValue, maxValue);

            return result;
        }

        private static double GenerateDouble()
        {
            var result = _random.NextDouble() * 10d;

            return result;
        }

        private static bool GenerateBool()
        {
            return _random.Next() % 2 == 0;
        }

        private static DateTime GenerateDateTime(int startYear = 2015)
        {
            var start = new DateTime(startYear, 1, 1);
            int range = ((TimeSpan)(DateTime.Today - start)).Days;

            start = start.AddDays(_random.Next(range))
                         .AddSeconds(_random.Next(86400));

            return start;
        }

        private static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}