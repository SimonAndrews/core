using Moq;
using System.Data.Entity;
using System.Linq;

namespace Core.UnitTestHelper
{
    public abstract class UnitTestHelper
    {
        internal Mock<DbSet<T>> SetupRepositoryData<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSet.Setup(m => m.Add(It.IsAny<T>()));
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.MockDbIdSetToPropertyCount);

            if (typeof(T).IsInterface)
            {
                mockSet.As<T>().CallBase = false;
            }

            return mockSet;
        }
    }
}
