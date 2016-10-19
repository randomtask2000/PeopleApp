﻿using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EnPeople.Models
{
    public static class Extensions
    {
        //http://myview.rahulnivi.net/dbset-find-api-missing-entity-framework-core-final-rc1-version/
        public static TEntity Find<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            var context = ((IInfrastructure<IServiceProvider>)set).GetService<DbContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                entries = Enumerable.Where(entries, e => e.Property(property.Name).CurrentValue == keyValues[i]);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                // Return the local object if it exists.
                return entry.Entity;
            }

            // TODO: Build the real LINQ Expression
            // set.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = Queryable.Where(set, (Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query.FirstOrDefault();
        }
        public static int GetAge(this DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }

        public static string InitialCap(this string str)
        {
            if (str == null)
                return null;

            str = str.ToLower(); 

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}