using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (GameManager.Instance.CheckBattleEnd() && GameManager.Instance.CheckNextStage())
            stateMachine.MovementSpeedModifier = 0f;
        else stateMachine.MovementSpeedModifier = 1f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();
        bool isTarget = SearchNextTarget();
        if (IsInAttackRagne() && isTarget)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        if (isTarget)
        {
            Move();
        }
        else
        {
            MoveToStartPos();
        }

        if(GameManager.Instance.CheckBattleEnd() && CheckStandStartPos())
        {            
            stateMachine.ChangeState(stateMachine.IdleState);            
            return;
        }
        
    }

    private void Move()
    {   
        Vector3 movementDirection = GetMovementDirectionToTarget();
        Move(movementDirection);
        Rotate(movementDirection);
    }
    private void MoveToStartPos()
    {
        MoveToward();
        Rotate(stateMachine.Player.StartPos);
    }

    private Vector3 GetMovementDirectionToTarget()
    {        
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Player.transform.position);
        dir.y = 0; // 높이보정
        return dir;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move((direction.normalized * movementSpeed) * Time.deltaTime);
    }

    private void MoveToward()
    {
        float movementSpeed = GetMovementSpeed();
        Vector3 playerPos = stateMachine.Player.transform.position;
        playerPos.y = 0;
        stateMachine.Player.Controller.enabled = false;
        stateMachine.Player.transform.position = Vector3.MoveTowards(playerPos, stateMachine.Player.StartPos, movementSpeed * Time.deltaTime);
        stateMachine.Player.Controller.enabled = true;
        //stateMachine.Player.Controller.Move(((stateMachine.Player.StartPos - playerPos) * movementSpeed) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Vector3 playerPos = stateMachine.Player.transform.position;
            playerPos.y = 0;    
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            //playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationDamping * Time.deltaTime);
            stateMachine.Player.transform.rotation = targetRotation;
        }
    }

    protected bool IsInAttackRagne()
    {
        if (GameManager.Instance.CheckBattleEnd()) return false;
        Vector3 pos = (stateMachine.Target.transform.position - stateMachine.Player.transform.position);
        pos.y = 0;
        float playerDistanceSqr = pos.sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Player.Data.AttackRange * stateMachine.Player.Data.AttackRange;
    }
}