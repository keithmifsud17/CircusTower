using System.Collections.Generic;
using System.Linq;

namespace CircusTower
{
    public class CircusTowerFactory
    {
        private readonly IEnumerable<Person> people;

        public CircusTowerFactory(IEnumerable<Person> people)
        {
            // Sort people by their height
            this.people = people.OrderBy(p => p, new HeightComparer());
        }

        public CircusTower BuildCircusTower()
        {
            // Each person is considered for the top position
            var towerCache = people
                .Select((person, i) => new { person, i })
                .ToDictionary(entry => entry.i, entry => CircusTower.With(entry.person));

            // Initially, the longest person is the longest circus tower
            var longestTower = towerCache.Last().Value;

            // We do not need to go over the first set (i=0) since we already established the longest tower at this point
            for (int i = 1; i < people.Count(); i++)
            {
                var person = people.ElementAt(i);

                // Find the longest tower which this person can hold on top of them
                towerCache[i] = FindLongestTowerForIndex(towerCache, i, person);

                if (towerCache[i].TotalHeight > longestTower.TotalHeight)
                {
                    longestTower = towerCache[i];
                }
            }

            return longestTower;
        }

        private CircusTower FindLongestTowerForIndex(Dictionary<int, CircusTower> towerCache, int index, Person person)
        {
            var longestTower = CircusTower.Empty;

            for (int i = 0; i < index; i++)
            {
                var tower = towerCache[i];

                // If this person is bigger than the person at the tower's base, and this tower is higher than any tower we've looked at till now
                if (tower.Last() < person && tower.TotalHeight > longestTower.TotalHeight)
                {
                    longestTower = tower;
                }
            }

            longestTower.StackPerson(person);
            return longestTower;
        }
    }
}