using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyStalkerPatrol : EnemyPatrol
    {
        private void Start()
        {
            ChoosePositionId();
            ResetWaitTime();
        }
        private void Update()
        {
            StalkPlayerOrPatrol();
        }

        private void StalkPlayerOrPatrol()
        {
            if (IsNextToPlayer())
                StalkPlayer();
            else
                Patrol();
            
        }

        private void StalkPlayer()
        {
            TargetPosition = player.transform.position;
            Move();
        }

        private void Patrol()
        {
            TargetPosition = path[PositionId].position;
            
            if (IsOnTargetPosition())
                ChoosePositionIdOrWait();
            
            Move();
        }
    }
}