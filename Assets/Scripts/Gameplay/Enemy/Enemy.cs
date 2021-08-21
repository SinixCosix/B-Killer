using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : Character
    {
        public Transform[] path;
        public float waitTime = 1f;

        private float _waitTime;
        private int _positionId;

        // Start is called before the first frame update
        private void Start()
        {
            ChoosePositionId();
            ResetWaitTime();
        }

        // Update is called once per frame
        private void Update()
        {
            CalculateTargetPosition();
            Move();

            if (IsOnTargetPosition())
                WaitOrResetTime();
        }

        protected override void CalculateTargetPosition()
        {
            TargetPosition = path[_positionId].position;
        }

        private bool IsOnTargetPosition()
            => Vector2.Distance(transform.position, TargetPosition) < 0.02f;

        private void WaitOrResetTime()
        {
            if (_waitTime <= 0)
            {
                ChoosePositionId();
                ResetWaitTime();
            }
            else
                _waitTime -= Time.deltaTime;
        }

        private void ChoosePositionId()
            => _positionId = Random.Range(0, path.Length);

        private void ResetWaitTime()
            => _waitTime = waitTime;
    }
}