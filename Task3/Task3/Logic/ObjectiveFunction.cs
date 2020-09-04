using System;
using System.Collections.Generic;

using GeneticSharp.Domain.Chromosomes;

using static Task3.Constants;


namespace Task3
{
    public class ObjectiveFunction
    {
        private readonly string objectiveFunction;
        private readonly double firstFunctionPart;
        private readonly double secondFunctionPart;
        private double FirstFitnessPercentage => firstFunctionPart / (firstFunctionPart + secondFunctionPart);
        private double SecondFitnessPercentage => secondFunctionPart / (firstFunctionPart + secondFunctionPart);

        private readonly double penaltyDistance;
        private readonly double maxDistance;

        public ObjectiveFunction(string code, double firstPart,
            double secondPart, double distance)
        {
            objectiveFunction = code.ToUpper();
            firstFunctionPart = firstPart;
            secondFunctionPart = secondPart;
            penaltyDistance = distance;
            maxDistance = 200;
        }

        public double CountFitness(Gene[] genes)
        {
            List<PointResult> results = AssignClusters(genes);

            return objectiveFunction switch
            {
                "S" => GetSilhouetteCoefficient(results),
                "P" => GetPenaltyCoefficient(genes, results),
                "M" => GetMixedFitness(genes, results),
                _ => throw new NotImplementedException(),
            };
        }

        public override string ToString()
        {
            return objectiveFunction switch
            {
                "S" => "Silhouette Coefficient",
                "P" => "Penalty Coefficient",
                "M" => $"Mixed: {firstFunctionPart}  {secondFunctionPart}",
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

                results.Add(new PointResult(bestCluster, bestDistance,
                    secondCluster, secondDistance));
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

        private double GetPenaltyCoefficient(Gene[] genes, List<PointResult> results)
        {
            double totalPoints = 0;

            for (int i = 0; i < genes.Length; i++)
            {
                double genePoints = results.Count;

                for (int j = 0; j < results.Count; j++)
                {
                    if (results[j].BestAssignment == (int)genes[i].Value &&
                        results[j].BestDistance > penaltyDistance)
                    {
                        genePoints -= 0.7;
                    }
                    else if (results[j].BestAssignment != (int)genes[i].Value &&
                             EuclideanDistance(Dataset[j], Dataset[(int)genes[i].Value]) < penaltyDistance)
                    {
                        genePoints -= 1.3;
                    }
                }

                totalPoints += (double) genePoints / results.Count;
            }

            return totalPoints / genes.Length;
        }

        private double GetMixedFitness(Gene[] genes, List<PointResult> results)
        {
            return FirstFitnessPercentage * GetSilhouetteCoefficient(results) +
                SecondFitnessPercentage * GetPenaltyCoefficient(genes, results);
        }

        #endregion
    }
}
