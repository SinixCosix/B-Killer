using Pathfinding;

namespace Gameplay.Enemy
{
    public class EnemyStalker : AbstractEnemy
    {
        // public AIDestinationSetter Setter;
        
        private void Update()
        {
               
            var destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
            destinationSetter.target = Player.transform;
        }
    }
}