using ChartModule.ViewModel;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;


namespace ChartModule.View
{
    /// <summary>
    /// Interaction logic for ChartControl.xaml
    /// </summary>
    [Export("ChartControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class ChartControl : UserControl
    {
        [ImportingConstructor]
        public ChartControl(ChartViewModel vm)
        {
            InitializeComponent();
            vm.CalculateDelayCommand.Execute(RadioButtonGroup);
            this.DataContext = vm;
        }
    }
}
