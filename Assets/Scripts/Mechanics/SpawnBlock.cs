using UnityEngine;

namespace Mechanics
{
    public class SpawnBlock : MonoBehaviour
    {
        public GameObject[] blocks;
        // Start is called before the first frame update
        private void Start()
        {
            var id = Random.Range(0, blocks.Length);
            Instantiate(blocks[id], transform.position, Quaternion.identity);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
