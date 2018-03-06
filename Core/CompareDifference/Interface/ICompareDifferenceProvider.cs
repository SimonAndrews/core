using System.Collections.Generic;
using Core.CompareDifference.Model;

namespace Core.ComparDifference.Interface
{
    public interface ICompareDifferenceProvider
    {
        IEnumerable<Difference> Compare<T>(T baseObject, T changedObject);
    }
}
