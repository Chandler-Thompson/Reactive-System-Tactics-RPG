using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition 
{
	public int duration { get { return _duration; } set { setDuration(value); }}

	int _duration = 10;

	void setDuration(int duration)
	{
		_duration = duration;
		_text = _duration.ToString();
	}

	void OnEnable ()
	{
		this.AddObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
	}

	void OnDisable ()
	{
		this.RemoveObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
	}

	void OnNewTurn (object sender, object args)
	{
		setDuration(_duration-1);
		if (_duration <= 0)
			Remove();
	}
}
