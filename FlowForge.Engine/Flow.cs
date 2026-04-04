using System.Collections.ObjectModel;

namespace FlowForge.Engine;

// TODO: design the flow -> combine multiple Nodes and for example set execution times

public class Flow
{
    public ObservableCollection<Node.Node> Nodes { get; } = [];
}