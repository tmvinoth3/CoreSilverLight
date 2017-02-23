using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;

namespace corepluralsight
{
    public partial class MainPage : UserControl
    {
        Perosn p = new Perosn { GivenName="Ram",SurName="Raam",Age=25.3};
        NotificationWindow nw;

        int runcount = 0;
        public MainPage()
        {
           
            InitializeComponent();

            this.DataContext = p;

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists("a.bin"))
                {
                    using (var stm = store.OpenFile("a.bin", FileMode.Open, FileAccess.Read))
                    using (var n = new BinaryReader(stm))
                    {
                        runcount = n.ReadInt32();
                    }
                }

                runcount++;

                using (var stm = store.OpenFile("a.bin", FileMode.Create, FileAccess.Write))
                using (var n = new BinaryWriter(stm))
                {
                    n.Write(runcount);
                }
            }
            textBox1.Text = runcount.ToString();
            if (Application.Current.IsRunningOutOfBrowser)
            {
                nw = new NotificationWindow();
            }
        

            myellipse.MouseLeftButtonDown += new MouseButtonEventHandler(myellipse_leftbuttondown);
        }

        void layout_fullScreen(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                button1.Content = "Return to normal";
            }
            else
            {
                button1.Content = "Enter Fullscreen";
            }
        }

        void myellipse_leftbuttondown(object sender,MouseButtonEventArgs e)
        {
            myellipse.Fill =new  SolidColorBrush(Colors.Green);
        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {
            myellipse.Fill = new SolidColorBrush(Colors.Red);
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            myellipse.Fill = new SolidColorBrush(Colors.Yellow);
        }

        private void myellipse_leftbuttonup(object sender, MouseButtonEventArgs e)
        {
            myellipse.Fill = new SolidColorBrush(Colors.Cyan);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var content = Application.Current.Host.Content;
            content.IsFullScreen = !content.IsFullScreen;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (nw != null)
            {
                nw.Content = new SilverlightControl1();
                nw.Show(3000);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var save = new SaveFileDialog();
            save.Filter = "Text Files (*.txt) | *.txt | All Files (*.*) | *.*";
            save.DefaultExt = ".txt";
            if (save.ShowDialog() == true)
            {
                using (Stream saveStream = save.OpenFile())
                using (var w = new StreamWriter(saveStream))
                {
                    w.Write(textBox1.Text);
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            var save = new OpenFileDialog();
            if (save.ShowDialog() == true)
            {
                using (Stream openStream = save.File.OpenRead())
                using (var r = new StreamReader(openStream))
                {
                    textBox1.Text = r.ReadToEnd();
                }
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    //Get isolated storage
            //    using (IsolatedStorageFile istore = new IsolatedStorageFile())
            //    {
            //        //create File
            //        using (IsolatedStorageFileStream ifs = new IsolatedStorageFileStream("a.txt", FileMode.Create, istore))
            //        {
            //            //write file
            //            using (StreamWriter sw = new StreamWriter(ifs))
            //            {
            //                sw.WriteLine(textBox1.Text +" Wrote to isolated storage - " + DateTime.Now.ToString());
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }



    }
}
