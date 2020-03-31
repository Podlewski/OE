using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using System;

using System.Collections;
using System.Collections.Generic;

namespace Task1.Strategies
{
    public class OneFifthSuccess
    {
        public string[] FunctionValues { get; private set; }

        private List<double> fitnesses;
        private int k;

        public OneFifthSuccess(int checkedFitnesses = 10)
        {
            fitnesses = new List<double>();
            k = checkedFitnesses;
        }

        public void Handle(object sender, EventArgs e)
        {
            var geneticAlgorithm = sender as GeneticAlgorithm;
            var bestChromosome = geneticAlgorithm.BestChromosome as FloatingPointChromosome;
            var fitness = bestChromosome.Fitness.Value;

            fitnesses.Add(fitness);

            if (fitnesses.Count > k)
            {
                int successCounter = 0;

                for(int i = fitnesses.Count - 1; i > fitnesses.Count - 11; i--)
                {
                    if (fitnesses[i] > fitnesses[i - 1])
                        successCounter++;
                }

                if (successCounter > 0.2 * k)
                    geneticAlgorithm.MutationProbability += 1.22f;

                else
                    geneticAlgorithm.MutationProbability -= 0.82f;

            }
        }
    }
}
