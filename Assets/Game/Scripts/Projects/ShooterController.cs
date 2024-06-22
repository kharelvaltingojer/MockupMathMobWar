using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Projects
{
    public class ShooterController : MonoBehaviour
    {
        public GameObject[] projects;
        private GameObject project;
        private ProjectTrigger _projectTrigger;

        private void OnEnable()
        {
            AssignProject();
        }

        private void AssignProject()
        {
            if (projects.Length == 0)
            {
                Debug.Log($"ShooterController has no projects assigned on {gameObject.name}");
                return;
            }
        
            int rdmProject = Random.Range(0, projects.Length);
            project = projects[rdmProject];
            _projectTrigger = project.GetComponent<ProjectTrigger>();
        }

        public void ShootTo(GameObject target)
        {
            if (project == null)
            {
                AssignProject();
            }
            GameObject newProject = Instantiate(project, transform.position, Quaternion.identity);
            newProject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * 10, ForceMode.Impulse);
            DestroyAfter(newProject);
        }
        
        IEnumerator DestroyAfter(GameObject obj)
        {
            ProjectLifetime projectLifetime = new ProjectLifetime(_projectTrigger.element);
            float seconds = projectLifetime.Lifetime;
            yield return new WaitForSeconds(seconds);
            if (obj == null)
            {
                yield break;
            }

            bool isColliderEnabled = obj.GetComponent<Collider>().enabled;

            if (isColliderEnabled)
            {
                Destroy(obj);
            }
        }
    }
}
