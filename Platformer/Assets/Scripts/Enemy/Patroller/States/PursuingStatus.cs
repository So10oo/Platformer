using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PursuingStatus : BasePatrollingState
{
    public PursuingStatus(Patroller patroller, StateMachine<Patroller> stateMachine, PatrollerSettings patrollerSettings) : base(patroller, stateMachine, patrollerSettings)
    {
    }
}

