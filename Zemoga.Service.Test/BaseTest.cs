using AutoFixture;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Test
{
    public abstract class BaseTest<T> where T : class
    {
        public Mock<DbSet<R>> CrateDbSet<R>(List<R> dataList) where R : class
        {
            var data = dataList.AsQueryable();
            var mockSet = new Mock<DbSet<R>>();
            mockSet.As<IQueryable<R>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<R>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<R>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<R>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

    }
}
