using FlowForge.Engine.Node;

namespace FlowForge.Engine;

public class Runner(List<Node.Node> nodes)
{
    public List<Node.Node> Nodes { get; set; } = nodes;

    public async Task Run()
    {
        foreach (var node in Nodes)
        {
            switch (node.State)
            {
                case NodeState.WaitingForInitializing:
                    await NodeHandler.LoadNode(node);
                    break;
                case NodeState.Initializing:
                case NodeState.Ready:
                case NodeState.WaitingForExecution:
                    await NodeHandler.ExecuteNode(node);
                    break;
                case NodeState.Running:
                case NodeState.Stopping:
                case NodeState.Stopped:
                case NodeState.Error:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}