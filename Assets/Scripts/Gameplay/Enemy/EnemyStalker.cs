using System;
using Pathfinding;

namespace Gameplay.Enemy
{
    public class EnemyStalker : AbstractEnemy
    {
        private void Start()
        {
            var destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
            destinationSetter.target = Player.transform;
        }
    }
}