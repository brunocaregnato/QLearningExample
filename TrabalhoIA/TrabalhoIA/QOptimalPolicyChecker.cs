using System.Collections.Generic;
using System.Linq;

namespace TrabalhoIA
{
    class QOptimalPolicyChecker
    {
        public List<QAction> OldActions { get; set; }

        public QOptimalPolicyChecker(List<QAction> oldActions)
        {
            OldActions = oldActions.Select(a => new QAction(a.Reward, a.Current, a.Target)).ToList();
        }

        public bool TableHasConverged(List<QAction> newActions)
        {
            for (int i = 0; i < OldActions.Count; i++)
            {
                if (!OldActions[i].Reward.Equals(newActions[i].Reward))
                    return false;
            }

            return true;
        }
    }
}
