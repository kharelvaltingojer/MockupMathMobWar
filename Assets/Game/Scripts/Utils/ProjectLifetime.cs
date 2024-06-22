using Game.Scripts.Enums;
using Game.Scripts.Projects;

namespace Game.Scripts.Utils
{
    public class ProjectLifetime
    {
        public float Lifetime { get; private set; }
        public ProjectLifetime(ProjectElement element)
        {
            switch (element)
            {
                case ProjectElement.Fire:
                    Lifetime = 1f;
                    break;
                case ProjectElement.Ice:
                    Lifetime = 3f;
                    break;
                case ProjectElement.Stone:
                    Lifetime = 6f;
                    break;
                default:
                    Lifetime = 3f;
                    break;
            }
        }
    }
}
