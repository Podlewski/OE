namespace Task3
{
    public struct Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point (double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    public struct PointResult
    {
        public int BestAssignment { get; private set; }
        public double BestDistance { get; private set; }
        public int SecondAssignment { get; private set; }
        public double SecondDistance { get; private set; }
        public bool Penalty { get; private set; }

        public PointResult(int bestAssignment, double bestDistance,
            int secondAssignment, double secondDistance, bool penalty)
        {
            BestAssignment = bestAssignment;
            BestDistance = bestDistance;
            SecondAssignment = secondAssignment;
            SecondDistance = secondDistance;
            Penalty = penalty;
        }
    }
}