using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePlacesAbilityEffect : BaseAbilityEffect
{
    public override int Predict (Tile target)
	{
		return 0;
	}

	protected override int OnApply (Tile target)
	{
		Unit attacker = GetComponentInParent<Unit>();
		Unit defender = target.content.GetComponent<Unit>();

		Tile oldAttackerTile = attacker.tile;
		Directions oldAttackerDir = attacker.dir;

		UnitFactory.Situate(attacker, defender.tile, defender.dir);
		UnitFactory.Situate(defender, oldAttackerTile, oldAttackerDir);

		return 0;
	}
}
