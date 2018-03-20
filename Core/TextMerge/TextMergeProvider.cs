using Core.TextMerge.Interface;
using System;
using System.Collections.Generic;

namespace Core.TextMerge
{
    public class TextMergeProvider : ITextMergeProvider
    {
        public string MergeText(IDictionary<string, string> mergeFields, KeyValuePair<char, char> mergeFieldWrappers, string content)
        {
            var mergedContent = string.Copy(content);

            foreach(var mergeField in mergeFields)
            {
                var mergeIdentifier = $"{mergeFieldWrappers.Key}{mergeField.Key}{mergeFieldWrappers.Value}";

                var count = 0;
                while (mergedContent.IndexOf(mergeIdentifier, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    // TODO: find nice solution for case insensitive replace
                    mergedContent = mergedContent.Replace(mergeIdentifier, mergeField.Value);
                    count++;

                    // Recursion break just incase, currently caused by case issue
                    if (count > 5)
                    {
                        Console.WriteLine(mergeIdentifier);
                        break;
                    }
                }
            }

            return mergedContent;
        }
    }
}