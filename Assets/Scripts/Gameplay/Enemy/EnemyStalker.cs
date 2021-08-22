using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyStalker : AbstractEnemy
    {
        private void Update()
        {
            CalculateTargetPosition();
            StalkPlayer();            
        }

        protected override void CalculateTargetPosition()
        {
            TargetPosition = path[0].position;
        }

        private void StalkPlayer()
        {
            if (IsOnTargetPosition())
                Move();
        }
    }
}