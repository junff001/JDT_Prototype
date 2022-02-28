using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{
    public override Rigidbody2D rigid { get; set; }
    public override BoxCollider2D collider { get; set; }
    public override Animator animator { get; set; }

    [SerializeField] private int hp;
    [SerializeField] private int maxHP;

    event Action<int> OnTakeDamage;
    event Action<int> OnTakeHeal;
    event Func<int> OnMaxHP;

    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        OnTakeDamage = TakeDamage;
        OnTakeHeal = TakeHeal;
        OnMaxHP = MaxHP;

        EventManager.AddEvent("TAKEDAMAGE", OnTakeDamage);
        EventManager.AddEvent("TAKEHEAL", OnTakeHeal);
        EventManager.AddEvent("MAXHP", OnMaxHP);

        //EventManager2.AddEvent_Action("TAKEDAMAGE", TakeDamage);
        //EventManager2.AddEvent_Action("TAKEHEAL", TakeHeal);
        //EventManager2.AddEvent_Function("MAXHP", MaxHP);
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
        EventManager2.TriggerEvent_Action("HPPERSIMMON_UI", damage);
    }

    void TakeHeal(int heal)
    {
        hp += heal;
        ClampHP();
        EventManager2.TriggerEvent_Action("HPINCREASE_UI", heal);
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

        //EventManager2.RemoveEvent("TAKEDAMAGE");
        //EventManager2.RemoveEvent("TAKEHEAL");
        //EventManager2.RemoveEvent("MAXHP");
    }
}
