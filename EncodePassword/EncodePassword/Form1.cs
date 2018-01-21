using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodePassword
{
    public partial class Form1 : Form
    {
       
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        //to get decoded data by clicking "Load" button
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                //open data.txt file
                DataField.Text = decodeText(System.IO.File.ReadAllText("data.txt"), textBox1.Text);
            }
            catch (System.IO.FileNotFoundException)
            {
                // if not found throw an exception
                DataField.Text = "File not found!";
            }

        }

        //to save data and encode to data.txt by clicking "Save" button
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // try to write data.txt file
                System.IO.File.WriteAllText("data.txt", encodeText(DataField.Text, textBox1.Text));
            }
            catch (UnauthorizedAccessException)
            {
                // catch exception if file dir not writable
                DataField.Text = "Directory not writable!";
            }
        }



        // method to encode string , with two arguments (data and key), return encoded data 
        private string encodeText(string data, string key)
        {
            // empty data variable creating 
            string encodedData = "";

            // lopp across every char in string, 
            // k is key counter
            for (int i = 0, k = 0; i < data.Length; i++, k++)
            {
                // if key out of range, set it to 0
                if (k == key.Length) k = 0;
                // convert char to int, add 100 for constant symbol count and add casted to int key char
                encodedData += (int)data[i] + 100 + (int)key[k];
            }
            return encodedData;
        }

        // method to decode string, with two arguments (data for decoding and key), return decoded data 
        private string decodeText(string data, string key)
        {
            // empty data variable creating 
            string decodedData = ""; 

            // lopp across every char in string
            for (int i = 0, k = 0; i < data.Length; i++)
            {
                // if key out of range, set it to 0
                if (k == key.Length) k = 0;
                // takes every third symbol, because one char value is 3 size integer 
                if (i % 3 == 0)
                {
                    // parse 3 symbols as substirng, subtract 100 and current char value
                    int a = Int32.Parse(data.Substring(i, 3)) - 100 - (int)key[k];
                    // add to variable
                    decodedData += (char)a;
                    // increase key counter 
                    k++; 
                }
            }
            return decodedData;
        }
    }
}
