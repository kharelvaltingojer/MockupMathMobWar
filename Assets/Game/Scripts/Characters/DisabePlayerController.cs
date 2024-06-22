using System.Collections;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class DisabePlayerController : MonoBehaviour
    {
        IEnumerator ClearPlayerControllers()
        {
            // get the player
            var player = GameObject.FindGameObjectWithTag("Player");
            // remove the script from the player
            Destroy(player.GetComponent<PlayerController>());
            // get all objects with PlayerController
            var playerControllers = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

            while (playerControllers.Length > 0)
            {
                foreach (var playerController in playerControllers)
                {
                    //remove the script from the object
                    Destroy(playerController);
                }
                // get all objects with PlayerController
                playerControllers = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
            }
            //wait for 1 second
            yield return new WaitForSeconds(1);
            // get all objects with PlayerController
            playerControllers = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
            // if there is no PlayerController, run it again
            if (playerControllers.Length > 0)
            {
                StartCoroutine(ClearPlayerControllers());
            }
        }

        IEnumerator RemovePlayerControllerFromPlayer()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player.GetComponent<PlayerController>());
            yield return null;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(RemovePlayerControllerFromPlayer());
            }
        }
    }
}
