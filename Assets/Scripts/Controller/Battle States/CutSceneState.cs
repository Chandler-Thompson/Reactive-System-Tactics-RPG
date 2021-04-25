using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutSceneState : BattleState 
{
	ConversationController conversationController;
	ConversationData data;

	protected override void Awake ()
	{
		base.Awake ();
		conversationController = owner.GetComponentInChildren<ConversationController>();
	}

	public override void Enter ()
	{
		base.Enter ();
		if (IsBattleOver())
		{
			if (DidPlayerWin())
			{
				string winConvo = PlayerPrefsController.GetString(SavedData.OutroConvoWin);
				data = Resources.Load<ConversationData>(winConvo);
			}
			else
			{
				string loseConvo = PlayerPrefsController.GetString(SavedData.OutroConvoLose);
				data = Resources.Load<ConversationData>(loseConvo);
			}
		}
		else
		{	
			string introConvo = PlayerPrefsController.GetString(SavedData.IntroConvo);
			data = Resources.Load<ConversationData>(introConvo);
		}


		if(data)
		{
			conversationController.Show(data);
		}
		else
		{
			OnCompleteConversation(null, null);
		}
	}

	public override void Exit ()
	{
		Debug.Log("[CutSceneState] Exiting...");
		base.Exit ();
		if (data)
			Resources.UnloadAsset(data);
	}

	protected override void AddListeners ()
	{
		base.AddListeners ();
		ConversationController.completeEvent += OnCompleteConversation;
	}

	protected override void RemoveListeners ()
	{
		base.RemoveListeners ();
		ConversationController.completeEvent -= OnCompleteConversation;
	}

	protected override void OnFire (object sender, InfoEventArgs<int> e)
	{
		base.OnFire (sender, e);
		conversationController.Next();
	}

	void OnCompleteConversation (object sender, System.EventArgs e)
	{
		if (IsBattleOver())
		{
			owner.ChangeState<EndBattleState>();
		}
		else
		{
			Debug.Log("[CutSceneState] Moving to SelectUnitState");
			owner.ChangeState<SelectUnitState>();
		}
	}
}
