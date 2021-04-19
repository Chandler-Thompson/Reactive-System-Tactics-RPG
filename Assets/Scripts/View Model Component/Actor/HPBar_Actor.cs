using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar_Actor : MonoBehaviour
{
   /* public HealthBar hpBar;
    public GameObject HP_Canvas;


    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
        HP_Canvas = Resources.Load("UI/HP_Canvas", typeof(GameObject)) as GameObject;
        Instantiate(HP_Canvas, new Vector3(0,0,0), Quaternion.identity);
        hold.transform.parent = this.transform;
        //hpBar = hold.HealthBar as HealthBar;
        
        //Debug.Log(Resources.Load("UI/HP_Canvas", typeof(GameObject)) as HealthBar);
    }

    void OnEnable()
    {
        this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    void OnHPDidChange(object sender, object args)
    {
        hpBar.SetHealth(stats[StatTypes.HP]);
    }

    void OnMHPDidChange(object sender, object args)
    {
        hpBar.SetMaxHealth(stats[StatTypes.MHP]);
    }*/
}
