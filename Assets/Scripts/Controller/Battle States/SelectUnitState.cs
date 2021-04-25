using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState 
{
	public override void Enter ()
	{
		Debug.Log("[SelectUnitState] Entered State.");
		base.Enter ();
		StartCoroutine("ChangeCurrentUnit");
	}

	public override void Exit ()
	{
		base.Exit ();
		statPanelController.HidePrimary();
	}

	IEnumerator ChangeCurrentUnit ()
	{
		Debug.Log("[SelectUnitState] Moving to next round.");
		owner.round.MoveNext();
		SelectTile(turn.actor.tile.pos);
		RefreshPrimaryStatPanel(pos);
		yield return null;
		owner.ChangeState<CommandSelectionState>();
	}
}