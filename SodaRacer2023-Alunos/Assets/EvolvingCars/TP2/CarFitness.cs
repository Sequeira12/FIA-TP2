using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Linq;

namespace GeneticSharp.Runner.UnityApp.Car
{
    public class CarFitness : IFitness
    {
        public CarFitness()
        {
            ChromosomesToBeginEvaluation = new BlockingCollection<CarChromosome>();
            ChromosomesToEndEvaluation = new BlockingCollection<CarChromosome>();
        }

        public BlockingCollection<CarChromosome> ChromosomesToBeginEvaluation { get; private set; }
        public BlockingCollection<CarChromosome> ChromosomesToEndEvaluation { get; private set; }
        public double Evaluate(IChromosome chromosome)
        {
            var c = chromosome as CarChromosome;
            ChromosomesToBeginEvaluation.Add(c);

            float fitness = 0; 
            do
            {
                Thread.Sleep(1000);

                /*YOUR CODE HERE: You should define de fitness function here!!
                 * 
                 * 
                 * You have access to the following information regarding how the car performed in the scenario:
                 * MaxDistance: Maximum distance reached by the car;
                 * MaxDistanceTime: Time taken to reach the MaxDistance;
                 * MaxVelocity: Maximum Velocity reached by the car;
                 * NumberOfWheels: Number of wheels that the cars has;
                 * CarMass: Weight of the car;
                 * IsRoadComplete: This variable has the value 1 if the car reaches the end of the road, 0 otherwise.
                 * 
                */
                float MaxDistance = c.MaxDistance;
                float MaxDistanceTime = c.MaxDistanceTime;
                float MaxVelocity = c.MaxVelocity;
                float NumberOfWheels = c.NumberOfWheels;
                float CarMass = c.CarMass;
                int IsRoadComplete = c.IsRoadComplete ? 1 : 0;

                // fitness = (float)(0.75 * MaxDistance + 0.25 * MaxDistanceTime);
                //fitness = (float)(6000 * (MaxDistance) + 20000 * (IsRoadComplete) + 20000 * (1 / CarMass) + 16000 * (MaxVelocity / NumberOfWheels));
                //fitness = MaxDistance / 200 + NumberOfWheels / 15 + MaxVelocity / 10 + IsRoadComplete * 10;
                //fitness = 1000 * IsRoadComplete + 40 * MaxDistance + 5 * (1 / CarMass) + 15 * (1 / NumberOfWheels) + 200 * MaxVelocity + 150 * MaxDistance / (MaxDistanceTime + 1);
                //fitness = 30 * MaxDistance + 100 * IsRoadComplete + 20 * MaxDistance / (MaxDistanceTime + 1);
                
                fitness =  (int) Math.Pow(MaxDistance,3) + 400000 * IsRoadComplete +(int) Math.Pow((MaxDistance / (MaxDistanceTime + 1)),2);
                c.Fitness = fitness;

            } while (!c.Evaluated);

            ChromosomesToEndEvaluation.Add(c);

            do
            {
                Thread.Sleep(1000);
            } while (!c.Evaluated);


            return fitness;
        }

    }
}