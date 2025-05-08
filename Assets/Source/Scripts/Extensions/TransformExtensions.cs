using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class TransformExtensions
    {
        private const float FlipAngle = 180f;

        public static void FlipByAxisY(this Transform transform)
        {
            transform.Rotate(0f, FlipAngle, 0f);
        }
    }
}