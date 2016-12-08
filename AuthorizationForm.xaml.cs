using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VkAnalyzePolls
{
    /// <summary>
    /// Interaction logic for AuthorizationForm.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var urlToken =
                string.Format(
                    "https://oauth.vk.com/authorize?client_id={0}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=groups&response_type=token&v=5.60", Settings.ApplicationId);
            GetToken.Navigate(urlToken);
        }

        private void GetToken_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (GetToken.Source.ToString().IndexOf("access_token=") != 0)
            {
                GetUserToken();
            }
        }

        private void GetUserToken()
        {
            char[] paramDelimetr = {'=', '&'};
            string[] url = GetToken.Source.ToString().Split(paramDelimetr);
            if (url.Length == 6 && url[2] == "expires_in")
            {
                //token; userId; expires in sec
                File.WriteAllText("UserInf.txt", string.Format("{0} {1} {2}", url[1], url[5], url[3]));
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
