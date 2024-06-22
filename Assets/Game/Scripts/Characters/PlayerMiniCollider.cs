using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Characters
{
    public class PlayerMiniCollider : MonoBehaviour
    {
        private CharacterController _controller;

        private void OnEnable()
        {
            _controller = GetComponent<CharacterController>();
        }

        void OnCollisionEnter(Collision collision) {
            ManageCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            ManageCollision(collision);
        }

        void ManageCollision(Collision collision) {
            GameObject me = gameObject;
            GameObject other = collision.gameObject;
            bool didCollideWithMiniPlayer = collision.gameObject.CompareTag("MiniPlayer");
            bool didCollideWithWall = collision.gameObject.CompareTag("wall");
        
            if (didCollideWithWall) {
                PushAway(me, other);
                Debug.Log("Collided with wall");
            }
            else if (didCollideWithMiniPlayer) {
                PushAway(me, other);
                Debug.Log("Collided with mini player");
            }
        }
    
        void PushAway(GameObject me, GameObject other) {
            Vector3 direction = me.transform.position - other.transform.position;
            direction.y = 0;

            if (Mathf.Abs(direction.x) < 0.1f && Mathf.Abs(direction.z) < 0.1f) {
                direction.x = (Random.Range(0f,1f) > 0.5f ? 1 : -1) * 0.1f;
                direction.z = (Random.Range(0f,1f) > 0.5f ? 1 : -1) * 0.1f;
            }

            direction.Normalize();
            float velocity = 10.0f;
            Vector3 velocityVector = direction * velocity;
            _controller.Move(velocityVector * Time.deltaTime);
        }
    }
}
