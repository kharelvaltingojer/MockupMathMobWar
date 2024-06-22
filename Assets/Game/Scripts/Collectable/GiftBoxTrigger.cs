using Game.Scripts.Characters;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Collectable
{
    public class GiftBoxTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject giftBox;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMiniSpawner playerMiniSpawner = other.GetComponent<PlayerMiniSpawner>();
                PlayerData playerData = other.GetComponent<PlayerController>().playerData;
                int result = Constants.MinionsGiftNumber;
                playerMiniSpawner.SpawnPlayerMini(result);
                playerData.spawns += result;
                giftBox.SetActive(false);
            }
        }
    }
}
