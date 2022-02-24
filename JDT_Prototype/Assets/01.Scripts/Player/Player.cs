using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override Rigidbody2D rigid { get; set; }
    public override BoxCollider2D collider { get; set; }
    public override Animator animator { get; set; }

    [SerializeField] private int hp;
    [SerializeField] private int maxHP;

    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        EventManager.AddEvent_Action("TAKEDAMAGE", TakeDamage);
        EventManager.AddEvent_Action("TAKEHEAL", TakeHeal);
        EventManager.AddEvent_Function("MAXHP", MaxHP);
    }

    protected override void Update()
    {
        
    }

    int MaxHP()
    {
        return maxHP;
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        ClampHP();
        EventManager.TriggerEvent_Action("HPPERSIMMON_UI", damage);
    }

    void TakeHeal(int heal)
    {
        hp += heal;
        ClampHP();
        EventManager.TriggerEvent_Action("HPINCREASE_UI", heal);
    }

    void ClampHP()
    {
        Mathf.Clamp(hp, 0, maxHP);
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("TAKEDAMAGE");
        EventManager.RemoveEvent("TAKEHEAL");
        EventManager.RemoveEvent("MAXHP");
    }
}
