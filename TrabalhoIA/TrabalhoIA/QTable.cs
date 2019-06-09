using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabalhoIA
{
    class QTable
    {
        public List<QAction> Actions { get; set; }
        public QQuadrant[,] Map { get; set; }

        public QTable(QQuadrant[,] map)
        {
            Actions = MapToTable(map);
            Map = map;
        }

        public List<QAction> MapToTable(QQuadrant[,] map)
        {
            var entries = new List<QAction>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i > 0) entries.Add(new QAction(0, map[i, j], map[i - 1, j]));
                    if (i < map.GetLength(0) - 1) entries.Add(new QAction(0, map[i, j], map[i + 1, j]));
                    if (j > 0) entries.Add(new QAction(0, map[i, j], map[i, j - 1]));
                    if (j < map.GetLength(1) - 1) entries.Add(new QAction(0, map[i, j], map[i, j + 1]));
                }
            }

            return entries;
        }

        public double GetReward(QQuadrant quadrant)
        {
            var list = GetPossibleActions(quadrant);
            double actionReward = list.Max(a => a.Reward);

            return quadrant.Reward + (0.5 * actionReward);
        }

        public IList<QAction> GetPossibleActions(QQuadrant quadrant)
            => Actions.Where(a => a.Current.Equals(quadrant)).ToList();

        public QAction GetNextAction(QQuadrant quadrant, bool includeRandomization = true)
        {
            var possibleActions = GetPossibleActions(quadrant);
            
            if (new Random().Next(0, 100) > 70 && includeRandomization)
            {
                var randomAction = new Random().Next(0, 100) % possibleActions.Count;
                return possibleActions[randomAction];
            }
            else
            {
                var bestAction = possibleActions[0];
                foreach (var action in possibleActions)
                {
                    if (bestAction.Reward < action.Reward)
                        bestAction = action;
                }

                return bestAction;
            }
        }

        public void PrintTable()
        {
            Console.WriteLine("+---------------+-----------------------+----------------------------+");
            Console.WriteLine("| Q()\t\t| Recompensa\t\t| Teste de Qnts vezes entrou |");
            Console.WriteLine("+---------------+-----------------------+----------------------------+");

            foreach (var entry in Actions)
                Console.WriteLine($"| {entry.Current.Name}, {entry.GetActionName()}\t| {entry.Reward.ToString().PadRight(17,' ')}\t| {entry.Current.TotalPasses.ToString().PadRight(17,' ')}\t     |");
            
            Console.WriteLine("+---------------+-----------------------+----------------------------+");
        }

        public void PrintBestPath()
        {
            var quadrant = Map[0, 0];
            Console.WriteLine("\n------------------------------\nCaminho Ótimo\n------------------------------\n");
            while (true)
            {
                var action = GetNextAction(quadrant, false);
                Console.WriteLine($"{action.Current.Name}, {action.GetActionName()}; reward: {action.Reward}");
                quadrant = action.Target;

                if (Map[0, 9].Equals(quadrant))
                    break;
            }
        }

        public void TrainEpoch(int episodes = 100)
        {
            for (int i = 0; i < episodes; i++)
            {
                var quadrant = Map[0, 0];
                
                while (true)
                {
                    var nextAction = GetNextAction(quadrant);
                    nextAction.Target.TotalPasses++;

                    var reward = GetReward(nextAction.Target);
                    nextAction.Reward = reward;

                    quadrant = nextAction.Target;

                    if (Map[0, 9].Equals(quadrant)) break;                    
                }
            }
        }

        public int Train(int minEpochs = 100)
        {
            int count = 0, totalEpochs = 0;

            while (count < minEpochs)
            {
                totalEpochs++;
                var optimalPolicy = new QOptimalPolicyChecker(Actions);
                TrainEpoch();
                count = optimalPolicy.TableHasConverged(Actions) ? count + 1 : 0;
            }

            return totalEpochs;
        }
    }
}
