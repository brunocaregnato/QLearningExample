using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class QAction
    {
        public double Reward { get;  set; }
        public QQuadrant Current { get;  set; }
        public QQuadrant Target { get;  set; }

        public QAction(int reward, QQuadrant current, QQuadrant target)
        {
            Reward = reward;
            Current = current;
            Target = target;
        }

    }
}
