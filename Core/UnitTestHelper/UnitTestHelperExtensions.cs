using System.Linq;

namespace Core.UnitTestHelper
{
    public static class UnitTestHelperExtensions
    {
        /// <summary>
        /// Mock out the Id Field of an object to be the PropertyCount for consistant testing
        /// </summary>
        public static void MockDbIdSetToPropertyCount<T>(this IQueryable<T> list, T data)
        {
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name.Contains("Id") && property.PropertyType.UnderlyingSystemType == typeof(int))
                {
                    property.SetValue(data, properties.Count());
                }
            }
        }
    }

}
