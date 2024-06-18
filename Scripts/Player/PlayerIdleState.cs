using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;      
        stateMachine.Player.transform.rotation = Quaternion.identity;        
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
        GameManager.Instance.ReadyToNextStage();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!GameManager.Instance.CheckNextStage())
        {            
            stateMachine.ChangeState(stateMachine.MoveState);
            return;
        }
    }
}