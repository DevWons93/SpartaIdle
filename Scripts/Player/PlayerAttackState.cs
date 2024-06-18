public class PlayerAttackState : PlayerState
{
    private bool alreadyAppliedDealing;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        normalizedTime %= 1f;
        if (stateMachine.Target.IsDie)
        {
            stateMachine.ChangeState(stateMachine.MoveState);
            return;
        }
        //
        if (normalizedTime < 0.9f)
        {
            if (!alreadyAppliedDealing)
            {
                //stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                //stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
                stateMachine.Target.TakeDamage(stateMachine.Player.Data.Damage);
            }

            if (alreadyAppliedDealing)
            {
                //stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            alreadyAppliedDealing = false;
        }
    }
}