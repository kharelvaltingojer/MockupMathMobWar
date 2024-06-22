using UnityEngine;

namespace Game.Scripts
{
    public class InfinitySpin: MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private void Spin()
        {
            gameObject.transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    
        private void Update()
        {
            Spin();
        }
    
    }
}
