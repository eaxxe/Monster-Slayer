using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform[] _patrolPoints;
    public Transform[] PatrolPoints
    {
        get { return _patrolPoints; }
        set { _patrolPoints = value; }
    }

    [SerializeField] private float _patrolSpeed = 2f;
    public float PatrolSpeed
    {
        get { return _patrolSpeed; }
        private set { _patrolSpeed = value; }
    }

    [SerializeField] private float _detectionRange = 6f;
    public float DetectionRange
    {
        get { return _detectionRange; }
        private set { _detectionRange = value; }
    }

    [SerializeField] private float _loseRange = 7f;
    public float LoseRange
    {
        get { return _loseRange; }
        private set { _loseRange = value; }
    }

    [SerializeField] private int _health = 100;
    public int Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    [SerializeField] private float _chaseSpeed = 4f;
    public float ChaseSpeed
    {
        get { return _chaseSpeed; }
        private set { _chaseSpeed = value; }
    }

    private Transform player;
    public Transform Player
    {
        get { return player; }
        private set { player = value; }
    }

    private IEnemyPatrol patrol;
    private IEnemyChase chase;
    private EnemyAttack attack;
    private IEnemyState currentState;

    void Start()
    {
        patrol = GetComponentInChildren<IEnemyPatrol>();
        if (patrol == null) Debug.LogError("Missing IEnemyPatrol component on Enemy");

        chase = GetComponentInChildren<IEnemyChase>();
        if (chase == null) Debug.LogError("Missing IEnemyChase component on Enemy");

        attack = GetComponentInChildren<EnemyAttack>();
        if (attack == null) Debug.LogError("Missing EnemyAttack component on Enemy");

        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (Player == null) Debug.LogError("Missing Player GameObject with tag 'Player'");

        patrol.SetPatrolSpeed(PatrolSpeed);
        SetState(new PatrolState());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
        else
        {
            Debug.LogError("Current state is null in Enemy");
        }
    }

    public void SetState(IEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("grrrrrrrrrrrrrrrr  " + Health);
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(transform.root.gameObject);
    }

    public void Flip()
    {
        Transform rootTransform = transform.root;
        Vector3 scaler = rootTransform.localScale;
        scaler.x *= -1;
        rootTransform.localScale = scaler;
    }

    public void CheckAndFlip(float directionX)
    {
        if ((transform.root.localScale.x > 0 && directionX < 0) || (transform.root.localScale.x < 0 && directionX > 0))
        {
            Flip();
        }
    }
}
