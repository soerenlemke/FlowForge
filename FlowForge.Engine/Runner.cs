using FlowForge.Engine.Node;

namespace FlowForge.Engine;

public abstract class Runner
{
    public async Task Run(List<Node.Node> nodes)
    {
        var tasks = nodes.Select(ProcessNodeAsync);
        await Task.WhenAll(tasks);
    }

    public async Task Stop(List<Node.Node> nodes)
    {
        foreach (var node in nodes)
        {
            await NodeHandler.StopNode(node);
        }
    }

    private static async Task ProcessNodeAsync(Node.Node node)
    {
        try
        {
            switch (node.State)
            {
                case NodeState.WaitingForInitializing:
                    await NodeHandler.LoadNodeAsync(node);
                    break;

                case NodeState.Ready:
                    await NodeHandler.SetNodeForExecutionAsync(node);
                    break;

                case NodeState.WaitingForExecution:
                    await NodeHandler.ExecuteNodeAsync(node);
                    break;

                case NodeState.Initializing:
                case NodeState.Running:
                case NodeState.Stopping:
                case NodeState.Stopped:
                case NodeState.Error:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch
        {
            node.State = NodeState.Error;
            // TODO: log error
            throw;
        }
    }
}