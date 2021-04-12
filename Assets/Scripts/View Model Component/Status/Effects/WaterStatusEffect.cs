using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStatusEffect : StatusEffect
{
	Color originalColor;
	bool statsChanged = false;

    void OnEnable()
    { 
    	base.initialize();
    	Debug.Log("Water Status Effect Enabled!");

    	//Reduce unit's movement (They're water logged!)
    	if(stats)
    	{
    		stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]-2, false);
    		statsChanged = true;
    	}

    	//Show the status effect visually
		if(mainMaterial != null){
			originalColor = mainMaterial.GetColor("_Color");
			mainMaterial.SetColor("_Color", Color.blue);
		}

    }

    void Update()
    {
    	if(myCondition == null){
    		myCondition = this.GetComponentInChildren<StatusCondition>();
    	}else{
	    	//Interact with other status effects
	    	if(status){
	    		//get and loop through all statuses on owner
	    		StatusCondition[] conditions = status.GetComponentsInChildren<StatusCondition>();
	    		foreach(StatusCondition condition in conditions){

	    			StatusEffect effect = condition.GetComponentInParent<StatusEffect>();

	    			if(effect is FireStatusEffect){//water puts out fire, but also evaporates!
	    				Debug.Log("Water status effect found Fire! Removing both status effects!");
	    				condition.Remove();
	    				myCondition.Remove();
	    				return;
	    			}

	    		}
	    	}
	    }
    }

    void OnDisable ()
	{	
		Debug.Log("Water Status Effect Disabled!");
		//reset unit's previous state
		if(mainMaterial != null)
			mainMaterial.SetColor("_Color", originalColor);
		if(statsChanged)
			stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]+2, false);
	}

}
