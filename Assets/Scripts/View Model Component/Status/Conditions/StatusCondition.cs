using UnityEngine;
using System.Collections;

public class StatusCondition : MonoBehaviour
{

	public const string UpdatedNotification = "StatusCondition.UpdatedNotification";

	public string text { get { return _text; }}

	protected Unit parentUnit;
	protected Status parentStatus;
	protected StatusEffect parentEffect;

	string _text;

	protected void UpdateText(string text){
		_text = text;
		parentUnit.PostNotification(UpdatedNotification, parentEffect);
	}

	protected void Update(){
		if(parentUnit == null)
			parentUnit = this.GetComponentInParent<Unit>();
		if(parentStatus == null)
			parentStatus = GetComponentInParent<Status>();
		if(parentEffect == null)
			parentEffect = GetComponentInParent<StatusEffect>();
	}

	public virtual void Remove ()
	{

		UpdateText("");

		if (parentStatus)
			parentStatus.Remove(this);
	}

}