using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepScapeStatusEffect : StatusEffect
{
	
	void OnEnable ()
	{
		base.initialize();
		this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);

		//Initial check, in case unit is already dead
		OnHPDidChange(null, null);
	}
	
	void OnDisable ()
	{
		this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
	}
	
	void OnHPDidChange (object sender, object args)
	{
		if(stats[StatTypes.HP] == 0){

			GameObject newOwnerInstance = null;
			if(owner.transform.name.Equals("Sleep Sheep"))
				newOwnerInstance = UnitFactory.Change(owner, "Sleep Demon");
			else if(owner.transform.name.Equals("Sleep Demon"))
				newOwnerInstance = UnitFactory.Change(owner, "Sleep Sheep");

			if(newOwnerInstance){
				Tile tile = owner.tile;
				Directions dir = owner.dir;

				GameObject bcObj = GameObject.Find("Battle Controller");
				newOwnerInstance.transform.SetParent(bcObj.transform.Find("Units"));
				BattleController bc = bcObj.GetComponent<BattleController>();

				bc.units.Remove(owner);
				Destroy(owner.transform.gameObject);

				owner = newOwnerInstance.GetComponent<Unit>();
				owner.Place(tile);
				owner.dir = dir;
				owner.Match();
				bc.units.Add(owner);

			}else{
				GameObject.Find("Battle Controller").GetComponent<BattleController>().units.Remove(owner);
				Destroy(owner);
			}

		}
	}
	
}
