namespace FlowForge.Engine.Node;

public abstract class NodeHandler
{
    public static async Task LoadNodeAsync(Node node)
    {
        node.State = NodeState.Initializing;
        
        // TODO: implement real Logic
        await Task.Delay(2000);

        // TODO: if loading fails we go into Error state
        node.State = true ? NodeState.Ready : NodeState.Error;
    }

    public static async Task SetNodeForExecutionAsync(Node node)
    {
        // TODO: any logic here? Maybe setting up nodes to a queue for execution?
        // TODO: option for running nodes at a specific time?
        await Task.Delay(2000);
        
        node.State = NodeState.WaitingForExecution;
    }

    public static async Task ExecuteNodeAsync(Node node)
    {
        node.State = NodeState.Running;
        
        // TODO: implement real Logic
        await Task.Delay(5000);
        
        node.State = NodeState.Stopped;
    }

    public static Task StopNode(Node node)
    {
        node.State = NodeState.Stopping;
        return Task.CompletedTask;
    }
}