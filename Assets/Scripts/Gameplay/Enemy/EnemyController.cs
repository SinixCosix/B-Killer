using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public Transform[] pathSpots;
        public float speed = 5f;
        public float waitTime = 1f;

        private float _waitTime;
        private int _randomSpot;
        private Vector2 _targetSpot;
        
        // Start is called before the first frame update
        private void Start()
        {
            _waitTime = waitTime;
            _randomSpot = Random.Range(0, pathSpots.Length);
        }

        // Update is called once per frame
        private void Update()
        {
            _targetSpot = pathSpots[_randomSpot].position;
            transform.position = Vector2.MoveTowards(transform.position, _targetSpot, speed * Time.deltaTime);
            
            if (!(Vector2.Distance(transform.position, _targetSpot) < 0.02f)) 
                return;
            
            if (_waitTime <= 0)
            {
                _randomSpot = Random.Range(0, pathSpots.Length);
                _waitTime = waitTime;
            }
            else
                _waitTime -= Time.deltaTime;
        }
    }
}