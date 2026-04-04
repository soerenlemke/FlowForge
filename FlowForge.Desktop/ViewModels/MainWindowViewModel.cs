using System.Collections.ObjectModel;
using FlowForge.Engine;
using FlowForge.Engine.Node;

namespace FlowForge.Desktop.ViewModels;

public partial class MainWindowViewModel(IEngine engine) : ViewModelBase
{
    public ObservableCollection<Node> Nodes => engine.Nodes;
}