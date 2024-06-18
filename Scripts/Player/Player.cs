using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public Health Health { get; private set; }
    public Vector3 StartPos { get; set; }
    public List<Enemy> Targets { get; set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();

        Data = Instantiate(Data);
        Targets = new List<Enemy>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {        
        stateMachine.ChangeState(stateMachine.MoveState);
    }

    private void Update()
    {        
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }    

    public void RemoveTarget(Health target)
    {
        Targets.Remove(target.GetComponent<Enemy>());
    }
}
