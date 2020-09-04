using System.Collections.Generic;  

using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;


namespace Task3
{
    public class ClusteringThreeParentCrossover : ThreeParentCrossover
    {
        public ClusteringThreeParentCrossover()
            :base()
        {
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var firstParentGenes = parents[0].GetGenes();
            var secondParentGenes = parents[1].GetGenes();

            var child = parents[2].Clone();

            for (int i = 0; i < parents[0].Length && i < parents[1].Length && i < parents[2].Length; i++)
            {
                if (firstParentGenes[i] == secondParentGenes[i])
                {
                    child.ReplaceGene(i, firstParentGenes[i]);
                }
            }

            return new List<IChromosome>() { child };
        }
    }
}
