using UnityEngine;
using System.Collections;

public class PerformAbilityState : BattleState 
{
	public override void Enter ()
	{
		base.Enter ();
		turn.hasUnitActed = true;
		if (turn.hasUnitMoved)
			turn.lockMove = true;
		StartCoroutine(Animate());
	}
	
	IEnumerator Animate ()
	{
		// TODO play animations, etc
		turn.actor.callAttackAnim();
		yield return new WaitForSeconds(1.8f);
		ApplyAbility();
		
		Debug.Log("[PerformAbilityState] Checking for battle over...");

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
		else if (!UnitHasControl())
			owner.ChangeState<SelectUnitState>();
		else if (turn.hasUnitMoved)
		{
			Debug.Log("[PerformAbilityState] End Facing...");
			owner.ChangeState<EndFacingState>();
		}
		else
		{
			Debug.Log("[PerformAbilityState] Select again...");
			owner.ChangeState<CommandSelectionState>();
		}
	}
	
	void ApplyAbility ()
	{
		turn.ability.Perform(turn.targets);
	}
	
	bool UnitHasControl ()
	{
		return turn.actor.GetComponentInChildren<KnockOutStatusEffect>() == null;
	}
}