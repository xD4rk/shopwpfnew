using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Schema;
using MySql.Data;


namespace SDKSamples.ImageSample
{
    public sealed partial class MainWindow : Window
    {

        private List<Photo> ImgList;
        public PhotoCollection Photos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPhotoClick(object sender, RoutedEventArgs e)
        {
            PhotoView pvWindow = new PhotoView();
            pvWindow.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;
            pvWindow.Show();
        }

        private void editPhoto(object sender, RoutedEventArgs e)
        {
            PhotoView pvWindow = new PhotoView();
            pvWindow.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;
            pvWindow.Show();
        }

        private void OnImagesDirChangeClick(object sender, RoutedEventArgs e)
        {
            this.Photos.Path = ImagesDir.Text;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ImagesDir.Text = Environment.CurrentDirectory + "\\images";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            string[] fileEntries = Directory.GetFiles("C:\\xampp\\images\\stories\\virtuemart\\product\\50x50");
            ImgList = new List<Photo>();
            foreach (string fileName in fileEntries)
            {
                if (fileName.EndsWith("jpg"))
                {
                    ImgList.Add(new Photo(fileName, fileName,1));
                }
                
            }
            foreach (var img in ImgList)
            {
                Photos.Add(img);
            }
               
        }

        private void PhotosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var y = PhotosListBox.SelectedItem as Photo;
            lblID.Content = y._id;
            lblName.Content = y._name;
        }
    }
}