using System;
using System.IO;
using System.Windows;

namespace SimpleEncrypt
{
    public partial class MainWindow : Window
    {
        public string Password { get; set; }
        public string[] FileNames { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void enc_choose_file_button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // just Encypting Image files
            dialog.Multiselect = true;
            dialog.DefaultExt = ".jpg";
            //dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif";
            dialog.Filter = "All files (*.*) | *.*";

            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                FileNames = dialog.FileNames;
            }
        }

        private void enc_button_Click(object sender, RoutedEventArgs e)
        {
            if (FileNames == null || FileNames.Length == 0)
            {
                MessageBox.Show("No file is selected.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            // set the password
            // default password is 'password'
            Password = enc_password.Text;
            if (string.IsNullOrEmpty(Password))
            {
                Password = "password";
            }

            foreach (var file in FileNames)
            {
                Encryption.EncryptFile(file, Password);
            }

            if (enc_save_checkBox.IsChecked != null && enc_save_checkBox.IsChecked.Value)
            {
                var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var fullFileName = Path.Combine(desktopFolder, "Password.txt");
                using (var tw = new StreamWriter(fullFileName, true))
                {
                    tw.WriteLine("password: " + Password);
                    tw.Close();
                }
            }

            MessageBox.Show($"{FileNames.Length} files encrypted!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            FileNames = null;
            Password = null;
        }

        private void dec_choose_file_button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // just Encypting Image files
            dialog.Multiselect = true;
            dialog.Filter = "Encrypted files (*.encrypted) | *.encrypted";
            Nullable<bool> result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                FileNames = dialog.FileNames;
            }
        }

        private void dec_button_Click(object sender, RoutedEventArgs e)
        {
            if (FileNames == null || FileNames.Length == 0)
            {
                MessageBox.Show("No file is selected.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            Password = dec_password.Text;
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("You need to enter the password. The default password is 'password'.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            foreach (var file in FileNames)
            {
                if (!Decryption.DecryptFile(file, Password))
                {
                    // faild to decrypt
                    return;
                }
            }

            MessageBox.Show($"{FileNames.Length} files decrypted!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            FileNames = null;
            Password = null;
        }
    }
}
