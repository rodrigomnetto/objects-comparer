using ObjectsComparer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ObjectsComparer
{
    public class ComparisonResult : IComparisonResult
    {
        public ComparisonResult(bool isDifferent, IEnumerable<string> differentProperties = null)
        {
            IsDifferent = isDifferent;
            DifferentProperties = differentProperties;
        }

        public bool IsDifferent { get; private set; }

        public bool HasDifferentProperties { get => DifferentProperties != null && DifferentProperties.Any(); }

        public IEnumerable<string> DifferentProperties { get; private set; }
    }
}
