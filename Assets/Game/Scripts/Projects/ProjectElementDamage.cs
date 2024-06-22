using Game.Scripts.Enums;

namespace Game.Scripts.Projects
{
    public class ProjectElementDamage {
        private float damage;
        public ProjectElementDamage(ProjectElement element)
        {
            switch (element)
            {
                case ProjectElement.Fire:
                    damage = 0.2f;
                    break;
                case ProjectElement.Ice:
                    damage = 0.1f;
                    break;
                case ProjectElement.Stone:
                    damage = 0.3f;
                    break;
            }
        }
        public float Damage() => damage;
    }
}