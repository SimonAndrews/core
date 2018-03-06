using System;

namespace Core.CompareDifference.Model
{
    public class Difference
    {
        public Type Type { get; set; }
        public string PropertyName { get; set; }
        public object OriginalValue { get; set; }
        public object NewValue { get; set; }
    }
}
