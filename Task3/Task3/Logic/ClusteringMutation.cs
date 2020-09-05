using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;
using System;

namespace Task3
{
    public class ClusteringMutation : IMutation
    {
        public bool IsOrdered => throw new NotImplementedException();

        public void Mutate(IChromosome chromosome, float probability)
        {
            if (RandomizationProvider.Current.GetDouble() <= probability)
            {
                var clusteringChromosome = chromosome as ClusteringChromosome;

                var length = chromosome.Length;
                if (length < 4)
                {
                    clusteringChromosome.Resize(length + 1);
                }
                else if (length > 6)
                {
                    clusteringChromosome.Resize(length - 1);
                }
            }
        }
    }
}
