using System.Collections.ObjectModel;

namespace FlowForge.Engine;

public class Engine : IEngine
{
    private readonly ObservableCollection<Node.Node> _nodes = [];
    public ReadOnlyObservableCollection<Node.Node> Nodes { get; }
    private bool _isInitialized;

    public Engine()
    {
        Nodes = new ReadOnlyObservableCollection<Node.Node>(_nodes);
    }
    
    public Task InitializeAsync()
    {
        // TODO: add real logic here
        
        if (_isInitialized) return Task.CompletedTask;
        
        _nodes.Add(new Node.Node("Node 1"));
        _nodes.Add(new Node.Node("Node 2"));
        _nodes.Add(new Node.Node("Node 3"));

        _isInitialized = true;

        return Task.CompletedTask;
    }
    
    public async Task StartAsync()
    {
        await Runner.Run(_nodes);
    }
    
    public async Task StopAsync()
    {
        await Runner.Stop(_nodes);
    }
}