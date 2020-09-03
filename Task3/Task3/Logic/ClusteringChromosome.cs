using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

using static Task3.Constants;

namespace Task3
{
    public class ClusteringChromosome : IChromosome
    {
        private readonly int _minValue = 0;
        private readonly int _maxValue;
        private Gene[] _genes;

        public ClusteringChromosome(int population)
        {
            _maxValue = population - 1;

            int startingGenes = RandomizationProvider.Current.GetInt(MinClusters, MaxClusters);
            _genes = new Gene[startingGenes];

            for (int i = 0; i < _genes.Length; i++)
            {
                _genes[i] = new Gene(RandomizationProvider.Current.GetInt(_minValue, _maxValue));
            }
        }

        public double? Fitness { get; set; }

        public int Length { get => _genes.Length; }

        public IChromosome Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] IChromosome other)
        {
            throw new NotImplementedException();
        }

        public IChromosome CreateNew()
        {
            return new ClusteringChromosome(_maxValue);
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
                return new Gene(-1);
        }

        public Gene[] GetGenes()
        {
            return _genes;
        }

        public void ReplaceGene(int index, Gene gene)
        {
            if(Length > index)
                _genes[index] = gene;
        }

        public void ReplaceGenes(int startIndex, Gene[] genes)
        {
            throw new NotImplementedException();
        }

        public void Resize(int newLength)
        {
            throw new NotImplementedException();
        }
    }
}
