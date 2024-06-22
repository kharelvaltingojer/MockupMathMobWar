using System.Collections;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class PlayerMiniSpawner : MonoBehaviour
    {
        public GameObject playerMiniPrefab;
        public Transform spawnPoint;
        PlayerData _playerData;

        private void OnEnable()
        {
            _playerData = GetComponent<PlayerController>().playerData;
            _playerData.spawns = 0;
            KillAllPlayerMini();
        }

        private void OnDisable()
        {
            _playerData.spawns = 0;
            KillAllPlayerMini();
        }
        private IEnumerator SpawnPlayerMini(Vector3 position, Quaternion rotation) 
        {
            Instantiate(playerMiniPrefab, position, rotation, spawnPoint);
            yield return null;
        }
        private IEnumerator SpawnPlayerMinis(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var randomXVariant = Random.Range(-4f, 4f); 
                var randomZVariant = Random.Range(-5f, 5f);
                var randomPosition = new Vector3(randomXVariant, spawnPoint.position.y, spawnPoint.position.z + randomZVariant);
                LifeManager.AddCurrentLife(+1);
                yield return StartCoroutine(SpawnPlayerMini(randomPosition, Quaternion.identity));
            }
        }
        public void SpawnPlayerMini(int quantity)
        {
            StartCoroutine(SpawnPlayerMinis(quantity));
        }
        private IEnumerator KillPlayerMini()
        {
            if (spawnPoint.childCount > 0)
            {
                var firstChild = spawnPoint.GetChild(0);
                if (firstChild != null)
                {
                    Destroy(firstChild.gameObject);
                    LifeManager.AddCurrentLife(-1);
                    yield return null; // Wait for the next frame
                }
            }
        }
        
        private IEnumerator KillPlayerMinis(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                yield return StartCoroutine(KillPlayerMini());
            }
        }
        
        public void KillPlayersMini(int quantity)
        {
            StartCoroutine(KillPlayerMinis(quantity));
        }
        
        private void KillAllPlayerMini()
        {
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
