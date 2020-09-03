using System;

using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;


namespace Task3
{
    public class ClusteringMutation : IMutation
    {
        public bool IsOrdered => throw new NotImplementedException();

        public void Mutate(IChromosome chromosome, float probability)
        {
            //throw new NotImplementedException();
        }
    }
}
