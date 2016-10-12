using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using POS.Domain.Infrastructure;

namespace POS.Domain.Test
{
    public static class TestHelper
    {
        public static Mock<PosContext> GetPosContext()
        {
            var context = new Mock<PosContext>();
            //DbContextFactory.SetContext(context.Object);
            return context;
        }
        public static Mock<DbSet<T>> GetSet<T>(Mock<PosContext> context = null) where T : class
        {
            var dbset = new Mock<DbSet<T>>();
            context?.Setup(c => c.Set<T>()).Returns(dbset.Object);
            return dbset;
        }
        public static Mock<DbSet<T>> GetQueryableSet<T>(IQueryable<T> data, Mock<PosContext> context = null) where T : class
        {
            var dbset = new Mock<DbSet<T>>();
            dbset.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            dbset.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbset.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbset.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            if (context != null)
                context.Setup(c => c.Set<T>()).Returns(dbset.Object);
            return dbset;
        }
        public static Mock<DbSet<T>> GetQueryableSet<T>(IEnumerable<T> list, Mock<PosContext> context = null) where T : class
        {
            var dbset = new Mock<DbSet<T>>();
            var data = list.AsQueryable();
            dbset.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            dbset.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbset.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbset.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            if (context != null)
                context.Setup(c => c.Set<T>()).Returns(dbset.Object);
            return dbset;
        }
        //public static void MockFind<T>(Mock<DbSet<T>> dbset) where T : class
        //{
        //    dbset.Setup(a => a.Find(It.IsAny<object[]>())).Returns<object[]>(i => { return dbset.Object.FirstOrDefault(c => c.Id == (int)i[0]); });

        //}
        public static void MockInclude<T>(Mock<DbSet<T>> dbset) where T : class
        {
            dbset.Setup(d => d.Include(It.IsAny<string>())).Returns(dbset.Object);
        }
    }
}
