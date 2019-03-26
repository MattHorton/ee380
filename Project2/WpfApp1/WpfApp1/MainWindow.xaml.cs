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
            Double r, l, c, fc, fstop, fpass;
            r = 0;
            l = 0;
            c = 0;
            fc = 0;
            fstop = 0;
            fpass = 0;
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
            if (!txtBoxFstop.Text.Equals("Value"))
            {
                fstop = Double.Parse(txtBoxFstop.Text);
                numArgs++;
            }
            if (!txtBoxFpass.Text.Equals("Value"))
            {
                fpass = Double.Parse(txtBoxFpass.Text);
                numArgs++;
            }
            switch(selectedFilterType)
            {
                case "Lowpass":
                    // do lowpass shit
                    switch (selectedFilterImplementation)
                    {
                        case "RC":
                            //must have either (r and c) or (r and fc) or (c and fc)
                            fc = 1 / (2 * 3.14 * r * c);
                            break;
                        case "RL":
                            //rl must have either (r and l) or (r and fc) or (l and fc)

                            break;
                        default:
                            break;
                    }
                    break;
                case "Highpass":
                    // do highpass shit
                    switch (selectedFilterImplementation)
                    {
                        case "RC":
                            //must have either (r and c) or (r and fc) or (c and fc)
                            break;
                        case "RL":
                            //rl must have either (r and l) or (r and fc) or (l and fc)
                            break;
                        default:
                            break;
                    }
                    break;
                case "Bandpass":
                    // do bandpass shit
                    switch (selectedFilterImplementation)
                    {
                        case "RLC":
                            //big branch of requirements
                            break;
                        default:
                            break;
                    }
                    break;
                case "Bandstop":
                    // do bandstop shit
                    switch (selectedFilterImplementation)
                    {
                        case "RLC":
                            //big branch of requirements
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            txtBoxR.Text = r.ToString();
            txtBoxL.Text = l.ToString();
            txtBoxC.Text = c.ToString();
            txtBoxFc.Text = fc.ToString();
            txtBoxFpass.Text = fpass.ToString();
            txtBoxFstop.Text = fstop.ToString();
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
        }
    }
}
