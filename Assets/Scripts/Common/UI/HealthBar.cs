using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    Stats stats;



    void Update()
    {
        if (stats == null)
        {
            stats = GetComponentInParent<Stats>();
            this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
            this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
            SetMaxHealth(stats[StatTypes.MHP]);
            SetHealth(stats[StatTypes.HP]);
        }

        //Debug.Log(stats);
    }

    void OnEnable()
    {
        stats = GetComponentInParent<Stats>();

    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    void OnHPDidChange(object sender, object args)
    {
        Debug.Log("Got Hurt");
        Debug.Log(stats[StatTypes.HP]);
        SetHealth(stats[StatTypes.HP]);
    }

    void OnMHPDidChange(object sender, object args)
    {
        Debug.Log(stats);
        SetMaxHealth(stats[StatTypes.MHP]);
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
