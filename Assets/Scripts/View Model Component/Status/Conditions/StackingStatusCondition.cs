using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingStatusCondition : StatusCondition
{
	public StackConditionTypes stackType;
    public int numStacks = 1;

    private bool isOriginal = true;

	void OnEnable ()
	{
		base.Update();//to get parentStatus
		
		//take on title of original if there are no other stacks of this type
		StackingStatusCondition[] otherStacks = parentStatus.GetComponentsInChildren<StackingStatusCondition>();
		foreach(StackingStatusCondition otherStack in otherStacks){
			if(otherStack.stackType == this.stackType && !otherStack.Equals(this)){
				isOriginal = false;
				return;
			}
		}
	}

	void OnDisable ()
	{	
		//pass on title of original on death of this stack, if possible
		if(isOriginal){
			StackingStatusCondition[] otherStacks = parentStatus.GetComponentsInChildren<StackingStatusCondition>();
			foreach(StackingStatusCondition otherStack in otherStacks){
				if(otherStack.stackType == this.stackType && !otherStack.Equals(this)){
					otherStack.isOriginal = true;
					break;
				}
			}
		}
		numStacks = 0;
	}

	void Update(){
		base.Update();

		if(parentStatus && isOriginal){//only the original status condition should collect other's stacks

			StackingStatusCondition[] otherStacks = parentStatus.GetComponentsInChildren<StackingStatusCondition>();
			foreach(StackingStatusCondition otherStack in otherStacks){
				if(otherStack.stackType == this.stackType && !otherStack.Equals(this)){
					this.numStacks += otherStack.numStacks;
					Debug.Log("[StackingStatusCondition] Took stacks from other "+this.stackType+" StackingStatusCondition.");
					otherStack.Remove();
					Debug.Log("[StackingStatusCondition] Removed other "+this.stackType+" StackingStatusCondition.");
				}
			}

		}else if(parentStatus == null){
			Debug.Log("[StackingStatusCondition] No parent status found!");
		}else{
			//Not original stack, so just wait to be collected from and removed
		}
	}
}
