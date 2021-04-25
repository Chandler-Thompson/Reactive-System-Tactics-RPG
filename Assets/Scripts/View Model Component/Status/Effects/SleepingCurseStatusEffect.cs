using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingCurseStatusEffect : StatusEffect
{

	Board board = null;

	int stackIncrementAmount = 1;

	int horizontalDistance = 2;
	int verticalDistance = 2;

    void OnEnable ()
    {
    	if(owner)
    	{
    		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    	}

    	board = GameObject.Find("Battle Controller").GetComponent<Board>();

    }

    void OnDisable()
    {
    	this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    }

    void OnNewTurn(object sender, object args)
    {

    	Debug.Log("[SleepingCurseStatusEffect] My turn!");

    	//Add another stack to the owner
    	IncrementStacks(status);

    	//Spread curse to nearby potential owners
    	if(board)
    	{
    		List<Tile> potentialTargets = board.Search(owner.tile, ExpandSearch);

    		foreach(Tile target in potentialTargets)
    		{
    			// Checking if there is a unit on the tile,
    			// as opposed to something else that can also hold statuses
    			Unit otherUnit = target.content.GetComponent<Unit>();
    			if(otherUnit)
    			{
    				Status otherStatus = target.content.GetComponent<Status>();
    				IncrementStacks(otherStatus); // Also adds initial stacks to new victim
    			}
    		}

    	}
    }

    void IncrementStacks(Status givenStatus)
    {
    	Debug.Log("[SleepingCurseStatusEffect] Incrementing stacks...");
		StackingStatusCondition newStack = givenStatus.Add<SleepingCurseStatusEffect, StackingStatusCondition>();
    	newStack.numStacks += stackIncrementAmount;
    	newStack.stackType = StackConditionTypes.PrepareForSheep;
    }

    bool ExpandSearch(Tile from, Tile to)
    {
    	return (from.distance + 1) <= horizontalDistance && Mathf.Abs(to.height - owner.tile.height) <= verticalDistance;
    }

}
