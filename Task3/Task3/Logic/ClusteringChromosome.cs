using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

using static Task3.Constants;


namespace Task3
{
    public class ClusteringChromosome : IChromosome
    {
        private readonly int _minIndex = 0;
        private readonly int _maxIndex;
        private Gene[] _genes;

        public ClusteringChromosome(int maxIndex)
        {
            _maxIndex = maxIndex;

            int startingGenes = RandomizationProvider.Current.GetInt(MinClusters, MaxClusters + 1);
            _genes = new Gene[startingGenes];

            for (int i = 0; i < _genes.Length; i++)
            {
                Gene newGene;

                do
                {
                    newGene = new Gene(RandomizationProvider.Current.GetInt(_minIndex, _maxIndex));
                } while (_genes.Contains(newGene));

                _genes[i] = newGene;
            }
        }

        private ClusteringChromosome(int maxIndex, Gene[] genes)
        {
            _maxIndex = maxIndex;
            _genes = (Gene[])genes.Clone();
        }

        public double? Fitness { get; set; }

        public int Length { get => _genes.Length; }

        public IChromosome Clone()
        {
            return new ClusteringChromosome(_maxIndex, _genes);
        }

        public int CompareTo([AllowNull] IChromosome other)
        {
            throw new NotImplementedException();
        }

        public IChromosome CreateNew()
        {
            return new ClusteringChromosome(_maxIndex);
        }

        public Gene GenerateGene(int geneIndex)
        {
            throw new NotImplementedException();
        }

        public Gene GetGene(int index)
        {
            if (Length > index)
                return _genes[index];
            else
            {
                Debug.Write("Not able to get gene: out of index.");
                return new Gene(-1);
            }
        }

        public Gene[] GetGenes()
        {
            return _genes;
        }

        public void ReplaceGene(int index, Gene gene)
        {
            if (Length > index)
                _genes[index] = gene;
            else
                Debug.Write("Not able to replace gene: out of index.");
        }

        public void ReplaceGenes(int startIndex, Gene[] genes)
        {
            throw new NotImplementedException();
        }

        public void Resize(int newLength)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Length}: {string.Join(" ", _genes)}";
        }
    }
}
