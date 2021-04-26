using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState 
{
	public override void Enter ()
	{
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
		owner.round.MoveNext();
		SelectTile(turn.actor.tile.pos);
		RefreshPrimaryStatPanel(pos);
		yield return null;
		if (IsBattleOver())
		{
			if(PlayerPrefsController.GetString(SavedData.OutroConvoWin).Equals("") 
				|| PlayerPrefsController.GetString(SavedData.OutroConvoLose).Equals(""))
			{
				owner.ChangeState<EndBattleState>();
			}
			else
			{
				owner.ChangeState<CutSceneState>();
			}
		}
		else
		{
			owner.ChangeState<CommandSelectionState>();
		}
	}
}