using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public float MovementSpeed { get; private set; }    
    public float MovementSpeedModifier { get; set; } = 1.0f;
    public Health Target { get; set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
   

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        AttackState = new PlayerAttackState(this);  

        MovementSpeed = player.Data.RunSpeed; 
    }
}
