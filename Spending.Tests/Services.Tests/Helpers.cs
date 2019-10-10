//namespace Spending.Services.Tests
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Diagnostics;
//    using Spending.Data;
//    using System;
//    using System.Runtime.CompilerServices;

//    public class Helpers : IDisposable
//    {
//        private readonly string _databaseName;
//        private static readonly Random _random = new Random();
//        private static readonly InMemoryDatabaseRoot _inMemoryDatabaseRoot = new InMemoryDatabaseRoot();
//        private readonly ApplicationDbContext _context;

//        public Helpers([CallerMemberName]string databaseName = null, string tenantName = null, bool useSqlServerDatabase = false)
//        {
//            _databaseName = databaseName;
//            _context = CreateContext(databaseName);
//        }

//        public static ApplicationDbContext CreateContext([CallerMemberName]string databaseName = null)
//        {
//            return new ApplicationDbContext(CreateDatabaseOptions<ApplicationDbContext>(databaseName));
//        }

//        public static DbContextOptions<T> CreateDatabaseOptions<T>(string databaseName)
//            where T : DbContext
//        {
//            DbContextOptionsBuilder<T> dbContextOptionsBuilder = new DbContextOptionsBuilder<T>()
//                .EnableSensitiveDataLogging()
//                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

//            return dbContextOptionsBuilder.UseInMemoryDatabase(databaseName, _inMemoryDatabaseRoot).Options;
//        }

//        public Spending CreateSpending()
//        {
//            var entity = new Spending
//            {
//                Id = GetRandomId(),
//            };
//            _context.Spendings.Add(entity);
//            _context.SaveChanges();
//            return entity;
//        }

//        public static string GetRandomString()
//        {
//            return Guid.NewGuid().ToString();
//        }

//        public static double GetRandomPercent()
//        {
//            return (_random.NextDouble() + 1) * 50;
//        }

//        public virtual void Dispose()
//        {
//            _context?.Dispose();
//        }

//        private int GetRandomId()
//        {
//            return _random.Next();
//        }
//    }
//}