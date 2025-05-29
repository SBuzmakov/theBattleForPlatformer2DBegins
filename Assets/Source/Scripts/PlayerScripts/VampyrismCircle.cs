using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class VampyrismCircle
    {
        private VampyrismCircle(CircleCollider2D vampyreCircle, float  vampyreSkillRadius)
        {
            vampyreCircle.radius = vampyreSkillRadius; 
        }
    }
}
