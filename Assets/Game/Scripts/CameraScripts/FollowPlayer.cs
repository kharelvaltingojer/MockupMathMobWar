using UnityEngine;

namespace Game.Scripts.CameraScripts
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform _playerTransform;
        public Vector3 objetOffset; 
        public GameObject offsetReference; 
        public Vector3 randomizeFromOffset;
        public Vector3 randomizeToOffset;
        public bool delayedFollow = true;
    
        private Vector3 _offset;
        [Range(1, 10)] [SerializeField]
        private int delayedFactor = 4;

        private void Awake()
        {
            FindPlayer();
        
            _offset = objetOffset;
        
            if (randomizeFromOffset.x != 0)
            {
                _offset.x = UnityEngine.Random.Range(randomizeFromOffset.x, randomizeToOffset.x);
            }
            if (randomizeFromOffset.y != 0)
            {
                _offset.y = UnityEngine.Random.Range(randomizeFromOffset.y, randomizeToOffset.y);
            }
            if (randomizeFromOffset.z != 0)
            {
                _offset.z = UnityEngine.Random.Range(randomizeFromOffset.z, randomizeToOffset.z);
            }
        }
    
        private void FindPlayer()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            if (offsetReference != null)
            {
                objetOffset = offsetReference.transform.position;
            }
        }
    
        private void LateUpdate()
        {
            if (_playerTransform == null)
            {
                FindPlayer();
            }
            else
            {
                var nextPosition = _playerTransform.position + _offset;
            
                if (delayedFollow)
                {
                    transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * delayedFactor);
                }
                else
                {
                    transform.position = nextPosition;
                }
            
            }
        
        
        }
    }
}
