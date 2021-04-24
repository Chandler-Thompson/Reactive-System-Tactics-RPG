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
    	if(mainMaterial != null){
			originalColor = mainMaterial.GetColor("_BaseColor");
			mainMaterial.SetColor("_BaseColor", Color.yellow);
		}

    }

    void OnDisable ()
	{
		Debug.Log("[SheepPerviousStatusEffect] Disabled!");
		this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
		if(mainMaterial != null)
			mainMaterial.SetColor("_BaseColor", originalColor);
	}

	void OnHPWillChange (object sender, object args)
	{
		ValueChangeException vce = args as ValueChangeException;
		vce.AddModifier(new MultDeltaModifier(int.MaxValue, damageMultiplier));
		Debug.Log("[SheepPerviousStatusEffect] Multiplying damage by "+damageMultiplier+"!");
		myCondition.Remove();//can only be procced once
	}

	void Update(){
		base.Update();
	}

}
