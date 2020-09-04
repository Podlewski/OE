using System;
using System.Collections.Generic;

using GeneticSharp.Domain.Chromosomes;

using static Task3.Constants;


namespace Task3
{
    public class ObjectiveFunction
    {
        private readonly string objectiveFunction;
        private readonly double firstFitnessPercentage;
        private readonly double secondFitnessPercentage;
        private readonly double pentaltyDistance;
        private readonly double maxDistance;

        public ObjectiveFunction(string code, double firstPercentage,
            double secondPercentage, double distance)
        {
            objectiveFunction = code.ToUpper();
            firstFitnessPercentage = firstPercentage / (firstPercentage + secondPercentage);
            secondFitnessPercentage = secondPercentage / (firstPercentage + secondPercentage);
            pentaltyDistance = distance;

            maxDistance = 200;
        }

        public double CountFitness(Gene[] genes)
        {
            List<PointResult> results = AssignClusters(genes);

            return objectiveFunction switch
            {
                "S" => GetSilhouetteCoefficient(results),
                "X" => throw new NotImplementedException(),
                "M" => GetMixedFitness(genes, results),
                _ => throw new NotImplementedException(),
            };
        }


        #region DistancesCounters

        public double EuclideanDistance(Point a, Point b)
        {
            return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
        }

        private List<PointResult> AssignClusters(Gene[] genes)
        {
            List<PointResult> results = new List<PointResult>();

            for (int i = 0; i < Dataset.Count; i++)
            {
                int bestCluster = -1;
                double bestDistance = maxDistance;
                int secondCluster = -1;
                double secondDistance = maxDistance;

                for (int j = 0; j < genes.Length; j++)
                {
                    double newDistance = EuclideanDistance(Dataset[i], Dataset[(int)genes[j].Value]);

                    if (bestDistance > newDistance)
                    {
                        secondDistance = bestDistance;
                        secondCluster = bestCluster;

                        bestDistance = newDistance;
                        bestCluster = j;
                    }
                    else if (secondDistance > newDistance)
                    {
                        secondDistance = newDistance;
                        secondCluster = j;
                    }
                }

                results.Add(new PointResult(bestCluster, bestDistance, secondCluster,
                    secondDistance, bestDistance > pentaltyDistance));
            }

            return results;
        }

        #endregion


        #region ClusteringMetrics

        private double GetSilhouetteCoefficient(List<PointResult> results)
        {
            double bestMeanDistance = 0;
            double secondMeanDistance = 0;

            for (int i = 0; i < results.Count; i++)
            {
                bestMeanDistance += results[i].BestDistance;
                secondMeanDistance += results[i].SecondDistance;
            }

            bestMeanDistance /= results.Count;
            secondMeanDistance /= results.Count;

            return (secondMeanDistance - bestMeanDistance)/Math.Max(bestMeanDistance, secondMeanDistance);
        }



        private double GetMixedFitness(Gene[] genes, List<PointResult> results)
        {
            //return firstFitnessPercentage * GetSilhouetteCoefficient(results) +
            //    secondFitnessPercentage * GetFitnessB(results);

            return firstFitnessPercentage * GetSilhouetteCoefficient(results);
        }

        #endregion


        #region PureDistancesCounting

        private double GetFitnessA(Gene[] genes, List<PointResult> results)
        {
            double totalAverageDistance = 0;

            for (int i = 0; i < genes.Length; i++)
            {
                double totalGeneDistance = 0;
                int foundAssigments = 0;

                for (int j = 0; j < results.Count; j++)
                {
                    if (results[j].BestAssignment == i)
                    {
                        foundAssigments++;
                        totalGeneDistance += results[j].Penalty ?
                            results[j].BestDistance * 2 : results[j].BestDistance;
                    }
                }

                totalAverageDistance = totalGeneDistance / foundAssigments;
            }

            return totalAverageDistance / genes.Length;
        }

        private double GetFitnessB(List<PointResult> results)
        {
            double totalDistance = 0;

            for (int i = 0; i < results.Count; i++)
                totalDistance += results[i].Penalty ?
                    results[i].BestDistance * 2 : results[i].BestDistance;

            return totalDistance / results.Count;
        }

        #endregion
    }
}
