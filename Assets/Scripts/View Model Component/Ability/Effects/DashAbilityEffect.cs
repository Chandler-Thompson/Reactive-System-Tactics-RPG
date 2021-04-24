using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityEffect : BaseAbilityEffect
{
    public override int Predict (Tile target)
	{
		return 0;
	}

	protected override int OnApply (Tile target)
	{
		// Unit defender = target.content.GetComponent<Unit>();
		// Status status = defender.GetComponentInChildren<Status>();

		// DurationStatusCondition[] candidates = status.GetComponentsInChildren<DurationStatusCondition>();
		// for (int i = candidates.Length - 1; i >= 0; --i)
		// {
		// 	StatusEffect effect = candidates[i].GetComponentInParent<StatusEffect>();
		// 	if ( CurableTypes.Contains( effect.GetType() ))
		// 		candidates[i].Remove();
		// }
		return 0;
	}
}
