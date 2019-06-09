using System;

namespace TrabalhoIA
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = BuildMapMatrix();
            var table = new QTable(matrix);
            table.PrintTable();
            Console.ReadLine();

            int totalEpochs = table.Train();
            
            Console.WriteLine($"Numero de execuções para encontrar o melhor caminho: {totalEpochs}");
            Console.ReadLine();

            table.PrintTable();
            Console.ReadLine();

            table.PrintBestPath();
            Console.ReadLine();
        }

        private static QQuadrant[,] BuildMapMatrix()
        {
            var aux = new string[5] { "A", "B", "C", "D", "E" };
            var matrix = new QQuadrant[5, 10];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    matrix[i, j] = new QQuadrant(-1, aux[i] + j);
                }
            }

            matrix[3, 1].Reward = -100;
            matrix[3, 2].Reward = -100;
            matrix[3, 4].Reward = -100;
            matrix[3, 5].Reward = -100;
            matrix[3, 7].Reward = -100;
            matrix[1, 3].Reward = -100;
            matrix[1, 7].Reward = -100;

            for (int i = 0; i < 9; i++)
            {
                matrix[0, i].Reward = -100;
            }

            matrix[0, 9].Reward = 100;
            return matrix;
        }
    }
}