using UnityEngine;
using UnityEngine.AI;

public class Bot : Characters
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 targetPos;
    private IState _currentState;
    private Transform _myTransform;
    private int HairCount { get => SkinShopUI.Instance.HairData.GetItemCount(); }
    private int PantCount { get => SkinShopUI.Instance.PantData.GetItemCount(); }

    public float timeToRun;
    public float range = 30f;
    protected override void Start()
    {
        _myTransform = this.transform;
        this.OnInit();
        base.Start();
    }
    public void OnInit()
    {
        _currentWp = UnityEngine.Random.Range(0, WPCount);
        _currentHair = UnityEngine.Random.Range(0, HairCount);
        _currentPant = UnityEngine.Random.Range(0, PantCount);
        _myTransform.localScale = Vector3.one * (1f + 1f / (11 - (playerLevel / 4)));
        _currentState = new IdleState();
        this.SetState(_currentState);
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsState(GameState.Playing)) return;
        _currentState?.OnExecute(this);
    }
    public void MoveRandomly()
    {
        if (!canMove) return;
        Vector3 randomDirection = _myTransform.position + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            targetPos = hit.position;
            targetPos.y = _myTransform.position.y;
            agent.SetDestination(targetPos);
            _myTransform.rotation = Quaternion.LookRotation(targetPos);
            if (timeToRun != 0) return;
            timeToRun = Vector3.Distance(_myTransform.position, targetPos) / speed;
        }
    }
    public void StopMove()
    {
        agent.ResetPath();
    }
    public override void OnDeath()
    {
        base.OnDeath();
        SetState(new DeadState());
    }
    public void ReturnBotToPool()
    {
        ResetTarget();
        this.OnInit();
        this.gameObject.SetActive(false);
        BotManager.Instance.PushBotBack(this);
    }
    public void ResetTarget()
    {
        target = null;
        targetList.Clear();
    }
    public void SetState(IState state)
    {
        _currentState?.OnExit(this);
        _currentState = state;
        _currentState?.OnEnter(this);
    }
}
