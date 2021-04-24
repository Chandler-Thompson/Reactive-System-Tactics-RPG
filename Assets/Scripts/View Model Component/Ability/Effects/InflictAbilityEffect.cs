using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class InflictAbilityEffect : BaseAbilityEffect 
{
	public string statusName;
	public bool isStackable;
	public int initialStacks;
	public bool hasMaxStacks;
	public int maxStacks;
	public bool isLimitedDuration;
	//[DrawIf("isLimitedDuration", true, ComparisonType.Equals)]
	public int duration;
	

	public override int Predict (Tile target)
	{
		return 0;
	}

	protected override int OnApply (Tile target)
	{
		Type statusType = Type.GetType(statusName);
		if (statusType == null || !statusType.IsSubclassOf(typeof(StatusEffect)))
		{
			Debug.LogError("Invalid Status Type");
			return 0;
		}

		MethodInfo mi = typeof(Status).GetMethod("Add");

		if(isLimitedDuration){
			//Add DurationStatusCondition
			Type[] types = new Type[]{ statusType, typeof(DurationStatusCondition) };
			MethodInfo constructed = mi.MakeGenericMethod(types);

			Status status = target.content.GetComponent<Status>();
			object durationRetValue = constructed.Invoke(status, null);

			DurationStatusCondition condition = durationRetValue as DurationStatusCondition;
			condition.duration = duration;
		}

		if(isStackable){
			//Add StackingStatusCondition
			Type[] types = new Type[]{ statusType, typeof(StackingStatusCondition) };
			MethodInfo constructed = mi.MakeGenericMethod(types);

			Status status = target.content.GetComponent<Status>();
			object stackingRetValue = constructed.Invoke(status, null);

			StackingStatusCondition condition = stackingRetValue as StackingStatusCondition;
			condition.numStacks = initialStacks;
			if(hasMaxStacks)
				condition.maxStacks = maxStacks;
			else
				condition.maxStacks = int.MaxValue;
		}
		
		return 0;
	}
}