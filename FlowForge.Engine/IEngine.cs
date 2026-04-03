using System.Collections.ObjectModel;

namespace FlowForge.Engine;

public interface IEngine
{
    ReadOnlyObservableCollection<Node.Node>  Nodes { get; }
    
    Task InitializeAsync();
    Task StartAsync();
    Task StopAsync();
}