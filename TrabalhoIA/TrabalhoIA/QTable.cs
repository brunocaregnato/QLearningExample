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

        public List<QAction> GetPossibleActions(QQuadrant quadrant)
        {
            var list = new List<QAction>();

            foreach (var a in Actions)
                if (a.Current.Equals(quadrant)) list.Add(a);

            return list;
        }

        public QAction GetNextAction(QQuadrant quadrant, bool includeRandomization = true)
        {
            var list = GetPossibleActions(quadrant);
            
            //acao aleatoria
            if (new Random().Next(0, 100) > 70 && includeRandomization)
            {
                int acaoAleatoria = new Random().Next(0, 100) % list.Count;

                return list[acaoAleatoria];
            }
            //maior acao
            else
            {
                var maior = list[0];
                for (int i = 0; i < list.Count; i++)
                {
                    if (maior.Reward < list[i].Reward) maior = list[i];
                }

                return maior;
            }
        }

        public void PrintTable()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("| Q()     | Recompensa       | Teste de Qnts vezes entrou");
            foreach (var entry in Actions)
                Console.WriteLine($"| {entry.Current.Name.Substring(0,1)} , " +
                    $"a{entry.Current.Name.Substring(1)}{entry.Target.Name.Substring(1)} | " +
                    $"{entry.Reward.ToString().PadRight(17,' ')}|" +
                    $"QntdVezesQueEntrou: {entry.Current.NumberOfTimesEntered}");
            
            Console.WriteLine("------------------------------");
        }

        public void PrintBestPath()
        {
            var quadrante = Map[0, 0];
            Console.WriteLine("\n------------------------------\nCaminho Ótimo\n------------------------------\n");
            while (true)
            {
                var action = GetNextAction(quadrante, false);
                Console.WriteLine($"{action.Current.Name} -> {action.Target.Name} r {action.Reward}");
                quadrante = action.Target;

                if (Map[0, 9].Equals(quadrante))
                    break;
            }
        }

        public void Train()
        {
            //Executa 100 vezes para encontrar o melhor caminho dessa historia
            for (int i = 0; i < 100; i++)
            {
                var quadrante = Map[0, 0];
                
                while (true)
                {
                    var nextAction = GetNextAction(quadrante);
                    nextAction.Target.NumberOfTimesEntered++;
                    var reward = GetReward(nextAction.Target);
                    nextAction.Reward = reward;
                    quadrante = nextAction.Target;

                    if (Map[0, 9].Equals(quadrante)) break;                    
                }
            }

            PrintTable();
        }
        
    }
}
