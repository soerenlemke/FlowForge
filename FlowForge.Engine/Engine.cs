using System.Collections.ObjectModel;
using FlowForge.Engine.Node;

namespace FlowForge.Engine;

public class Engine : IEngine
{
    private readonly List<Node.Node> _nodes = [];
    private readonly ReadOnlyCollection<Node.Node> _readOnlyNodes;
    private bool _isInitialized;

    public Engine()
    {
        _readOnlyNodes = _nodes.AsReadOnly();
    }

    public IReadOnlyList<Node.Node> Nodes => _readOnlyNodes;

    public event EventHandler<NodeEventArgs>? NodeAdded;
    public event EventHandler<NodeEventArgs>? NodeRemoved;
    public event EventHandler<NodeStateChangedEventArgs>? NodeStateChanged;

    public Task InitializeAsync(CancellationToken ct = default)
    {
        if (_isInitialized) return Task.CompletedTask;

        AddNode(new Node.Node("Node 1"));
        AddNode(new Node.Node("Node 2"));
        AddNode(new Node.Node("Node 3"));

        foreach (var node in Nodes)
        {
            SetState(node, NodeState.Ready);
        }

        _isInitialized = true;
        return Task.CompletedTask;
    }

    public async Task StartNodeAsync(Guid nodeId, CancellationToken ct = default)
    {
        var node = FindNode(nodeId);

        try
        {
            SetState(node, NodeState.Running);
            await Task.Delay(5000, ct); // TODO: real logic should be executed here
            await StopNodeAsync(nodeId, ct);
        }
        catch (OperationCanceledException)
        {
            SetState(node, NodeState.Stopped);
            throw;
        }
        catch
        {
            SetState(node, NodeState.Error);
            throw;
        }
    }

    public async Task StopNodeAsync(Guid nodeId, CancellationToken ct = default)
    {
        var node = FindNode(nodeId);
        SetState(node, NodeState.Stopped);
    }

    private void AddNode(Node.Node node)
    {
        _nodes.Add(node);
        NodeAdded?.Invoke(this, new NodeEventArgs(node));
    }

    private void RemoveNode(Node.Node node)
    {
        _nodes.Remove(node);
        NodeRemoved?.Invoke(this, new NodeEventArgs(node));
    }

    private void SetState(Node.Node node, NodeState newState)
    {
        if (node.State == newState) return;

        var oldState = node.State;
        node.State = newState;
        NodeStateChanged?.Invoke(this, new NodeStateChangedEventArgs(node.Id, oldState, newState));
    }

    private Node.Node FindNode(Guid nodeId)
    {
        var node = _nodes.FirstOrDefault(n => n.Id == nodeId);
        return node ?? throw new ArgumentException($"Node with ID {nodeId} not found.");
    }
}