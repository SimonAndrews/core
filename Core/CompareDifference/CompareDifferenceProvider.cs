using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.ComparDifference.Interface;
using Core.CompareDifference.Model;

namespace Core.CompareDifference
{
    public class CompareDifferenceProvider : ICompareDifferenceProvider
    {
        public IEnumerable<Difference> Compare<T>(T baseObject, T changedObject)
        {
            var differences = new List<Difference>();
            var baseProperties = baseObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            var changedProperties = changedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

            foreach (var baseProperty in baseProperties)
            {
                foreach (var changedProperty in changedProperties)
                {
                    if (baseProperty.Name == changedProperty.Name && baseProperty.PropertyType == changedProperty.PropertyType)
                    {
                        if (!Equals(baseProperty.GetValue(baseObject, null), changedProperty.GetValue(changedObject, null)))
                        {
                            differences.Add(new Difference
                            {
                                Type = baseProperty.PropertyType,
                                PropertyName = baseProperty.Name,
                                OriginalValue = baseProperty.GetValue(baseObject, null),
                                NewValue = changedProperty.GetValue(changedObject, null)
                            });
                        }
                    }
                }
            }

            return differences;
        }
    }
}
