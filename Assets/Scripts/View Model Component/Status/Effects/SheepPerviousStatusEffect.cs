using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPerviousStatusEffect : StatusEffect
{
	Color originalColor;
	int damageMultiplier = 10;

    void OnEnable()
    {
    	base.initialize();
    	Debug.Log("[SheepPerviousStatusEffect] Enabled!");

    	//damage owner when their health would change
    	if(owner)
    	{
    		this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
    	}

    	//Show the status effect visually

    }

    void OnDisable ()
	{
		
		Debug.Log("[SheepPerviousStatusEffect] Disabled!");
		this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);

		//Remove status effect visual
	}

	void OnHPWillChange (object sender, object args)
	{

		StackingStatusCondition stackingCondition = myCondition as StackingStatusCondition;
		int numStacks = stackingCondition.numStacks;

		int totalDamageMultiplier = numStacks * damageMultiplier;

		ValueChangeException vce = args as ValueChangeException;
		Debug.Log("[SheepPerviousStatusEffect] Multiplying damage by "+totalDamageMultiplier+"! ("+damageMultiplier+"x multiplier * "+numStacks+" stacks)");
		vce.AddModifier(new MultDeltaModifier(int.MaxValue, totalDamageMultiplier));
		myCondition.Remove();//can only be procced once
	}

	void Update(){
		base.Update();
	}

}
