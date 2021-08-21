using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class AbstractEnemy : Character
    {
        public Transform[] path;
        public float waitTime = 1f;

        protected int PositionId;

        private float _waitTime;

        protected bool IsOnTargetPosition()
            => Vector2.Distance(transform.position, TargetPosition) < 0.02f;

        protected void ChoosePositionId()
            => PositionId = Random.Range(0, path.Length);

        protected void ResetWaitTime()
            => _waitTime = waitTime;

        protected void Wait()
        {
            if (IsTimeUp())
                ResetWaitTime();
            else
                _waitTime -= Time.deltaTime;
        }

        protected bool IsTimeUp()
            => _waitTime <= 0;
    }
}