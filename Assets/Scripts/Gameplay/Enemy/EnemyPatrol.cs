using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyPatrol : AbstractEnemy
    {
        public Transform[] patrolPath;
        public float waitTimeOnSpotSec = 1f;

        private int _spotId;

        private float _waitTimeOnSpotSec;

        private void Start()
        {
            ChoosePositionId();
            ResetWaitTime();
        }

        private void Update()
        {
            Patrol();
        }

        protected void Patrol()
        {
            TargetPosition = patrolPath[_spotId].position;

            if (IsOnPathSpot())
            {
                ChoosePositionId();
                Wait();
            }

            Move();
        }

        private bool IsOnPathSpot()
            => Vector2.Distance(transform.position, TargetPosition) < minTargetDistance;

        protected void ChoosePositionId()
        {
            _spotId = Random.Range(0, patrolPath.Length);
        }

        private void Wait()
        {
            if (IsTimeUp())
                ResetWaitTime();
            else
                _waitTimeOnSpotSec -= Time.deltaTime;
        }

        protected void ResetWaitTime()
            => _waitTimeOnSpotSec = waitTimeOnSpotSec;

        private bool IsTimeUp()
            => _waitTimeOnSpotSec <= 0;
    }
}