﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

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
        public const List<Point> Elements = null;
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
        [Option("-f", ValueName = "FUNCTION",
            Description = "REQUIRED. Select objective function: A, B oraz MIXED.")]
        [AllowedValues("A", "B", "MIXED", IgnoreCase = true)]
        public string Function { get; } = "normal";

        [Option("-r", Description = "Use Roulette Wheel Selection instead of Elite Selection .")]
        public bool IsRoulette { get; } = false;

        [Option("-t", Description = "Use Three Parent Crossover instead of Uniform Crossover.")]
        public bool IsThreeParent { get; } = false;

        [Option("-u", ValueName = "CHANCE", Description = "Chance to uniform crossover" +
            "(if '-tpc' is not specified), from 0,01 to 0,9, by default: 0,1.")]
        [Range(typeof(float), "0,01", "0,9")]
        public float UniformChance { get; } = 0.1f;

        [Option("-m", ValueName = "CHANCE",
            Description = "Chance to mutate, from 0,01 to 0,9, by default: 0,1.")]
        [Range(typeof(float), "0,01", "0,9")]
        public float MutationChance { get; } = 0.1f;


        public static int Main(string[] args) =>
            CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            IChromosome chromosome = new ClusteringChromosome(Population);
            IPopulation population = new Population(Population, Population, chromosome);
            IFitness fitness = new FuncFitness((c) =>
            {
                var values = c.GetGenes();
                return 0;
            });
            ISelection selection = IsRoulette ?
                (ISelection) new RouletteWheelSelection() : new EliteSelection();
            ICrossover crossover = IsThreeParent ?
                (ICrossover) new ThreeParentCrossover() : new UniformCrossover(UniformChance);
            IMutation mutation = new ClusteringMutation();

            var geneticAlgorithm = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new GenerationNumberTermination(Generations)
            };
            geneticAlgorithm.Start();

            var bestChromose = geneticAlgorithm.BestChromosome as ClusteringChromosome;
            Console.WriteLine("RESULT\n" +
                $"\nGenerations: {geneticAlgorithm.GenerationsNumber}" +
                $"\nPopulation: {Population}" +
                $"\nSelection: {(IsRoulette ? "Roulette Wheel" : "Elite")}" +
                $"\nCrossover: {(IsThreeParent ? "Three Parent" : "Uniform")}" +
                $"\n\nClusters: {bestChromose.Fitness.Value}" +
                $"\nFitness: {Math.Round(bestChromose.Fitness.Value, 3)}");
        }
    }
}