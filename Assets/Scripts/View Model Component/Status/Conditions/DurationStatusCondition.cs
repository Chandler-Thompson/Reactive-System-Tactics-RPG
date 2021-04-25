using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition 
{
	public int duration { get { return _duration; } set { setDuration(value); }}

	int _duration = 10;

	void setDuration(int duration)
	{
		_duration = duration;
		base.UpdateText(_duration.ToString());
	}

	void OnEnable ()
	{
		base.Update();
		
		this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, parentUnit);
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, parentUnit);
	}

	void OnNewTurn (object sender, object args)
	{
		setDuration(_duration-1);
		if (_duration <= 0)
			Remove();
	}

	void Update()
	{
		base.Update();
	}
}
