namespace FlowForge.Engine.Node;

public enum NodeState
{
    WaitingForInitializing,
    Initializing,
    Ready,
    WaitingForExecution,
    Running,
    Stopping,
    Stopped,
    Error
}