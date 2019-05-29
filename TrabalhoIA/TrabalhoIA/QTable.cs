using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class QTable
    {
        public List<QEntry> Entries { get; set; }

        public QTable(QQuadrant[,] map)
        {
            Entries = MapToTable(map);
        }

        public List<QEntry> MapToTable(QQuadrant[,] map)
        {
            var entries = new List<QEntry>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i > 0) entries.Add(new QEntry(0, map[i, j], map[i - 1, j]));
                    if (i < map.GetLength(0) - 1) entries.Add(new QEntry(0, map[i, j], map[i + 1, j]));
                    if (j > 0) entries.Add(new QEntry(0, map[i, j], map[i, j - 1]));
                    if (j < map.GetLength(1) - 1) entries.Add(new QEntry(0, map[i, j], map[i, j + 1]));
                }
            }
            return entries;
        }

        public void PrintTable()
        {
            Console.WriteLine();


            foreach (var entry in Entries)
            {
                Console.WriteLine($"From {entry.Current.Name} to {entry.Target.Name} reward {entry.Reward}");
            }
     
            Console.WriteLine();
        }


    }
}
