using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepScapeStatusEffect : StatusEffect
{
	
	void OnEnable ()
	{
		base.initialize();

		if(owner && stats){
			this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
		}

		//Initial check, in case unit is already dead
		OnHPDidChange(null, null);
	}
	
	void OnDisable ()
	{
		this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
	}
	
	void OnHPDidChange (object sender, object args)
	{
		if(stats[StatTypes.HP] == 0)
		{
			StartCoroutine(ReplaceOnDeath());
		}
	}

	IEnumerator ReplaceOnDeath ()
	{
		yield return new WaitForSeconds(2.0f); // allow death animation to finish
		if(owner.transform.name.Equals("Sleep Sheep"))
			UnitFactory.Replace(owner, "Sleep Demon");
		else if(owner.transform.name.Equals("Sleep Demon"))
			UnitFactory.Replace(owner, "Sleep Sheep");
	}
	
}
