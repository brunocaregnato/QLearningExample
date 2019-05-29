using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class QEntry
    {
        public int Reward { get; private set; }
        public QQuadrant Current { get; private set; }
        public QQuadrant Target { get; private set; }

        public QEntry(int reward, QQuadrant current, QQuadrant target)
        {
            Reward = reward;
            Current = current;
            Target = target;
        }

    }
}
