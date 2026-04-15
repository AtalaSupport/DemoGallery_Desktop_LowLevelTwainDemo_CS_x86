using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atalasoft.Twain;

namespace LowLevelTwainDemo
{
    public partial class SetCapabilityValueDialog : Form
    {
        public SetCapabilityValueDialog(DeviceCapability capability, object[] array)
        {
            InitializeComponent();

            this.lblCapability.Text = capability.ToString();
            this.cboValues.Items.AddRange(array);
            this.cboValues.SelectedIndex = 0;
        }

        public object NewValue
        {
            get { return cboValues.SelectedItem; }
        }
    }
}

