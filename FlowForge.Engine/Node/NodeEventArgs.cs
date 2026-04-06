namespace FlowForge.Engine.Node;

public class NodeEventArgs(Node node) : EventArgs
{
    public Node Node { get; } = node;
}