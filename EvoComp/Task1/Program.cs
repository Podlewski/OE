using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using Task1;

namespace Zad1
{
    class Program
    {
        const int NUMBER_OF_BITS = 2 * 8 * sizeof(float);

        public static ICrossover ParserCrossover(int n)
        {
            return n switch
            {
                1 => new UniformCrossover(),
                2 => new ThreeParentCrossover(),
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
                1 => new GenerationNumberTermination((int)x),
                2 => new FitnessThresholdTermination(x),
                _ => throw new ArgumentException(),
            };
        }

        static void Main(string[] args)
        {
            // Args

            int crossover, mutation, geneticAlgorithmChoice, selection, termination, function;
            float terminationValue, minValue, maxValue;
            if (args.Length != 0)
            {
                geneticAlgorithmChoice = int.Parse(args[0]);
                crossover = int.Parse(args[1]);
                mutation = int.Parse(args[2]);
                selection = int.Parse(args[3]);
                termination = int.Parse(args[4]);
                terminationValue = float.Parse(args[5]);
                function = int.Parse(args[6]);
                minValue = float.Parse(args[7]);
                maxValue = float.Parse(args[8]);
            }
            else
            {
                //todo: implement genetic algorithm variant
                Console.Clear();
                Console.Write("Choose genetic algorithm variant:" +
                                "\n[1] Simple" +
                                "\n[2] Adaptive" +
                                "\n\nChoise: ");
                geneticAlgorithmChoice = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("Choose crossover method:" +
                                "\n[1] Uniform" +
                                "\n[2] Three Parent" +
                                "\n\nChoise: ");
                crossover = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("Choose mutation method:" +
                                "\n[1] Flip Bit" +
                                "\n[2] Reverse Sequence" +
                                "\n\nChoise: ");
                mutation = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("Choose selection method" +
                                "\n[1] Elite" +
                                "\n[2] Roulette Wheel" +
                                "\n\nChoise: ");
                selection = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("Choose termination method:" +
                                "\n[1] Generation Number" +
                                "\n[2] Fitness Threshold" +
                                "\n\nChoise: ");
                termination = int.Parse(Console.ReadLine());

                Console.Clear();
                if(termination == 2)
                {
                    Console.Write("Enter Generation Number: ");
                    terminationValue = float.Parse(Console.ReadLine());
                }
                else
                {
                    Console.Write("Enter Fitness Threshold: ");
                    terminationValue = float.Parse(Console.ReadLine());
                }                

                Console.Clear();
                Console.Write("Choose function:" +
                                "\n[1] sin(x1)*cos(x2)" +
                                "\n[2] sin(x1)*cos(x2) + x1 + x2" +
                                "\n[3] sin(x1)*cos(x2) + x1^2 + x2^2" +
                                "\n[4] x1^x2" +
                                "\n\nChoise: ");
                function = int.Parse(Console.ReadLine());

                Console.Clear();
                Console.Write("Enter range" +
                                "\nMin value: ");
                minValue = float.Parse(Console.ReadLine());
                Console.Write("Max value: ");
                maxValue = float.Parse(Console.ReadLine());
            }

            //Prepare

            double[] minValues = { minValue, minValue };
            double[] maxValues = { maxValue, maxValue };
            int[] totalBits = { NUMBER_OF_BITS, NUMBER_OF_BITS };
            int[] fractionDigits = { 0, 0 };

            var chromosome = new FloatingPointChromosome(minValues, maxValues, totalBits, fractionDigits);

            var population = new Population(50, 100, chromosome);

            var fitness = new FuncFitness((c) =>
            {
                var values = (c as FloatingPointChromosome).ToFloatingPoints();
                var x1 = values[0];
                var x2 = values[1];

                return FunctionsToOptimize.Choose(function, x1, x2);
            });

            var geneticAlgorithm = new GeneticAlgorithm(population, fitness, ParseSelection(selection), ParserCrossover(crossover), ParseMutation(mutation))
            {
                Termination = ParseTermination(termination, terminationValue)
            };

            geneticAlgorithm.Start();

            var finalPhenotype = (geneticAlgorithm.BestChromosome as FloatingPointChromosome).ToFloatingPoints();
            var finalFitness = (geneticAlgorithm.BestChromosome as FloatingPointChromosome).Fitness.Value;

            Console.Clear();
            Console.WriteLine("RESULT" +
                "\n\nGenerations: " + geneticAlgorithm.GenerationsNumber +
                "\nRange:       " + minValue + ", " + maxValue +
                "\n\nx1:      " + finalPhenotype[0] +
                "\nx2:      " + finalPhenotype[1] +
                "\nFitness: " + finalFitness
                );

            Console.ReadKey();
        }
    }
}
