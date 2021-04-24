using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition 
{
	public int duration = 10;

	void OnEnable ()
	{
		this.AddObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
	}

	void OnNewTurn (object sender, object args)
	{
		Debug.Log("[DurationStatusCondition] New Turn! Decrementing duration.");
		duration--;
		if (duration <= 0)
			Remove();
	}
}
