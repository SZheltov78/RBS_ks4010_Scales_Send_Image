﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Отправить_картинки_на_весы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent() ;
        }

        public string FileName = "imagePackage.zip"; //файл должен содержать папку image_product 
                                                     //в кторой должны быть картинки {PLU}.png
                                                     
        public string SendStr = "set:data:imagePackage.zip:"; //первая строка, которую нужно отправить

        private void Form1_Load(object sender, EventArgs e)
        {        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nb = 1024;
            long FileSize = 0;
            byte[] buf = new byte[1024];
            TcpClient TC = new TcpClient("10.252.132.198", 1000);//port 1000 
            NetworkStream NS = TC.GetStream();

            FileStream FS = new FileStream(FileName, FileMode.Open);
            FileSize = FS.Length;

            SendStr += FileSize.ToString() + Environment.NewLine; //последний параметр строки - раpмер фала в байтах

            byte[] str = Encoding.Default.GetBytes(SendStr);
            NS.Write(str, 0, str.Length);//сначала отправляется строка 

            do //потом файл
            {
                nb = FS.Read(buf, 0, 1024);
                NS.Write(buf, 0, nb);
            }
            while (nb == 1024);
        }
    }
}
