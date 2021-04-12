using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStatusEffect : StatusEffect
{
	Color originalColor;

    void OnEnable()
    {
    	base.initialize();
    	Debug.Log("Fire Status Effect Enabled!");

    	//damage owner on each new turn
    	if(owner)
    	{
    		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    	}

    	//Show the status effect visually
    	if(mainMaterial != null){
			originalColor = mainMaterial.GetColor("_Color");
			mainMaterial.SetColor("_Color", Color.yellow);
		}

    }

    void OnDisable ()
	{
		Debug.Log("Fire Status Effect Disabled!");
		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
		if(mainMaterial != null)
			mainMaterial.SetColor("_Color", originalColor);
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

	    			if(effect is WaterStatusEffect){//fire evaporates water, but is also put out!
	    				Debug.Log("Fire status effect found Water! Removing both status effects!");
	    				condition.Remove();
	    				myCondition.Remove();
	    				return;
	    			}

	    		}
	    	}
	    }
	}

    void OnNewTurn(object sender, object args)
    {
    	Debug.Log("Fire Status Effect Tick!");
    	Stats s = GetComponentInParent<Stats>();
		int currentHP = s[StatTypes.HP];
		int reduce = 10;
		s.SetValue(StatTypes.HP, (currentHP - reduce), true);
    }
}
