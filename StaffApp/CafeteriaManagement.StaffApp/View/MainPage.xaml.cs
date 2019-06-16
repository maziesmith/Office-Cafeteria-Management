using System.Windows.Controls;
using CafeteriaManagement.StaffApp.ViewModel;
using Syncfusion.UI.Xaml.Kanban;

namespace CafeteriaManagement.StaffApp.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SfKanban_OnCardDragEnd(object sender, KanbanDragEndEventArgs e)
        {
            ((MainPageViewModel)DataContext).ItemMoved(e);
        }
    }
}
