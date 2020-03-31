﻿using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;

namespace Zad1
{
    class Program
    {
        public static ICrossover ParserCrossover(int n)
        {
            return n switch
            {
                2 => new CutAndSpliceCrossover(),
                3 => new CycleCrossover(),
                7 => new ThreeParentCrossover(),
                _ => throw new ArgumentException(),
            };
        }

        public static IMutation ParseMutation(int n)
        {
            return n switch
            {
                1 => new FlipBitMutation(),
                2 => new ReverseSequenceMutation(),
                _ => throw new ArgumentException(),
            };
        }

        public static ISelection ParseSelection(int n)
        {
            return n switch
            {
                1 => new EliteSelection(),
                2 => new RouletteWheelSelection(),
                _ => throw new ArgumentException(),
            };
        }

        public static ITermination ParseTermination(int n, float x)
        {
            return n switch
            {
                2 => new GenerationNumberTermination((int)x),
                4 => new FitnessThresholdTermination(x),
                _ => throw new ArgumentException(),
            };
        }

        static void Main(string[] args)
        {
            int crossover, mutation, geneticAlgorithm, selection, termination, function;
            float terminationValue, lowerBound, upperBound;
            if (args.Length != 0)
            {
                geneticAlgorithm = int.Parse(args[0]);
                crossover = int.Parse(args[1]);
                mutation = int.Parse(args[2]);
                selection = int.Parse(args[3]);
                termination = int.Parse(args[4]);
                terminationValue = float.Parse(args[5]);
                function = int.Parse(args[6]);
                lowerBound = float.Parse(args[7]);
                upperBound = float.Parse(args[8]);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Choose genetic algorithm variant" +
                    "\n[1] Simple" +
                    "\n[2] Adaptive");
                geneticAlgorithm = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Choose crossover method:" +
                                    //"\n[1] Uniform" +
                                    "\n[2] Cut and Splice" +
                                    "\n[3] Cycle" +
                                    //"\n[4] One Point" +
                                    //"\n[5] Partially Mapped" +
                                    //"\n[6] Postition Based" +
                                    "\n[7] Three Parent");
                crossover = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Choose mutation method:" +
                        "\n[1] Flip Bit" +
                        "\n[2] Reverse Sequence"
                        //"\n[3] Twors" +
                        //"\n[4] Uniform"
                        );
                mutation = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Choose selection method" +
                    "\n[1] Elite" +
                    "\n[2] Roulette Wheel"
                    //"\n[3] Stochastic Universal Sampling" +
                    //"\n[4] Tournament"
                    );
                selection = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Choose termination method:" +
                    //"\n[1] Fitness Stagnation" +
                    "\n[2] Generation Number" +
                    //"\n[3] Time Evolving" +
                    "\n[4] Fitness Threshold");
                termination = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Enter value:");
                terminationValue = float.Parse(Console.ReadLine());

                Console.Clear();
                Console.WriteLine("Choose function:" +
                    "\n[1] sin(x1)*cos(x2)" +
                    "\n[2] sin(x1)*cos(x2) + x1 + x2" +
                    "\n[3] sin(x1)*cos(x2) + x1^2 + x2^2" +
                    "\n[4] x^2");
                function = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("\nEnter range:" +
                        "\nlower bound = ");
                lowerBound = float.Parse(Console.ReadLine());
                Console.Write("upper bound = ");
                upperBound = float.Parse(Console.ReadLine());

            }
        }
    }
}