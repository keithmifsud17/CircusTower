using System.Collections;
using System.Collections.Generic;

namespace CircusTower
{
    public class CircusTower : IEnumerable<Person>
    {
        private readonly List<Person> people = new List<Person>();

        public int TotalHeight { get; private set; } = 0;

        public void StackPerson(Person person)
        {
            people.Add(person);
            TotalHeight += person.Height;
        }

        public IEnumerator<Person> GetEnumerator() => people.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static CircusTower Empty => new CircusTower();

        public static CircusTower With(Person person)
        {
            var tower = new CircusTower();
            tower.StackPerson(person);
            return tower;
        }
    }
}