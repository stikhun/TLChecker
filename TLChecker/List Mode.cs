/****************************** ghost1372.github.io ******************************\
*	Module Name:	List Mode.cs
*	Project:		TLChecker
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 9, 24, 10:03 ب.ظ
*
***********************************************************************************/

using System;
using System.Windows.Forms;

namespace TLChecker
{
    public partial class List_Mode : Form
    {
        public List_Mode()
        {
            InitializeComponent();
        }

        private void List_Mode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileLines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                foreach (var singleLine in fileLines)
                {
                    listBox1.Items.Add(singleLine);
                }
            }
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            var client = new TLSharp.Core.TelegramClient(BuildVar.apiId, BuildVar.apiHash);
            await client.ConnectAsync();
            if (client.IsConnected)
            {
                try
                {
                    btnCheck.Enabled = false;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        var number = listBox1.Items[i].ToString();
                        var normalizedNumber = number.StartsWith("+") ? number.Substring(1) : number;

                        var result = await client.IsPhoneRegisteredAsync(normalizedNumber);
                        if (result)
                        {
                            listBox2.Items.Add(number);
                            listBox1.Items.RemoveAt(i);
                        }
                    }
                    btnCheck.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                WriteText(listBox2.Items[i].ToString());
            }
            btnSave.Enabled = true;
            MessageBox.Show(string.Format("Saved\n{0}", Application.StartupPath + @"\" + BuildVar.Save_Location));
        }

        private static void WriteText(string Text)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(BuildVar.Save_Location, true))
            {
                file.WriteLine(Text);
            }
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            btnSave2.Enabled = false;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                WriteText(listBox1.Items[i].ToString());
            }
            btnSave2.Enabled = true;
            MessageBox.Show(string.Format("Saved\n{0}", Application.StartupPath + @"\" + BuildVar.Save_Location));
        }
    }
}