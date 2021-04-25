using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepParalysisStatusEffect : StatusEffect
{

    bool statsChanged = false;
    int amountSlowedStage1 = 0;
    int amountSlowedStage2 = 0;

    int oldNumStage = 0;
    int currNumStage = 0;

    void OnEnable()
    { 
    	base.initialize();

    	if(owner)
    	{
    		this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
    		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    	}

    	//Show the status effect visually
    	Debug.Log("[SleepParalysisStatusEffect] Enabled!");
    }

    void OnDisable ()
	{	
		currNumStage = (myCondition as StackingStatusCondition).numStacks;
		Debug.Log("[SleepParalysisStatusEffect] Disabling with "+currNumStage+" stacks...");

		//Revert the debuffs placed by current stage and those below it
		if(currNumStage >= 1)
		{
			RevertStage1();
		}
		if(currNumStage >= 2)
		{
			RevertStage2();
		}
		if(currNumStage >= 3)
		{
			RevertStage3();
		}

		this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);

		Debug.Log("[SleepParalysisStatusEffect] Disabled!");
	}

	void OnHPDidChange (object sender, object args)
	{
		// if(currNumStage == 3)
		// 	Stage3();
	}

	void OnNewTurn (object sender, object args)
	{
		Debug.Log("[ParalysisStatusEffect] New turn. Decrementing stacks.");
		(myCondition as StackingStatusCondition).numStacks--;	
	}

	void Stage1 ()
	{
		//Reduce unit's movement (They are slowly getting paralysed!)
    	if(stats)
    	{
            amountSlowedStage1 = stats[StatTypes.MOV]/2;
    		stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]/2, false);
    		statsChanged = true;
    	}
	}

	void RevertStage1 ()
	{
		if(statsChanged)
		{
			Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 1...");
			stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]+amountSlowedStage1, false);
			amountSlowedStage1 = 0;
		}
	}

	void Stage2 ()
	{
		//Remove unit's movement (They are paralysed!)
    	if(stats)
    	{
            amountSlowedStage2 = stats[StatTypes.MOV];
    		stats.SetValue(StatTypes.MOV, 0, false);
    		statsChanged = true;
    	}
	}

	void RevertStage2 ()
	{
		if(statsChanged)
		{
			Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 2...");
			Debug.Log("[SleepParalysisStatusEffect]\tCurr MOV:"+stats[StatTypes.MOV]+" | Returned MOV:"+amountSlowedStage2);
			stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]+amountSlowedStage2, false);
			Debug.Log("[SleepParalysisStatusEffect]\tCurr MOV:"+stats[StatTypes.MOV]);
			amountSlowedStage2 = 0;
		}
	}

	void Stage3 ()
	{
		//owner is paralysed, knock them towards Sandman
		BattleController bc = GameObject.Find("Battle Controller").GetComponent<BattleController>();

		Tile newTile = bc.board.GetNextTileInDirection(owner.tile, Directions.West);
		while (newTile != null && newTile.content != null)
			newTile = bc.board.GetNextTileInDirection(owner.tile, Directions.West);

		if(newTile != null){
			owner.Place(newTile);
			owner.Match();
		}

		//Can't do anything if already at edge of map (case where newTile is null here...)

	}

	void RevertStage3 ()
	{
		Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 3...");
		//Nothing to revert...
	}

	void Update (){
		base.Update();

		oldNumStage = currNumStage;
		currNumStage = (myCondition as StackingStatusCondition).numStacks;

		if(currNumStage == 0)
			myCondition.Remove();

		int transitionDirection = currNumStage - oldNumStage;

		if(transitionDirection < 0)
		{ // Revert stages
			switch(currNumStage)
			{ // No need for case 0, RevertStage1 called in OnDisable
				case 1:
					RevertStage2();
					break;
				case 2:
					RevertStage3();
					break;
			}
		}else if(transitionDirection > 0)
		{ // Advance stages
			switch(currNumStage)
			{
				case 1:
					Stage1();
					break;
				case 2:
					Stage2();
					break;
				case 3:
					Stage3();
					break;
			}
		}

	}
}
