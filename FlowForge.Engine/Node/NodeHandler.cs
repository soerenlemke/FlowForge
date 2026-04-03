namespace FlowForge.Engine.Node;

public abstract class NodeHandler
{
    public static async Task LoadNode(Node node)
    {
        node.State = NodeState.Initializing;
        
        // TODO: implement real Logic
        await Task.Delay(2000);

        // TODO: if loading fails we go into Error state
        node.State = true ? NodeState.Ready : NodeState.Error;
    }

    public static async Task ExecuteNode(Node node)
    {
        node.State = NodeState.Running;
        
        // TODO: implement real Logic
        await Task.Delay(5000);
        
        node.State = NodeState.Stopped;
    }
}