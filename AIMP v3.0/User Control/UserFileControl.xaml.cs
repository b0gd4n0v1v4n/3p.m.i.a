using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using Models.Entities;

namespace AIMP_v3._0.User_Control
{
    /// <summary>
    /// Interaction logic for UserFileControl.xaml
    /// </summary>
    public partial class UserFileControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserFileControl()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register("Text", typeof(string), typeof(UserFileControl),
        new UIPropertyMetadata("FILE", _TextChanged));

        public int? UserFileId
        {
            get { return (int?)GetValue(UserFileIdProperty); }
            set { SetValue(UserFileIdProperty, value); }
        }

        public static readonly DependencyProperty UserFileIdProperty =
    DependencyProperty.Register("UserFileId", typeof(int?), typeof(UserFileControl),
        new UIPropertyMetadata());

        private static void _TextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as UserFileControl).OnPropertyChanged("Text");
        }

        public bool OpenButtonIsEnable
        {
            get { return (bool) GetValue(OpenButtonIsEnableProperty); }
            set { SetValue(OpenButtonIsEnableProperty, value); }
        }

        public static readonly DependencyProperty OpenButtonIsEnableProperty =
            DependencyProperty.Register("OpenButtonIsEnable", typeof (bool), typeof (UserFileControl),
                new UIPropertyMetadata(false, (o, args) =>
                {
                    (o as UserFileControl).OnPropertyChanged("OpenButtonIsEnable");
                }));

        public UserFile UserFile
        {
            get { return (UserFile)GetValue(UserFileProperty); }
            set { SetValue(UserFileProperty, value); }
        }

        public static readonly DependencyProperty UserFileProperty =
            DependencyProperty.Register("UserFile", typeof(UserFile), typeof(UserFileControl),
                new UIPropertyMetadata(null));

        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserFile != null)
                {
                    OpenUserFile.Open(UserFile.Name, UserFile.File);
                }
                else if(UserFileId != null)
                {
                    using (AimpService service = new AimpService())
                    {
                        var response = service.GetUserFile(UserFileId.Value);
                        if(response.Error)
                            throw new Exception(response.Message);

                        UserFile = response.UserFile;
                        OpenUserFile.Open(UserFile.Name, UserFile.File);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось открыть файл",ex.Message);
            }
        }

        private void Explorer_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "(*.jpg;*.png;*.jpeg;*.bmp;*.rar;*.pdf) | *.jpg;*.png;*.jpeg;*.bmp;*.rar;*.pdf";

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        UserFile file = new UserFile();
                        file.Name = open.SafeFileName;
                        file.File = OpenUserFile.GetFile(open.FileName);

                        SetValue(UserFileProperty, file);
                        SetValue(OpenButtonIsEnableProperty, true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось добавить файл");
            }
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            SetValue(UserFileProperty, null);
            SetValue(UserFileIdProperty, null);
            SetValue(OpenButtonIsEnableProperty, false);
        }
    }
}
