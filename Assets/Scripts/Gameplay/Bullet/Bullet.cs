using System;
using UnityEngine;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public Rigidbody2D rb;

        private void Update()
        {
            rb.velocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            Destroy(gameObject);
            
        }
    }
}