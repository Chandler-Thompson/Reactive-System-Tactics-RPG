using UnityEngine;

public class SavedData
{
    private SavedData(string value) { Value = value; }

    public string Value { get; set; }

    public static SavedData CurrLevelName   { get { return new SavedData("CurrLevelName"); } }
    public static SavedData CurrLevelUnitRecipes   { get { return new SavedData("CurrLevelUnitRecipes"); } }
    public static SavedData CurrLevelUnitLevels    { get { return new SavedData("CurrLevelUnitLevels"); } }

}