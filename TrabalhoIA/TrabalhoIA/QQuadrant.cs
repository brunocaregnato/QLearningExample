using System;

namespace TrabalhoIA
{
    class QQuadrant
    {
        public double Reward { get; set; }
        public string Name { get; }
        public int TotalPasses { get; set; }


        public QQuadrant(int reward, string name)
        {
            Reward = reward;
            Name = name;
            TotalPasses = 0;
        }
    }
}
