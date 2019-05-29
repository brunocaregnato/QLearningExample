using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class QQuadrant
    {
        public int Reward { get;  set; }
        public string Name { get; set; }


        public QQuadrant(int reward, string name)
        {
            Reward = reward;
            Name = name;
        }



    }
}
