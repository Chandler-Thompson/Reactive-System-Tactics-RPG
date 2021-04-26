using UnityEngine;

public class SavedData
{
    private SavedData(string value) { Value = value; }

    public string Value { get; set; }

    public static SavedData IntroConvo	{ get { return new SavedData("IntroConvo"); }}
    public static SavedData OutroConvoWin	{ get { return new SavedData("OutroConvoWin"); }}
    public static SavedData OutroConvoLose	{ get { return new SavedData("OutroConvoLose"); }}

    public static SavedData CurrLevelNum	{ get { return new SavedData("CurrLevelNum"); }}
    public static SavedData CurrLevelName   { get { return new SavedData("CurrLevelName"); }}
    public static SavedData CurrLevelUnitRecipes   { get { return new SavedData("CurrLevelUnitRecipes"); }}
    public static SavedData CurrLevelUnitLevels    { get { return new SavedData("CurrLevelUnitLevels"); }}

    public static SavedData LevelScores	{ get { return new SavedData("LevelScores"); }}

}