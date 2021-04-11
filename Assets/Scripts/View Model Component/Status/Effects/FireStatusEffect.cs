using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStatusEffect : StatusEffect
{
	Unit owner;

    void OnEnable()
    {
    	owner = GetComponentInParent<Unit>();
    	if(owner)
    		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    }

    void OnDisable ()
	{
		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
	}

    void OnNewTurn(object sender, object args)
    {
    	Stats s = GetComponentInParent<Stats>();
		int currentHP = s[StatTypes.HP];
		int reduce = 10;
		s.SetValue(StatTypes.HP, (currentHP - reduce), false);
    }
}
