using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;

public class Roulette : SelectionBase
{
    public Roulette() : base(2)
    {
    }


    
  


    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();

        //YOUR CODE HERE
        double sumFitness = 0.0f;
        int i = 0;
        for(i = 0; i < population.Count; i++){
            sumFitness = sumFitness + population[i].Fitness;
        }

       
        for(i = 0; i < number; i++){
            var pointer = RandomizationProvider.Current.GetDouble();
            double partial = 0.0f;
            int index;
            for(index = 0; partial <= pointer; index++){
                partial += (population[index].Fitness / sumFitness);
            }
            parents.Add(population[index-1]);
        }
        return parents;
    }
}
