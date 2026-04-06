namespace FlowForge.Engine.Node;

public class Node(string name)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public NodeState State { get; internal set; } = NodeState.WaitingForInitializing;
}