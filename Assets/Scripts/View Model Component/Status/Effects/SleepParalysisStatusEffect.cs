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
    	base.initialize("SleepParalysisStatusEffect");

    	if(owner)
    	{
    		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    	}

    	//Show the status effect visually
    }

    void OnDisable ()
	{	

		//Revert the debuffs placed by current stage and those below it
		if(currNumStage >= 1)
		{
			Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 1.");
			RevertStage1();
		}
		if(currNumStage >= 2)
		{
			Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 2.");
			RevertStage2();
		}
		if(currNumStage >= 3)
		{
			Debug.Log("[SleepParalysisStatusEffect] Reverting Stage 3.");
			RevertStage3();
		}

		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);

	}

	void OnNewTurn (object sender, object args)
	{
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
			stats.SetValue(StatTypes.MOV, stats[StatTypes.MOV]+amountSlowedStage2, false);
			amountSlowedStage2 = 0;
		}
	}

	void Stage3 ()
	{
		//owner is paralysed, knock them towards Sandman
		BattleController bc = GameObject.Find("Battle Controller").GetComponent<BattleController>();

		Tile newTile = bc.board.GetNextTileInDirection(owner.tile, Directions.West);
		while (newTile != null && newTile.content != null)
			newTile = bc.board.GetNextTileInDirection(newTile, Directions.West);

		if(newTile != null){
			owner.Place(newTile);
			owner.Match();
		}

		//Can't do anything if already at edge of map (case where newTile is null here...)

	}

	void RevertStage3 ()
	{
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
			{
				case 0:
					RevertStage1();
					break;
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
