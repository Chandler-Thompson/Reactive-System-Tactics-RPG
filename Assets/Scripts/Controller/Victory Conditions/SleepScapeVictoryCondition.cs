using UnityEngine;
using System.Collections;

public class SleepScapeVictoryCondition : BaseVictoryCondition 
{

	public Unit hero;

	Color victoryTileColor = new Color(1, 1, 1, 1);
	Color defeatTileColor = new Color(0, 0, 1, 0);

	protected override void OnEnable ()
	{
		base.OnEnable();

		//Set max tiles to defeat
		for(int i = 0; i < bc.board.max.y; i++)
		{
			Point point = new Point(bc.board.max.x, i);
			Tile tile = bc.board.GetTile(point);

			if(tile)
			{
				Transform quad = tile.transform.Find("Quad");
				if(quad){
					quad.GetComponent<Renderer>().material.SetColor("_BaseColor", defeatTileColor);
				}else{
					tile.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", defeatTileColor);
				}
			}
			

		}

		//Set min tiles to victory
		for(int i = 0; i < bc.board.min.y; i++)
		{
			Point point = new Point(bc.board.min.x, i);
			Tile tile = bc.board.GetTile(point);

			if(tile)
			{
				Transform quad = tile.transform.Find("Quad");
				if(quad){
					quad.GetComponent<Renderer>().material.SetColor("_BaseColor", victoryTileColor);
				}else{
					tile.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", victoryTileColor);
				}
			}

		}

	}

	protected override void CheckForGameOver ()
	{
		base.CheckForGameOver();
		if (hero.tile.pos.x == bc.board.max.x)
		{
			Victor = Alliances.Enemy;
		}
		else if (hero.tile.pos.x == bc.board.min.x)
		{
			Victor = Alliances.Hero;
		}
	}
}