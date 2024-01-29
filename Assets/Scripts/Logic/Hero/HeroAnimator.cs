using UnityEngine;

namespace Logic.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private Animator _animator;

        private readonly int _idleStateHash = Animator.StringToHash("IdleState");
        private readonly int _walkStateHash = Animator.StringToHash("WalkState");
        private readonly int _aimStateHash = Animator.StringToHash("AimState");
        private readonly int _shootHash = Animator.StringToHash("Shoot");

        public AnimatorState State { get; private set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        private void Start() =>
            PlayIdle();

        public void PlayIdle() =>
            _animator.Play(_idleStateHash);

        public void PlayWalk() =>
            _animator.Play(_walkStateHash);

        public void PlayAim() =>
            _animator.Play(_aimStateHash);

        public void PlayShoot() =>
            _animator.SetTrigger(_shootHash);

        public void EnteredState(int stateHash) =>
            State = StateFor(stateHash);

        public void ExitedState(int stateHash)
        {
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _walkStateHash)
                state = AnimatorState.Walking;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}