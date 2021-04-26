using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndBattleState : BattleState 
{

	GameController gc;

	public override void Enter ()
	{
		base.Enter ();

		int currLevel = PlayerPrefsController.GetInt(SavedData.CurrLevelNum);
		List<int> levelScores = PlayerPrefsController.GetIntList(SavedData.LevelScores);

		if(owner.GetComponent<BaseVictoryCondition>().Victor == Alliances.Hero)
			levelScores[currLevel] = 1;
		if(owner.GetComponent<BaseVictoryCondition>().Victor == Alliances.Enemy)
			levelScores[currLevel] = -1;

		PlayerPrefsController.StoreIntList(SavedData.LevelScores, levelScores);

		gc = GameObject.Find("Game Controller").GetComponent<GameController>();
		gc.sceneController.LoadNextScene();
	}
}
