using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace CircusTower.Tests
{
    public class CircusTowerTests
    {
        [Fact]
        public void TestProvidedSample()
        {
            var people = new List<Person>
            {
                new Person(64, 120),
                new Person(65, 100),
                new Person(70, 150),
                new Person(56, 90),
                new Person(75, 190),
                new Person(60, 95),
                new Person(68, 110),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            tower.Should().BeEquivalentTo(
                new Person(56, 90),
                new Person(60, 95),
                new Person(65, 100),
                new Person(68, 110),
                new Person(70, 150),
                new Person(75, 190)
            );
        }

        [Fact]
        public void TestComplexScenario()
        {
            var people = new List<Person>
            {
                new Person(1, 1),
                new Person(1, 7),
                new Person(1, 9),

                new Person(2, 2),
                new Person(2, 6),

                new Person(3, 3),
                new Person(3, 5),

                new Person(4, 4),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // each person needs to be both lighter and shorter than the next, thus 1,7 wont qualify
            tower.Should().BeEquivalentTo(
                new Person(1, 1),
                new Person(2, 2),
                new Person(3, 3),
                new Person(4, 4)
            );
        }

        [Fact]
        public void TestSimpleScenario()
        {
            var people = new List<Person>
            {
                new Person(1, 1),
                new Person(2, 2),
                new Person(3, 3),
                new Person(4, 1),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // each person needs to be both lighter and shorter than the next, thus 1,7 wont qualify
            tower.Should().BeEquivalentTo(
                new Person(1, 1),
                new Person(2, 2),
                new Person(3, 3)
            );
        }

        [Fact]
        public void TestComplexScenarioWithSmartDecisions()
        {
            var people = new List<Person>
            {
                new Person(1, 1),
                new Person(1, 7),
                new Person(1, 13),

                new Person(2, 2),
                new Person(2, 6),

                new Person(3, 3),
                new Person(3, 5),

                new Person(4, 4),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // {1,13} is longer than the {1,1}, {2,2}, {3,3}, {4,4} tower
            tower.Should().BeEquivalentTo(
                new Person(1, 13)
            );
        }
    }
}