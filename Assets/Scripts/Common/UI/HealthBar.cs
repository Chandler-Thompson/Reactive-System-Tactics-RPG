using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text sleepP_text;

    public Dictionary<string, Text> statusCounters;

    private Animator anim;
    private int currentHP; //used to test whether or not they got hurt or healed
    private int oldHP;

    Stats stats;
    Status status;

    void Update()
    {
        if (stats == null)
        {
            stats = GetComponentInParent<Stats>();

            this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
            this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);

            SetMaxHealth(stats[StatTypes.MHP]);
            SetHealth(stats[StatTypes.HP]);

            oldHP = stats[StatTypes.HP];
        }

        if (status == null)
        {
            status = GetComponentInParent<Status>();

            this.AddObserver(UpdateStatusCounter, Status.AddedNotification, status);
            this.AddObserver(UpdateStatusCounter, Status.RemovedNotification, status);
        }

    }

    void OnEnable()
    {
        stats = GetComponentInParent<Stats>();
        anim = transform.parent.GetComponentInChildren<Animator>();

    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
        this.RemoveObserver(UpdateStatusCounter, Status.AddedNotification, status);
        this.RemoveObserver(UpdateStatusCounter, Status.RemovedNotification, status);
    }

    void OnHPDidChange(object sender, object args)
    {
        currentHP = stats[StatTypes.HP];
        SetHealth(stats[StatTypes.HP]);

        if(anim != null)
        {
            if (currentHP < oldHP)
            {
                if (currentHP <= 0)
                {
                    oldHP = currentHP;
                    anim.SetTrigger("Death");
                }
                else
                {
                    oldHP = currentHP;
                    anim.SetTrigger("Hurt");
                }
                
            }
            else if (currentHP > oldHP)
            {
                oldHP = currentHP;
            }
        }
        
    }

    void OnMHPDidChange(object sender, object args)
    {
        SetMaxHealth(stats[StatTypes.MHP]);
    }

    void UpdateStatusCounter (object sender, object args)
    {
        StatusEffect newEffect = args as StatusEffect;
        Text counter = statusCounters[newEffect.name];

        StatusCondition newEffectCondition = newEffect.GetComponentInChildren<StatusCondition>();

        counter.text = newEffectCondition.text;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;    
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
