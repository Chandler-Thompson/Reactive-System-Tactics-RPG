using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState 
{
	public override void Enter ()
	{
		base.Enter ();
		Debug.Log("[SelectUnitState] Entered State.");
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