using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingStatusCondition : StatusCondition
{
	public StackConditionTypes stackType;
    public int numStacks = 0;

    private bool isOriginal = false;

	void OnEnable ()
	{
		// StackingStatusCondition[] otherStacks = parentStatus.GetComponentsInChildren<StackingStatusCondition>();
		// foreach(StackingStatusCondition otherStack in otherStacks){
		// 	if(otherStack.stackType == this.stackType && !otherStack.Equals(this)){
		// 		this.numStacks += otherStack.numStacks;
		// 		otherStack.Remove();
		// 	}
		// }
		// if(){

		// }
		numStacks++;
	}

	void OnDisable ()
	{
		numStacks = 0;
	}

	void Update(){
		base.Update();

		if(parentStatus){

			StackingStatusCondition[] otherStacks = parentStatus.GetComponentsInChildren<StackingStatusCondition>();
			foreach(StackingStatusCondition otherStack in otherStacks){
				if(otherStack.stackType == this.stackType && !otherStack.Equals(this)){
					Debug.Log("[StackingStatusCondition] Took stacks from other "+this.stackType+" StackingStatusCondition.");
					this.numStacks += otherStack.numStacks;
					otherStack.Remove();
				}
			}

		}else{
			Debug.Log("[StackingStatusCondition] No parent status found!");
		}
	}
}
