using System;

public class StoryNode
{
    public string[] Answers;
    public string History;
    public int Index;
    public bool IsFinal;
    public bool LoseGame;
    public StoryNode[] NextNode;
    public Action OnNodeVisited;
}