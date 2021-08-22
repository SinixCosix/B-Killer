namespace Gameplay.Enemy
{
    public class EnemyPatrolStalker : EnemyPatrol
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
    }
}