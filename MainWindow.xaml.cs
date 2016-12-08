using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VkAnalyzePolls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VkApi _vkApi;
        private string _token;  //Токен, использующийся при запросах
        private string _userId;
        private Dictionary<string, string> _response;  //Ответ на запросы


        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationForm authorizationForm = new AuthorizationForm();
            authorizationForm.ShowDialog();
        }

        private void BtnGetInformation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader streamReader = new StreamReader("UserInf.txt");
                _token = streamReader.ReadLine().Split(' ')[0];
                streamReader.Close();

                _vkApi = new VkApi(_token);
                string[] prms = { "city", "country"};
                _response = _vkApi.GetInformation(TxtUserId.Text, prms);
                if (_response != null)
                {
                    LblFirstName.Content = _response["first_name"];
                    LblLastName.Content = _response["last_name"];
                    LblCountry.Content = _vkApi.GetCountryById(_response["country"]);
                    LblCity.Content = _vkApi.GetCityById(_response["city"]);
                    BtnLogIn.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BtnLogIn.Visibility = Visibility.Visible;
                }
            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader streamReader = new StreamReader("UserInf.txt");
                string userInfoText = streamReader.ReadLine();
                string[] userInfoParts = new string[3];

                if (!string.IsNullOrEmpty(userInfoText))
                {
                    userInfoParts = userInfoText.Split(' ');
                }

                _token = userInfoParts[0];
                _userId = userInfoParts[1];
                streamReader.Close();

                if (_token != null)
                {
                    _vkApi = new VkApi(_token);
                    string[] Params = {"city", "country"};
                    _response = _vkApi.GetInformation(_userId, Params);
                    if (_response != null)
                    {
                        TxtUserId.Text = _userId;
                        LblFirstName.Content = _response["first_name"];
                        LblLastName.Content = _response["last_name"];
                        LblCountry.Content = _vkApi.GetCountryById(_response["country"]);
                        LblCity.Content = _vkApi.GetCityById(_response["city"]);
                        BtnLogIn.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        BtnLogIn.Visibility = Visibility.Visible;
                    }
                }
            }
            catch
            {
            }
        }
    }
}
