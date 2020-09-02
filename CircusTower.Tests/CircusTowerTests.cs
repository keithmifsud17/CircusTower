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
        public void TestIgnoreInvalidPaths()
        {
            var people = new List<Person>
            {
                new Person(50, 150),
                new Person(60, 160),
                new Person(70, 170),
                new Person(80, 140), // This person will not be considered since they are shorter and heavier
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // each person needs to be both lighter and shorter than the next, thus 1,7 wont qualify
            tower.Should().BeEquivalentTo(
                new Person(50, 150),
                new Person(60, 160),
                new Person(70, 170)
            );
        }

        [Fact]
        public void TestComplexScenario()
        {
            var people = new List<Person>
            {
                new Person(50, 150),
                new Person(50, 180),
                new Person(50, 190),

                new Person(60, 160),
                new Person(60, 170),

                new Person(70, 170),
                new Person(70, 180),

                new Person(80, 190),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // each person needs to be both lighter and shorter than the next, thus 1,7 wont qualify
            tower.Should().BeEquivalentTo(
                new Person(50, 150),
                new Person(60, 160),
                new Person(70, 170),
                new Person(80, 190)
            );
        }

        [Fact]
        public void TestComplexScenarioWithSmartDecisions()
        {
            var people = new List<Person>
            {
                new Person(50, 150),
                new Person(50, 180),
                new Person(50, 680), // Impossible height, but shows ability to select a short path with higher height

                new Person(60, 160),
                new Person(60, 170),

                new Person(70, 170),
                new Person(70, 180),

                new Person(80, 190),
            };

            var tower = new CircusTowerFactory(people).BuildCircusTower();

            // {1,13} is longer than the {1,1}, {2,2}, {3,3}, {4,4} tower
            tower.Should().BeEquivalentTo(
                new Person(50, 680)
            );
        }
    }
}