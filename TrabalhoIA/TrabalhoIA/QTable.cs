using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {
                if (a.Current.Equals(quadrant)) list.Add(a);

            }

            return list;
        }

        public QAction GetNextAction(QQuadrant quadrant, bool includeRandomization = true)
        {
            var list = GetPossibleActions(quadrant);
            
            //pega maior acao
            if (new Random().Next(0, 100) > 70 && includeRandomization)
            {
                int acaoAleatoria = new Random().Next(0, 100) % list.Count;

                return list[acaoAleatoria];
            }
            //acao aleatoria
            else
            {
                var maior = list[0];
                for (int i = 0; i < list.Count; i++)
                {
                    if (maior.Reward < list[i].Reward) maior = list[i];
                }

                return maior;

                //list = list.Where(e => e.Reward == list.Max(l => l.Reward)).ToList();
                //return list[new Random().Next(list.Count)];
            }
        }

        public void PrintTable()
        {
            Console.WriteLine();

            foreach (var entry in Actions)
            {
                Console.WriteLine($"From {entry.Current.Name} to {entry.Target.Name} reward {entry.Reward}");
            }
     
            Console.WriteLine();
        }

        public void PrintBestPath()
        {
            var quadrante = Map[0, 0];
            
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
            for (int i = 0; i < 10; i++)
            {
                var quadrante = Map[0, 0];
                
                while (true)
                {
                    var t = GetNextAction(quadrante);
                    var s = GetReward(t.Target);
                    t.Reward = s;
                    quadrante = t.Target;

                    if (Map[0, 9].Equals(quadrante))
                    {
                        //if (i % 50 == 0)
                        //{
                        //    Console.WriteLine("\nterminou\n");
                        //    PrintTable();
                        //    Console.ReadLine();
                        //}
                        break;
                    }
                }
            }

            PrintTable();
            Console.ReadLine();
        }
    }
}
