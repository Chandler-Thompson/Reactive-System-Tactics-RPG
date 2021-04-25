using UnityEngine;
using System.Collections;

public class StatusCondition : MonoBehaviour
{

	public const string UpdatedNotification = "StatusCondition.UpdatedNotification";

	public string text { get { return _text; }}

	protected Status parentStatus;
	protected StatusEffect parentEffect;

	string _text;

	protected void UpdateText(string text){
		_text = text;
		Unit owner = this.GetComponentInParent<Unit>();
		Debug.Log("[StatusCondition] ParentEffect: "+parentEffect);
		owner.PostNotification(UpdatedNotification, parentEffect);
	}

	protected void Update(){
		if(parentStatus == null)
			parentStatus = GetComponentInParent<Status>();
		if(parentEffect == null)
			parentEffect = GetComponentInParent<StatusEffect>();
	}

	public virtual void Remove ()
	{
		if (parentStatus)
			parentStatus.Remove(this);
	}

}