using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSleepAbilityEffect : BaseAbilityEffect
{
    static HashSet<Type> CurableTypes
	{
		get
		{
			if (_curableTypes == null)
			{
				_curableTypes = new HashSet<Type>();
				_curableTypes.Add( typeof(SleepParalysisStatusEffect) );
			}
			return _curableTypes;
		}
	}
	static HashSet<Type> _curableTypes;

	public override int Predict (Tile target)
	{
		return 0;
	}

	protected override int OnApply (Tile target)
	{
		Unit defender = target.content.GetComponent<Unit>();
		Status status = defender.GetComponentInChildren<Status>();

		StackingStatusCondition[] candidates = status.GetComponentsInChildren<StackingStatusCondition>();
		for (int i = candidates.Length - 1; i >= 0; --i)
		{
			StatusEffect effect = candidates[i].GetComponentInParent<StatusEffect>();
			if ( CurableTypes.Contains( effect.GetType() ))
				candidates[i].Remove();
		}
		return 0;
	}
}
