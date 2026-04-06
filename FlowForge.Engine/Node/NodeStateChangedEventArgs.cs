namespace FlowForge.Engine.Node;

public sealed class NodeStateChangedEventArgs(
    Guid nodeId,
    NodeState oldState,
    NodeState newState) : EventArgs
{
    public Guid NodeId { get; set; } = nodeId;
    public NodeState OldState { get; set; } = oldState;
    public NodeState NewState { get; set; } = newState;
}