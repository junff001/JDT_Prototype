using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using MonsterLove.StateMachine;

public abstract class Enemy : MonoBehaviour
{
    public enum States {Chase, Attack }

    [Header("패스파인더")]
    protected Transform target;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float minDist = 3f;
    public float nextWayPointDist = 3f;

    [Header("A*")]
    public bool aStarfollowEnabled = true;

    [Header("스텟")] // 나중에 빼내지 않을까?
    protected float hp;
    public float attackDist;
    public float chaseDist = 0;

    public float enemySpeed;
    protected int currentWaypoint = 0;
    public LayerMask whatIsObstacle;
    protected Animator anim;
    protected Rigidbody2D rb;
    public Path path;
    protected Seeker seeker;

    private bool canMove = false; // 시작할 때 true로

    [SerializeField] private bool isMeleeAtacker = true;
    private StateMachine<States, StateDriverUnity> fsm;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("player")?.transform;
        fsm = new StateMachine<States, StateDriverUnity>(this);
        StateEnter(States.Chase);

    }

    protected virtual void Start()
    {
        if (target)
        {
            InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        }
    }

    private void FixedUpdate()
    {
        if (TargetInChaseDistance() && aStarfollowEnabled)
        {
            PathFollow();
        }
    }

    protected virtual void Update()
    {
        if(canMove) Move();
    }

    void StateEnter(States state)
    {
        fsm.ChangeState(state);
        switch (state)
        {
            case States.Chase:
                StartCoroutine(Chase_Update());
                break;

            case States.Attack:
                StartCoroutine(Attack_Update());
                break;
        }
    }

    IEnumerator Chase_Update()
    {
        canMove = true;

        while (!TargetInAttackDistance())
        {
            Debug.Log("추격");
            yield return new WaitForSeconds(0.5f);
        }
        StateEnter(States.Attack);
    }

    IEnumerator Attack_Update()
    {
        canMove = false;

        while (TargetInAttackDistance())
        {
            Attack();
            Debug.Log("공격");
            yield return new WaitForSeconds(0.5f);
        }
        StateEnter(States.Chase);
    }

    public abstract void Attack();

    private void SightCheck()
    {
        if (IsCanSeeTarget())
        {
            aStarfollowEnabled = false;
        }
        else
        {
            if (!aStarfollowEnabled)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            aStarfollowEnabled = true;
        }
    }

    private void Move()
    {
        SightCheck();

        if (isMeleeAtacker)
        {
            if (!aStarfollowEnabled)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            }
        }
        else
        {
            if (!aStarfollowEnabled && !TargetInAttackDistance())
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
            }
        }
    }

    private void UpdatePath()
    {
        if (aStarfollowEnabled && TargetInChaseDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        if (minDist * minDist > ((Vector2)path.vectorPath[currentWaypoint] - rb.position).sqrMagnitude)
        {
            currentWaypoint++;
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * enemySpeed;

        transform.Translate(force * Time.fixedDeltaTime);

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWayPointDist)
        {
            currentWaypoint++;
        }
    }

    private bool TargetInChaseDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < chaseDist;
    }

    private bool TargetInAttackDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < attackDist;
    }

    protected bool IsCanSeeTarget()
    {
        return true;
      
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected WaitForSeconds Wait(string animName)
    {
        return new WaitForSeconds(Utill.GetAnimLength(anim, "animName"));
    }
}
