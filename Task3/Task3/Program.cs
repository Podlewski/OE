using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using McMaster.Extensions.CommandLineUtils;


namespace Task3
{
    public static class Constants
    {
        public const int MinClusters = 2;
        public const int MaxClusters = 8;
        public static readonly List<Point> Dataset =
            FileReader.ReadCsv(@"..\..\..\..\DataPreparer\ProcessedMallCustomers.csv");
    }

    public class Program
    {
        [Required]
        [Option("-g", ValueName = "GENERATIONS", Description = "REQUIRED. Set number of generations.")]
        public int Generations { get; }

        [Required]
        [Option("-p", ValueName = "POPULATION", Description = "REQUIRED. Set size of population.")]
        public int Population { get; }

        [Required]
        [Option("-f", ValueName = "FUNCTION", Description = "REQUIRED. Select objective " +
            "function: S (Silhouette), X (WIP) oraz M (mixed | WIP).")]
        //[AllowedValues("S", "X", "M", IgnoreCase = true)]
        [AllowedValues("S", IgnoreCase = true)]
        public string Function { get; }

        [Option("-f1", ValueName = "PERCENTAGE", Description = "Percentage of fitnesses for " +
            "first function in mixed objectives functions (from 0,1 to 0,9 | by default: 0.5)")]
        [Range(typeof(float), "0,1", "0,9")]
        public float FirstFunctionPerctnage { get; } = 0.5f;

        [Option("-f2", ValueName = "PERCENTAGE", Description = "Percentage of fitnesses for " +
            "second function in mixed objectives functions (from 0,1 to 0,9 |by default: 0.5)")]
        [Range(typeof(float), "0,1", "0,9")]
        public double SecondFunctionPerctnage { get; } = 0.5f;

        [Option("-d", Description = "Specify distance after which objective function charges " +
            "fitness for given point with penalty (from 0,5 to 4,0 | by default: 1,5)")]
        [Range(typeof(float), "0,5", "4,0")]
        public double PenaltyDistance { get; } = 1.5;

        [Option("-r", Description = "Use Roulette Wheel Selection instead of Elite Selection .")]
        public bool IsRoulette { get; } = false;

        [Option("-t", Description = "Use Three Parent Crossover instead of Uniform Crossover.")]
        public bool IsThreeParent { get; } = false;

        [Option("-uc", ValueName = "CHANCE", Description = "Chance to uniform crossover" +
            "if '-tpc' is not specified (from 0,01 to 0,9 | by default: 0,1)")]
        [Range(typeof(float), "0,01", "0,9")]
        public float UniformChance { get; } = 0.1f;

        [Option("-mc", ValueName = "CHANCE",
            Description = "Chance to mutate (from 0,01 to 0,9 | by default: 0,1)")]
        [Range(typeof(float), "0,01", "0,9")]
        public float MutationChance { get; } = 0.1f;


        public static int Main(string[] args) =>
            CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            ObjectiveFunction function = new ObjectiveFunction(Function, FirstFunctionPerctnage,
                SecondFunctionPerctnage, PenaltyDistance);

            IChromosome chromosome = new ClusteringChromosome(Constants.Dataset.Count);
            IPopulation population = new Population(Population, Population, chromosome);
            IFitness fitness = new FuncFitness((c) =>
            {
                var values = c.GetGenes();
                var a = function.CountFitness(values);
                return a;
            });
            ISelection selection = IsRoulette ?
                (ISelection) new RouletteWheelSelection() : new EliteSelection();
            ICrossover crossover = IsThreeParent ?
                (ICrossover) new ClusteringThreeParentCrossover() :
                new ClusteringUniformCrossover(UniformChance);
            IMutation mutation = new ClusteringMutation();

            var geneticAlgorithm = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new GenerationNumberTermination(Generations)
            };
            geneticAlgorithm.Start();

            var bestChromose = geneticAlgorithm.BestChromosome as ClusteringChromosome;
            Console.WriteLine("RUN SETTINGS:" +
                $"\n  Generations: {geneticAlgorithm.GenerationsNumber}" +
                $"\n  Population:  {Population}" +
                $"\n  Selection:   {(IsRoulette ? "Roulette Wheel" : "Elite")}" +
                $"\n  Crossover:   {(IsThreeParent ? "Three Parent" : "Uniform")}");
            Console.WriteLine("\nRUN RESULTS:" +
                $"\n  Clusters: {bestChromose.Length}" +
                $"\n  Fitness:  {Math.Round(bestChromose.Fitness.Value, 3)}");
        }
    }
}
