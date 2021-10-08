using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics
{
    public class LevelGenerator : MonoBehaviour
    {
        public Transform startingPosition;
        public GameObject[] rooms;
        public float moveAmount;

        public float spawnRoomTime = 0.25f;
        private float _spawnRoomTime;
        
        private int _direction;

        private void Start()
        {
            Instantiate(rooms[0], transform.position, Quaternion.identity);
            _direction = Random.Range(1, 6);
        }

        private void Update()
        {
            if (_spawnRoomTime <= 0)
            {
                Move();
                _spawnRoomTime = spawnRoomTime;
            }
            else
                _spawnRoomTime -= Time.deltaTime;
        }

        private void Move()
        {
            var position = transform.position;
            
            switch (_direction)
            {
                case 1:
                case 2:
                    transform.position = new Vector2(position.x + moveAmount, position.y);
                    break;
                case 3:
                case 4:
                    transform.position = new Vector2(position.x - moveAmount, position.y);
                    break;
                case 5:
                    transform.position = new Vector2(position.x , position.y- moveAmount);
                    break;
            }
            
            Instantiate(rooms[0], transform.position, Quaternion.identity);
            _direction = Random.Range(1, 6);
        }
    }
}