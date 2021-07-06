using System.Collections.Generic;

namespace ObjectsComparer.Interfaces
{
    public interface IComparisonResult
    {
        bool IsDifferent { get; }

        bool HasDifferentProperties { get; }

        IEnumerable<string> DifferentProperties { get; }
    }
}
