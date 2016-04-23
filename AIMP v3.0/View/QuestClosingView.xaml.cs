using System.Windows;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Логика взаимодействия для QuestClosingView.xaml
    /// </summary>
    public partial class QuestClosingView : Window
    {
        public QuestClosingView()
        {
            InitializeComponent();
        }

        public QuestClosingView(string quest)
        {
            InitializeComponent();
            TextQuest.Text = quest;
        }
        private void NO_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void YES_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
