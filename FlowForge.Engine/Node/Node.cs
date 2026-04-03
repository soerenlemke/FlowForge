namespace FlowForge.Engine.Node;

public class Node(string name, DateTime executionTime = default)
{
    public required string Name { get; set; } = name;
    public DateTime? ExecutionTime { get; set; } = executionTime;
    public NodeState State { get; set; } = NodeState.WaitingForInitializing;
}