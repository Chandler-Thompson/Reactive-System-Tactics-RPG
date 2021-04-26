using UnityEngine;
using System.Collections;

public class SleepScapeVictoryCondition : BaseVictoryCondition 
{

	public Unit hero;

	protected override void CheckForGameOver ()
	{
		base.CheckForGameOver();
		Debug.Log("[SleepScapeVictoryCondition] Checking for game over...");
		Debug.Log("[SleepScapeVictoryCondition] Hero: "+hero.tile.pos.x);
		Debug.Log("[SleepScapeVictoryCondition] Lose: "+bc.board.max.x);
		Debug.Log("[SleepScapeVictoryCondition] Win: "+bc.board.min.x);
		if (hero.tile.pos.x == bc.board.max.x)
		{
			Debug.Log("[SleepScapeVictoryCondition] Eyy, you're bad!");
			Victor = Alliances.Enemy;
		}
		else if (hero.tile.pos.x == bc.board.min.x)
		{
			Debug.Log("[SleepScapeVictoryCondition] Eyy, ya won!");
			Victor = Alliances.Hero;
		}
	}
}