namespace Gameplay.Enemy
{
    public class EnemyStalker : AbstractEnemy
    {
        private void Update()
        {
            StalkPlayer();
        }

        private void StalkPlayer()
        {
            TargetPosition = Player.transform.position;

            if (IsNextToPlayer())
                Move();
        }
    }
}