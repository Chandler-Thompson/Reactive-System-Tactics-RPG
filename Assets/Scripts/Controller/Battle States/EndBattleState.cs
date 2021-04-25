using UnityEngine;
using System.Collections;

public class EndBattleState : BattleState 
{

	GameController gc;

	public override void Enter ()
	{
		base.Enter ();
		gc = GameObject.Find("Game Controller").GetComponent<GameController>();
		gc.sceneController.LoadScene("Transition Scene");
	}
}
