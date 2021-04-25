using UnityEngine;
using System.Collections;

public class StatusCondition : MonoBehaviour
{

	public string text { get { return _text; }}

	protected string _text;
	protected Status parentStatus;

	protected void Update(){
		if(parentStatus == null)
			parentStatus = GetComponentInParent<Status>();
	}

	public virtual void Remove ()
	{
		if (parentStatus)
			parentStatus.Remove(this);
	}

}