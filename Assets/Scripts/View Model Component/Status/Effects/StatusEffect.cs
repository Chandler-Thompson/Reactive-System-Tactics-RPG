using UnityEngine;
using System.Collections;

public abstract class StatusEffect : MonoBehaviour 
{
	protected Transform myParent;
	protected StatusCondition myCondition;

	protected Unit owner;
	protected Stats stats;
	protected Status status;

	protected Tile holder;

	protected Material mainMaterial;

	protected void initialize(){
		myParent = transform.parent;
		myCondition = this.GetComponentInChildren<StatusCondition>();

		owner = GetComponentInParent<Unit>();
		stats = GetComponentInParent<Stats>();
		status = GetComponentInParent<Status>();

		holder = GetComponentInParent<Tile>();

		mainMaterial = myParent.GetComponentInChildren<MeshRenderer>().material;

	}

}