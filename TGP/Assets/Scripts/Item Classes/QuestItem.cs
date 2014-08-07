/// <summary>
/// QuestItem.cs
/// 
/// @Galen Manuel
/// @Feb. 24th, 2014
/// 
/// This is the base class for all quest items in the game.
/// </summary>
public class QuestItem : Item 
{
	private Quest _myQuest;

	public QuestItem() : base()
	{
		_myQuest = null;
	}

	public QuestItem(string name, string description, int maxAmt, int curAmt, bool canBeDestroyed, Quest myQuest)
	{
		name = name;
		description = description;
		maxAmount = maxAmt;
		curAmount = curAmt;
		canBeDestroyed = canBeDestroyed;
		_myQuest = myQuest;
	}

	public Quest MyQuest
	{
		get { return _myQuest; }
		set { _myQuest = value; }
	}
}
