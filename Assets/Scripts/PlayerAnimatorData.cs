using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Animations
    {
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int RunHash = Animator.StringToHash("Run");
        public static readonly int WallSlideHash = Animator.StringToHash("Wall-Slide");
        public static readonly int JumpHash = Animator.StringToHash("Jump");
        public static readonly int JumpToFallHash = Animator.StringToHash("JumptoFall");
        public static readonly int FallHash = Animator.StringToHash("Fall");
    }
}
