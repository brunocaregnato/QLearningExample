using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoIA
{
    class QGreatPolicy
    {
        public List<QAction> Actions { get; set; }

        public QGreatPolicy(List<QAction> actions)
        {
            Actions = actions;
        }

        public int VerifyGreatPolicy(List<QAction> actions, int cont)
        {
            bool encontrouDiferente = false;
            for (int i = 0; i < Actions.Count; i++)
            {
                if (!Actions[i].Reward.Equals(actions[i].Reward))
                {
                    encontrouDiferente = true;
                    break;
                }
            }

            if (encontrouDiferente) cont = 0;
            else cont++;
            
            return cont;
        }
    }
}
