using UnityEngine;

public static class EnemyAnimationData
{
    public static class BasicEnemy
    {
        public static class Animations
        {
            public static readonly int IdleHash = Animator.StringToHash("Idle");
            public static readonly int WalkHash = Animator.StringToHash("Walk");
        }
    }
}
