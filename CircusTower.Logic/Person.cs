namespace CircusTower
{
    public struct Person
    {
        public Person(int weight, int height)
        {
            Weight = weight;
            Height = height;
        }

        public int Height { get; }

        public int Weight { get; }

        public bool SmallerThan(Person other) => Height < other.Height && Weight < other.Weight;

        public static bool operator <(Person a, Person b) => a.Height < b.Height && a.Weight < b.Weight;

        public static bool operator >(Person a, Person b) => a.Height > b.Height && a.Weight > b.Weight;
    }
}