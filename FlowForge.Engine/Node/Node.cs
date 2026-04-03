namespace FlowForge.Engine.Node;

public class Node(string name)
{
    public required string Name { get; set; } = name;
    public NodeState State { get; set; }
}