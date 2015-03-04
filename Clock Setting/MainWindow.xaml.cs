using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace Clock_Setting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SerialPort MyPort = new SerialPort();

        System.DateTime Now = System.DateTime.Now;


        public MainWindow()
        {
            InitializeComponent();
            Now = System.DateTime.Now;
            TimeDisplay.Text = Now.ToLongTimeString();
            TimeStampDisplay.Text = ToUnixTimestamp(Now).ToString(); 
            COMPortList.ItemsSource = SerialPort.GetPortNames();
        }


        long ToUnixTimestamp(System.DateTime dt)
        {
            DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
            return (dt.Ticks - unixRef.Ticks) / 10000000;
        }

        private void COMPortRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            COMPortList.ItemsSource = SerialPort.GetPortNames();

        }

        private void COMPortSelected(object sender, SelectionChangedEventArgs e)
        {
            if (!MyPort.IsOpen)
            {
                MyPort.PortName = COMPortList.SelectedItem.ToString();
                try
                {

                    MyPort.Open();
                }
                catch (Exception)
                {
                    Status.Text = "Unable to connect";
                    Status.Background = new SolidColorBrush(Colors.Red);
                }
                if (MyPort.IsOpen)
                {
                    Status.Text = "Successfully connected to : " + MyPort.PortName;
                }
            }
        }

        private void Disctonnect_Click(object sender, RoutedEventArgs e)
        {
            if (MyPort.IsOpen)
            {
                MyPort.Close();
            }
            Status.Text = "Disconnected";
        
        }

        private void SendTimeButton_Click(object sender, RoutedEventArgs e)
        {
            Now = System.DateTime.Now;
            TimeDisplay.Text = Now.ToLongTimeString();
            TimeStampDisplay.Text = ToUnixTimestamp(Now).ToString();
            if (MyPort.IsOpen)
            {
                MyPort.DiscardInBuffer();
                MyPort.Write('T' + (ToUnixTimestamp(Now)+2).ToString());
                Status.Text = "Time data sent to Qlock";
            }
            else
            {
                Status.Text = "Please connect to Qlock first.";
                Status.Background = new SolidColorBrush(Colors.Red);
            }

        }



    }
}
