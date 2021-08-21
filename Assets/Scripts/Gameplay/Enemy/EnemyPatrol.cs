using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyPatrol : AbstractEnemy
    {
        private void Start()
        {
            ChoosePositionId();
            ResetWaitTime();
        }

        private void Update()
        {
            CalculateTargetPosition();
            Move();

            if (IsOnTargetPosition())
                ChoosePositionIdOrWait();
        }

        protected override void CalculateTargetPosition()
        {
            TargetPosition = path[PositionId].position;
        }

        private void ChoosePositionIdOrWait()
        {
            if (IsTimeUp())
                ChoosePositionId();

            Wait();
        }
    }
}