using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using FlowForge.Engine;
using FlowForge.Engine.Node;

namespace FlowForge.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IEngine _engine;

    [ObservableProperty]
    private ObservableCollection<NodeItemViewModel> _nodes = [];

    public MainWindowViewModel(IEngine engine)
    {
        _engine = engine;

        foreach (var node in engine.Nodes)
        {
            Nodes.Add(new NodeItemViewModel(_engine, node.Id, node.Name, node.State));
        }
        
        _engine.NodeAdded += OnNodeAdded;
        _engine.NodeRemoved += OnNodeRemoved;
        _engine.NodeStateChanged += OnNodeStateChanged;
    }

    private void OnNodeAdded(object? sender, NodeEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            Nodes.Add(new NodeItemViewModel(_engine, e.Node.Id, e.Node.Name, e.Node.State));
        });
    }
    
    private void OnNodeRemoved(object? sender, NodeEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var removed = Nodes.FirstOrDefault(n => n.Id == e.Node.Id);
            if (removed != null) Nodes.Remove(removed);
        });
    }
    
    private void OnNodeStateChanged(object? sender, NodeStateChangedEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var changed = Nodes.FirstOrDefault(n => n.Id == e.NodeId);
            changed?.State = e.NewState;
        });
    }
}