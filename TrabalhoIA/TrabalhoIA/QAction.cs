using System;

namespace TrabalhoIA
{
    class QAction
    {
        public double Reward { get;  set; }
        public QQuadrant Current { get; }
        public QQuadrant Target { get; }
        
        public QAction(double reward, QQuadrant current, QQuadrant target)
        {
            Reward = reward;
            Current = current;
            Target = target;
        }
    }
}
