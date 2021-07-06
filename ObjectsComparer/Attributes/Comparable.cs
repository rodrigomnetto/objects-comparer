using System;
using IComparable = ObjectsComparer.Interfaces.IComparable;

namespace ObjectsComparer.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Comparable : Attribute, IComparable
    {
    }
}
