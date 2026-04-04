using System.Collections.ObjectModel;

namespace FlowForge.Engine;

public class Engine : IEngine
{
    public ObservableCollection<Node.Node> Nodes { get; } = [];
    private bool _isInitialized;
    
    public Task InitializeAsync()
    {
        // TODO: add real logic here
        
        if (_isInitialized) return Task.CompletedTask;
        
        Nodes.Add(new Node.Node("Node 1"));
        Nodes.Add(new Node.Node("Node 2"));
        Nodes.Add(new Node.Node("Node 3"));

        _isInitialized = true;

        return Task.CompletedTask;
    }
    
    public async Task StartAsync()
    {
        await Runner.Run(Nodes);
    }
    
    public async Task StopAsync()
    {
        await Runner.Stop(Nodes);
    }
}