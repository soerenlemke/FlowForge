namespace FlowForge.Engine.Node;

public class Node(string name)
{
    public string Name { get; set; } = name;
    public NodeState State { get; set; } = NodeState.WaitingForInitializing;
}