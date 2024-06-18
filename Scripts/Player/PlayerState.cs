using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : IState
{
    private readonly float rotationDamping = 1.0f;
    protected PlayerStateMachine stateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {        
    }

    public virtual void Exit()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {      
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool SearchNextTarget()
    {
        if (stateMachine.Target != null) return true;
        float minDist = float.MaxValue;

        if (stateMachine.Player.Targets.Count == 0)
        {
            stateMachine.Target = null;            
            return false;
        }

        foreach (var item in stateMachine.Player.Targets)
        {
            float dir = (item.transform.position - stateMachine.Player.transform.position).magnitude;
            if(dir < minDist)
            {
                stateMachine.Target = item.gameObject.GetComponent<Health>();               
            }
        }

        return true;
    }

    protected bool CheckStandStartPos()
    {
        Vector3 playerPos = stateMachine.Player.transform.position;
        playerPos.y = 0;
        if ((playerPos - stateMachine.Player.StartPos).magnitude == 0f)
            return true;
        return false;
    }
}