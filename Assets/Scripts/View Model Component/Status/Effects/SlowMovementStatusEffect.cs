using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovementStatusEffect : StatusEffect
{
	bool statsChanged = false;
    int amountSlowed = 0;

    void OnEnable()
    { 
    	base.initialize();
    	Debug.Log("[SlowMovementStatusEffect] Enabled!");

    	//Reduce unit's movement (They're water logged!)
    	if(stats)
    	{
            amountSlowed = stats[StatTypes.MOV]/2;
    		stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]/2, false);
    		statsChanged = true;
    	}

    	//Show the status effect visually

    }

    void OnDisable ()
	{	
		Debug.Log("[SlowMovementStatusEffect] Disabled!");
		//reset unit's previous state
		if(statsChanged)
			stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]+amountSlowed, false);
	}
}
