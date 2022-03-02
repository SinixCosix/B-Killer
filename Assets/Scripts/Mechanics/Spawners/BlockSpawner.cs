using UnityEngine;

namespace Mechanics.Spawners
{
    public class BlockSpawner : MonoBehaviour
    {
        public GameObject[] blocks;
        private void Start()
        {
            var id = Random.Range(0, blocks.Length);
            Instantiate(blocks[id], transform.position, Quaternion.identity);
        }
    }
}
