﻿using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Randomizations;

namespace Task3
{
    public class ClusteringUniformCrossover : UniformCrossover
    {
        public ClusteringUniformCrossover(float mixProbability)
            : base(mixProbability)
        {
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var firstParent = parents[0];
            var secondParent = parents[1];
            var firstChild = firstParent.Clone();
            var secondChild = secondParent.Clone();

            for (int i = 0; i < firstParent.Length && i < secondParent.Length; i++)
            {
                if (RandomizationProvider.Current.GetDouble() < MixProbability)
                {
                    if(!firstChild.GetGenes().Contains(secondParent.GetGene(i)))
                        firstChild.ReplaceGene(i, secondParent.GetGene(i));

                    if (!secondChild.GetGenes().Contains(firstParent.GetGene(i)))
                        secondChild.ReplaceGene(i, firstParent.GetGene(i));
                }
            }

            return new List<IChromosome> { firstChild, secondChild };
        }
    }
}
