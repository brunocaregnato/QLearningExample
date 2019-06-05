using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class Program
    {
        static void Main(string[] args)
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
            
            PrintaMatriz(matrix);
            var table = new QTable(matrix);
            table.PrintTable();
            Console.ReadLine();


            var greatPolicy = new QGreatPolicy(table.Actions);
            int cont = 0, numberExecutions = 0;
            while (cont < 10)
            {
                numberExecutions++;
                table.Train();
                cont = greatPolicy.VerifyGreatPolicy(table.Actions, cont);
            }

            Console.WriteLine($"Numero de execucoes: {numberExecutions}");
            table.PrintBestPath();
            Console.ReadLine();
        }

        public static void PrintaMatriz(QQuadrant[,] matrix)
        {
            Console.WriteLine();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine($"Valor: {i} -");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($" \"{matrix[i, j].Name} reward {matrix[i , j].Reward}\"\n");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}