using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    [System.Serializable]
    public class StatusCounter
    {
        public string statusEffectName;
        public Text statusEffectCounterTextField;
    }

    public List<StatusCounter> statusCounters;

    private Animator anim;
    private int currentHP; //used to test whether or not they got hurt or healed
    private int oldHP;

    Stats stats;
    Unit owner;

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

        if (owner == null)
        {
            owner = GetComponentInParent<Unit>();
            this.AddObserver(UpdateStatusCounter, StatusCondition.UpdatedNotification, owner);
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
        this.RemoveObserver(UpdateStatusCounter, StatusCondition.UpdatedNotification, owner);
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

        Text counterTextField = null;

        StatusEffect newEffect = args as StatusEffect;

        foreach(StatusCounter counter in statusCounters)
        {
            if (counter.statusEffectName.Equals(newEffect.name))
            {
                counterTextField = counter.statusEffectCounterTextField;
            }
        }

        StatusCondition newEffectCondition = newEffect.GetComponentInChildren<StatusCondition>();

        if(counterTextField && newEffectCondition)
        {
            counterTextField.text = newEffectCondition.text;
        }
        
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
