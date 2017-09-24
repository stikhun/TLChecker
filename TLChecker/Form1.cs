/****************************** ghost1372.github.io ******************************\
*	Module Name:	Form1.cs
*	Project:		TLChecker
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 9, 24, 09:51 ب.ظ
*
***********************************************************************************/

using System;
using System.Windows.Forms;

namespace TLChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Select_Mode().ShowDialog();
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
                    var number = txtNumber.Text;
                    // check mikonim age + dasht bayad ouno hazf konim
                    // age dasht ke hazfesh mikonim, age nadasht ham bikhialesh mishim
                    var normalizedNumber = number.StartsWith("+") ? number.Substring(1) : number;

                    var result = await client.IsPhoneRegisteredAsync(normalizedNumber);

                    MessageBox.Show((result ? "This Number Registered."
                                : "This Number Not Registered."));
                    btnCheck.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}