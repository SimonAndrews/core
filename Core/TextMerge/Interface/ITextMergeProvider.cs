using System.Collections.Generic;

namespace Core.TextMerge.Interface
{
    public interface ITextMergeProvider
    {
        string MergeText(IDictionary<string, string> mergeFields, KeyValuePair<char, char> mergeFieldWrappers, string content);
    }
}