using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private String selectedFilterType;
        private String selectedFilterImplementation;
        private int numArgs;



        private void MnuBandstop_Click(object sender, RoutedEventArgs e)
        {
            mnuFilter.Header = "Bandstop";
            selectedFilterType = "Bandstop";
        }
        private void MnuBandpass_Click(object sender, RoutedEventArgs e)
        {
            mnuFilter.Header = "Bandpass";
            selectedFilterType = "Bandpass";
        }
        private void MnuHighpass_Click(object sender, RoutedEventArgs e)
        {
            mnuFilter.Header = "Highpass";
            selectedFilterType = "Highpass";
        }
        private void MnuLowpass_Click(object sender, RoutedEventArgs e)
        {
            mnuFilter.Header = "Lowpass";
            selectedFilterType = "Lowpass";
        }
        private void MnuRC_Click(object sender, RoutedEventArgs e)
        {
            mnuRCRLRLC.Header = "RC";
            selectedFilterImplementation = "RC";
        }
        private void MnuRL_Click(object sender, RoutedEventArgs e)
        {
            mnuRCRLRLC.Header = "RL";
            selectedFilterImplementation = "RL";
        }
        private void MnuRLC_Click(object sender, RoutedEventArgs e)
        {
            mnuRCRLRLC.Header = "RLC";
            selectedFilterImplementation = "RLC";
        }








        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Double r, l, c, fc;
            r = 0;
            l = 0;
            c = 0;
            fc = 0;
            String fstop = "";
            String fpass = "";
            if(!txtBoxR.Text.Equals("Value"))
            {
                r = Double.Parse(txtBoxR.Text);
                numArgs++;
            }
            if (!txtBoxL.Text.Equals("Value"))
            {
                l = Double.Parse(txtBoxL.Text);
                numArgs++;
            }
            if (!txtBoxC.Text.Equals("Value"))
            {
                c = Double.Parse(txtBoxC.Text);
                numArgs++;
            }
            if (!txtBoxFc.Text.Equals("Value"))
            {
                fc = Double.Parse(txtBoxFc.Text);
                numArgs++;
            }
            /*if (!txtBoxFstop.Text.Equals("Value"))
            {
                fstop = Double.Parse(txtBoxFstop.Text);
                numArgs++;
            }
            if (!txtBoxFpass.Text.Equals("Value"))
            {
                fpass = Double.Parse(txtBoxFpass.Text);
                numArgs++;
            }*/
            switch(selectedFilterType)
            {
                case "Lowpass":
                    // do lowpass shit
                    if(selectedFilterImplementation == "RC")
                    {
                        fc = 1 / (2 * 3.14 * r * c);
                        lowpassRCimg.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case "Highpass":
                    // do highpass shit
                    if (selectedFilterImplementation == "RL")
                    {
                        fc = r / (2 * 3.14 *l);
                        highpassRLimg.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case "Bandpass":
                    // do bandpass shit
                    if (selectedFilterImplementation == "RLC")
                    {
                        //fc = r / (2 * 3.14 * l);
                        Double w0 = 1 / (Math.Sqrt(l * c)); //natural freq
                        Double Q = (1 / r) * (Math.Sqrt(l / c)); // q factor
                        Double dw = w0 / Q; //bandwidth (rads)
                        Double f2 = (w0 + (dw / 2)) / (2 * 3.14); //upper freq
                        Double f1 = (w0 - (dw / 2)) / (2 * 3.14); //lower freq
                        fpass = f1.ToString() + f2.ToString();
                        fc = w0 / (2 * 3.14);
                        bandpassRLCimg.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                case "Bandstop":
                    // do bandstop shit
                    if (selectedFilterImplementation == "RLC")
                    {
                        Double w0 = 1 / (Math.Sqrt(l * c)); //natural freq
                        Double Q = (1 / r) * (Math.Sqrt(l / c)); // q factor
                        Double dw = w0 / Q; //bandwidth (rads)
                        Double f2 = (w0 + (dw / 2)) / (2 * 3.14); //upper freq
                        Double f1 = (w0 - (dw / 2)) / (2 * 3.14); //lower freq
                        fstop = f1.ToString() + f2.ToString();
                        fc = w0 / (2 * 3.14);
                        bandstopRLCimg.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
            txtBoxR.Text = r.ToString();
            txtBoxL.Text = l.ToString();
            txtBoxC.Text = c.ToString();
            txtBoxFc.Text = fc.ToString();
            txtBoxFpass.Text = fpass;//.ToString();
            txtBoxFstop.Text = fstop;//.ToString();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            numArgs = 0;
            txtBoxR.Text = "Value";
            txtBoxL.Text = "Value";
            txtBoxC.Text = "Value";
            txtBoxFc.Text = "Value";
            txtBoxFpass.Text = "Value";
            txtBoxFstop.Text = "Value";
            mnuFilter.Header = "Filter Type";
            mnuRCRLRLC.Header = "Implementation Type";

            highpassRLimg.Visibility = System.Windows.Visibility.Hidden;
            lowpassRCimg.Visibility = System.Windows.Visibility.Hidden;
            bandpassRLCimg.Visibility = System.Windows.Visibility.Hidden;
            bandstopRLCimg.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
