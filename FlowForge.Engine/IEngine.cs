using System.Collections.ObjectModel;

namespace FlowForge.Engine;

public interface IEngine
{
    ObservableCollection<Node.Node> Nodes { get; }
    
    Task InitializeAsync();
    Task StartAsync();
    Task StopAsync();
}