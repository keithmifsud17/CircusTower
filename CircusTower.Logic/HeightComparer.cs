using System.Collections.Generic;

namespace CircusTower
{
    public class HeightComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Height.CompareTo(y.Height);
        }
    }
}