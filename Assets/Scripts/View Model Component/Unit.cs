using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	public Tile tile { get; protected set; }
	public Directions dir;
	private Directions oldDir;
	private int convertedDir = 0;
	private Animator anim;

	public void Place (Tile target)
	{
		// Make sure old tile location is not still pointing to this unit
		if (tile != null && tile.content == gameObject)
			tile.content = null;
		
		// Link unit and tile references
		tile = target;
		
		if (target != null)
			target.content = gameObject;
	}

	public void Match ()
	{
		transform.localPosition = tile.center;
		transform.localEulerAngles = dir.ToEuler();
	}

	public void Start()
	{ 
		anim = GetComponentInChildren<Animator>();
	}

	public void Update()
	{
		if (dir != oldDir)
		{
			oldDir = dir;
			
			if (dir == Directions.East) convertedDir = 0;
			else if (dir == Directions.South) convertedDir = 1;
			else if (dir == Directions.West) convertedDir = 2;
			else if (dir == Directions.North) convertedDir = 3;

			Debug.Log("Dir = " + dir);

			if (anim != null)
			{
				anim.SetInteger("Direction", convertedDir);
			}
			
		}
	}
}