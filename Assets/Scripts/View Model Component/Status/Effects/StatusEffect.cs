using System;
using UnityEngine;
using System.Collections;

public abstract class StatusEffect : MonoBehaviour 
{

	public string name { get { return _name; }}

	protected Transform myParent;
	protected StatusCondition myCondition;

	protected Unit owner;
	protected Stats stats;
	protected Status status;

	protected Tile holder;

	protected Material mainMaterial;

	string _name;

	protected void initialize(string name){
		_name = name;

		myParent = transform.parent;
		myCondition = this.GetComponentInChildren<StatusCondition>();

		owner = GetComponentInParent<Unit>();
		stats = GetComponentInParent<Stats>();
		status = GetComponentInParent<Status>();

		holder = GetComponentInParent<Tile>();

		try{
			mainMaterial = myParent.GetComponentInChildren<MeshRenderer>().material;
		}catch(NullReferenceException e){
			mainMaterial = null;
		}

	}

	protected void Update(){
		if(myCondition == null){
			myCondition = this.GetComponentInChildren<StatusCondition>();
		}
	}

}