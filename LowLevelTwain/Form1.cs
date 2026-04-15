using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atalasoft.Twain;
using System.Security.Permissions;
using System.IO;

namespace LowLevelTwainDemo
{
    public partial class Form1 : Form, IMessageFilter
    {
        private TwainController _controller;
        private TwainIdentity _device;
        private TwainUserInterface _userInterface;
        private bool _usingTwain2;
        private string _fileTransferName;
        private SourceImageFormat _fileTransferFormat;
        private List<SourceImageFormat> _fileTransferFormats;
        
        public Form1()
        {
            InitializeComponent();
        }

        #region Form Events

        private void Form1_Load(object sender, EventArgs e)
        {
            _controller = new TwainController(this, CreateApplicationIdentity());
            _controller.AcquireCanceled += new EventHandler(_controller_AcquireCanceled);
            _controller.AcquireFinished += new EventHandler(_controller_AcquireFinished);
            _controller.ImageAcquired += new ImageAcquiredEventHandler(_controller_ImageAcquired);
            _controller.FileTransfer += new FileTransferEventHandler(_controller_FileTransfer);
            _controller.MemoryFileTransfer += new MemoryFileTransferEventHandler(_controller_MemoryFileTransfer);
            _controller.TwainException += new AsynchronousExceptionEventHandler(_controller_TwainException);

            this.statusVersion.Text = GetDotTwainVersion();
            UpdateStepsMenu();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDownTwain();
        }

        #endregion

        #region DotTwain Events

        void _controller_TwainException(object sender, AsynchronousExceptionEventArgs e)
        {
            ShutDownTwain();
            UpdateStatusInformation("Shut Down");
            MessageBox.Show(e.Exception.Message, "TwainException", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void _controller_FileTransfer(object sender, FileTransferEventArgs e)
        {
            e.FileName = _fileTransferName;
            e.FileFormat = _fileTransferFormat;
        }

        void _controller_MemoryFileTransfer(object sender, MemoryFileTransferEventArgs e)
        {
            if (e.OutputStream == null)
            {
                _fileTransferName = Path.GetTempFileName();
                e.OutputStream = new FileStream(_fileTransferName, FileMode.Create, FileAccess.Write);
            }
            else
            {
                e.OutputStream.Close();
                this.picImage.Image = new Bitmap(_fileTransferName);
                File.Delete(_fileTransferName);
                UpdateStatusInformation("Memory File Acquired");
            }
        }

        void _controller_ImageAcquired(object sender, AcquireEventArgs e)
        {
            if (e.Image != null)
                this.picImage.Image = e.Image;
            else if (e.FileName != null)
                this.picImage.Image = new Bitmap(e.FileName);
        }

        void _controller_AcquireFinished(object sender, EventArgs e)
        {
            UpdateStatusInformation("Acquire Finished");
            DisableInterface();
        }

        void _controller_AcquireCanceled(object sender, EventArgs e)
        {
            UpdateStatusInformation("Acquire Canceled");
        }

        #endregion

        #region Menu Events

        private void openSourceManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TwainReturnCode ret = _controller.OpenSourceManager();
                if (ret == TwainReturnCode.TWRC_SUCCESS) LoadDeviceList();
                UpdateStatusInformation(ret.ToString());
                UpdateStepsMenu();
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message, "TwainException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeSourceManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TwainReturnCode ret = _controller.CloseSourceManager();
                if (ret == TwainReturnCode.TWRC_SUCCESS) ClearDeviceList();
                UpdateStatusInformation(ret.ToString());
                UpdateStepsMenu();
            }
            catch (TwainException ex)
            {
                MessageBox.Show("TwainException:\r\n\r\n" + ex.Message);
            }
        }

        private void openSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TwainReturnCode ret = _controller.OpenSource(_device);
                if (ret == TwainReturnCode.TWRC_SUCCESS)
                {
                    // Starting with TWAIN 2.0, use a callback instead of the Window message pump.
                    _usingTwain2 = (_controller.ApplicationIdentity.SupportedGroups & TwainDataGroupFlags.DF_DSM2) == TwainDataGroupFlags.DF_DSM2;
                    if (_usingTwain2)
                        _controller.RegisterTwainCallback(new TwainDeviceCallbackEventHandler(OnTwainCallback));
                    else
                        Application.AddMessageFilter(this);

                    FillCapabilityMenus();
                }

                UpdateStatusInformation(ret.ToString());
                UpdateStepsMenu();
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message, "TwainException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TwainReturnCode ret = _controller.CloseSource();
                if (ret == TwainReturnCode.TWRC_SUCCESS)
                {
                    // The TWAIN callback is automatically removed when CloseSource is called,
                    // so we only need to remove the message filter when a callback is not used.
                    if ((_controller.ApplicationIdentity.SupportedGroups & TwainDataGroupFlags.DF_DSM2) != TwainDataGroupFlags.DF_DSM2)
                        Application.RemoveMessageFilter(this);

                    ClearCapabilityMenus();
                }
                UpdateStatusInformation(ret.ToString());
                UpdateStepsMenu();
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message, "TwainException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableInterface(TwainTransferMethod.TWSX_NATIVE);
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableInterface(TwainTransferMethod.TWSX_MEMORY);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = GetFileFilter();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _fileTransferName = dlg.FileName;
                    _fileTransferFormat = _fileTransferFormats[dlg.FilterIndex - 1];
                    EnableInterface(TwainTransferMethod.TWSX_FILE);
                }
            }
        }

        private void memoryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableInterface(TwainTransferMethod.TWSX_MEMFILE);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnDeviceMenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (sender == null) return;

            // Uncheck the previous device.
            int index = this.deviceToolStripMenuItem.DropDownItems.IndexOfKey(_device.ID.ToString());
            ToolStripMenuItem previousItem = (ToolStripMenuItem)this.deviceToolStripMenuItem.DropDownItems[index];
            previousItem.Checked = false;

            item.Checked = true;
            _device = item.Tag as TwainIdentity;
        }

        private void OnGetCapability(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;

            DeviceCapability cap = (DeviceCapability)item.Tag;
            Type t = _controller.GetCapabilityType(cap);

            bool bval = false;
            int ival = 0;
            float fval = 0;
            RectangleF rval = RectangleF.Empty;
            string result = "";

            switch (t.Name)
            {
                case "Int32":
                    _controller.GetCapabilityValue(cap, out ival);
                    result = ival.ToString();
                    break;
                case "Boolean":
                    _controller.GetCapabilityValue(cap, out bval);
                    result = bval.ToString();
                    break;
                case "Single":
                    _controller.GetCapabilityValue(cap, out fval);
                    result = fval.ToString();
                    break;
                case "String":
                    _controller.GetCapabilityValue(cap, out result);
                    break;
                case "RectangleF":
                    _controller.GetCapabilityValue(cap, out rval);
                    result = rval.ToString();
                    break;
            }

            UpdateStatusInformation(cap.ToString() + " = " + result);
        }

        private void OnSetCapability(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;

            DeviceCapability cap = (DeviceCapability)item.Tag;

            object[] array;
            if (_controller.GetCapabilityArray(cap, out array) == TwainReturnCode.TWRC_SUCCESS)
            {
                Type valueType = _controller.GetCapabilityType(cap);
                ConvertTypeFromTwainToDotNet(valueType, array);

                using (SetCapabilityValueDialog dlg = new SetCapabilityValueDialog(cap, array))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        switch (valueType.Name)
                        {
                            case "Boolean":
                                _controller.SetCapabilityValue(cap, (bool)dlg.NewValue);
                                break;
                            case "Int32":
                                _controller.SetCapabilityValue(cap, (int)dlg.NewValue);
                                break;
                            case "Single":
                                _controller.SetCapabilityValue(cap, (float)dlg.NewValue);
                                break;
                            case "String":
                                _controller.SetCapabilityValue(cap, (string)dlg.NewValue);
                                break;
                            case "RectangleF":
                                _controller.SetCapabilityValue(cap, (RectangleF)dlg.NewValue);
                                break;
                        }

                        UpdateStatusInformation(cap.ToString() + " set to " + dlg.NewValue.ToString());
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private string GetDotTwainVersion()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load("Atalasoft.DotTwain");
            return "DotTwain " + asm.GetName().Version.ToString();
        }

        private TwainIdentity CreateApplicationIdentity()
        {
            TwainIdentity id = new TwainIdentity("Low Level Demo", "DotTwain", "Atalasoft", 8, 0, LanguageType.EnglishUsa, CountryCode.Usa, "DotTwain Low Level Demo");

            // We want to use TWAIN 2.0 if it exists.
            id.ProtocolMajor = 2;
            id.ProtocolMinor = 0;
            id.SupportedGroups = TwainDataGroupFlags.DG_CONTROL | TwainDataGroupFlags.DG_IMAGE | TwainDataGroupFlags.DF_APP2;

            return id;
        }

        private void ShutDownTwain()
        {
            // Do our best to back out of the current TWAIN state.
            if (_controller != null)
            {
                if (_controller.State > TwainState.SourceEnabled)
                    _controller.GetPendingTransferCount(TwainTriplet.PendingXfersReset);

                if (_controller.State == TwainState.SourceEnabled)
                    _controller.SendCommand(TwainTriplet.UserInterfaceDisableDS, _device, _userInterface);

                if (_controller.State == TwainState.SourceOpen)
                    _controller.CloseSource();

                if (_controller.State == TwainState.SourceManagerOpen)
                    _controller.CloseSourceManager();

                _controller.Dispose();
            }
        }

        private void UpdateStatusInformation(string information)
        {
            this.statusInformation.Text = information;
            this.statusState.Text = (_controller == null ? "" : _controller.State.ToString());
        }

        private void LoadDeviceList()
        {
            try
            {
                TwainIdentity[] items = _controller.GetTwainDevices();
                if (items == null || items.Length == 0)
                    UpdateStatusInformation("No devices were found.");
                else
                {
                    // Get the default device so we can place a checkmark on its menu.
                    _device = _controller.GetDefaultDevice();

                    foreach (TwainIdentity id in items)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(id.ProductName, null, new EventHandler(OnDeviceMenuClick), id.ID.ToString());
                        item.Tag = id;
                        if (id.ProductName == _device.ProductName) item.Checked = true; 
                        this.deviceToolStripMenuItem.DropDownItems.Add(item);
                    }
                }
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message, "TwainException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDeviceList()
        {
            this.deviceToolStripMenuItem.DropDownItems.Clear();
        }

        private void FillCapabilityMenus()
        {
            // Load the supported capabilities into the menu.
            object[] array;
            if (_controller.GetCapabilityArray(DeviceCapability.CAP_SUPPORTEDCAPS, out array) == TwainReturnCode.TWRC_SUCCESS)
            {
                foreach (object obj in array)
                {
                    // We will ignore custom capabilities.
                    ushort val = (ushort)obj;
                    if (val < 0x8000)
                        AddCapabilityToMenus((DeviceCapability)val);
                }
            }
            else
                MessageBox.Show("The driver failed when asking for its supported capabilities.", "CAP_SUPPORTEDCAPS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void AddCapabilityToMenus(DeviceCapability capability)
        {
            bool canGet = false;
            bool canSet = false;

            TwainCapability cap = new TwainCapability(capability, TwainContainer.TWON_DONTCARE16, IntPtr.Zero);
            TwainReturnCode ret = _controller.SendCommand(TwainTriplet.CapabilityQuerySupport, _device, cap);
            if (ret == TwainReturnCode.TWRC_SUCCESS)
            {
                QueryOperation ops = QueryOperation.TWQC_NONE;
                using (TwainMemory memory = _controller.CreateTwainMemory(cap.Data))
                {
                    ops = (QueryOperation)_controller.ReadTwainContainerData(cap.ContainerType, memory);
                }

                canGet = ((ops & QueryOperation.TWQC_GET) == QueryOperation.TWQC_GET);
                canSet = ((ops & QueryOperation.TWQC_SET) == QueryOperation.TWQC_SET);

                // Disable any transfer methods not supported.
                if (canGet && capability == DeviceCapability.ICAP_XFERMECH)
                {
                    object[] getArray;
                    ret = _controller.GetCapabilityArray(capability, out getArray);
                    UpdateTransferMenus(getArray);
                }

                // Add the menu items.
                if (canGet)
                {
                    ToolStripMenuItem getItem = new ToolStripMenuItem(capability.ToString(), null, new EventHandler(OnGetCapability));
                    getItem.Tag = capability;
                    this.getCapabilityToolStripMenuItem.DropDownItems.Add(getItem);
                }

                if (canSet)
                {
                    ToolStripMenuItem setItem = new ToolStripMenuItem(capability.ToString(), null, new EventHandler(OnSetCapability));
                    setItem.Tag = capability;
                    this.setCapabilityToolStripMenuItem.DropDownItems.Add(setItem);
                }
            }
        }

        private void ClearCapabilityMenus()
        {
            this.getCapabilityToolStripMenuItem.DropDownItems.Clear();
            this.setCapabilityToolStripMenuItem.DropDownItems.Clear();
        }

        private void UpdateTransferMenus(object[] values)
        {
            this.nativeToolStripMenuItem.Enabled = false;
            this.memoryFileToolStripMenuItem.Enabled = false;
            this.memoryToolStripMenuItem.Enabled = false;
            this.fileToolStripMenuItem.Enabled = false;

            foreach (object obj in values)
            {
                TwainTransferMethod tm = (TwainTransferMethod)(ushort)obj;
                switch (tm)
                {
                    case TwainTransferMethod.TWSX_NATIVE:
                        this.nativeToolStripMenuItem.Enabled = true;
                        break;
                    case TwainTransferMethod.TWSX_FILE:
                    case TwainTransferMethod.TWSX_FILE2:
                        this.fileToolStripMenuItem.Enabled = true;
                        break;
                    case TwainTransferMethod.TWSX_MEMFILE:
                        this.memoryFileToolStripMenuItem.Enabled = true;
                        break;
                    case TwainTransferMethod.TWSX_MEMORY:
                        this.memoryToolStripMenuItem.Enabled = true;
                        break;
                }
            }
        }

        private void UpdateStepsMenu()
        {
            switch (_controller.State)
            {
                case TwainState.Unloaded:
                case TwainState.Loaded:
                    this.openSourceManagerToolStripMenuItem.Enabled = true;
                    this.closeSourceManagerToolStripMenuItem.Enabled = false;
                    this.deviceToolStripMenuItem.Enabled = false;
                    this.openSourceToolStripMenuItem.Enabled = false;
                    this.closeSourceToolStripMenuItem.Enabled = false;
                    this.getCapabilityToolStripMenuItem.Enabled = false;
                    this.setCapabilityToolStripMenuItem.Enabled = false;
                    this.acquireToolStripMenuItem.Enabled = false;
                    break;
                case TwainState.SourceManagerOpen:
                    this.openSourceManagerToolStripMenuItem.Enabled = false;
                    this.closeSourceManagerToolStripMenuItem.Enabled = true;
                    this.deviceToolStripMenuItem.Enabled = true;
                    this.openSourceToolStripMenuItem.Enabled = (_device != null);
                    this.closeSourceToolStripMenuItem.Enabled = false;
                    this.getCapabilityToolStripMenuItem.Enabled = false;
                    this.setCapabilityToolStripMenuItem.Enabled = false;
                    this.acquireToolStripMenuItem.Enabled = false;
                    break;
                case TwainState.SourceOpen:
                    this.openSourceManagerToolStripMenuItem.Enabled = false;
                    this.closeSourceManagerToolStripMenuItem.Enabled = false;
                    this.deviceToolStripMenuItem.Enabled = false;
                    this.openSourceToolStripMenuItem.Enabled = false;
                    this.closeSourceToolStripMenuItem.Enabled = true;
                    this.getCapabilityToolStripMenuItem.Enabled = true;
                    this.setCapabilityToolStripMenuItem.Enabled = true;
                    this.acquireToolStripMenuItem.Enabled = true;
                    break;
                case TwainState.SourceEnabled:
                case TwainState.ReadyToTransfer:
                case TwainState.Transferring:
                    this.closeSourceToolStripMenuItem.Enabled = false;
                    this.getCapabilityToolStripMenuItem.Enabled = false;
                    this.setCapabilityToolStripMenuItem.Enabled = false;
                    this.acquireToolStripMenuItem.Enabled = false;
                    break;
            }
        }

        private void EnableInterface(TwainTransferMethod method)
        {
            if (_controller.SetCapabilityValue(DeviceCapability.ICAP_XFERMECH, (int)method) == TwainReturnCode.TWRC_SUCCESS)
            {
                _userInterface = new TwainUserInterface(true, true, this.Handle);
                TwainReturnCode ret = _controller.SendCommand(TwainTriplet.UserInterfaceEnableDS, _device, _userInterface);
                UpdateStepsMenu();
                UpdateStatusInformation(ret.ToString());
            }
        }

        private void DisableInterface()
        {
            // Close the device interface.
            TwainReturnCode ret = _controller.SendCommand(TwainTriplet.UserInterfaceDisableDS, _device, _userInterface);
            _userInterface = null;
            UpdateStepsMenu();
            UpdateStatusInformation(ret.ToString());
        }

        private string GetFileFilter()
        {
            object[] array;
            if (_controller.GetCapabilityArray(DeviceCapability.ICAP_IMAGEFILEFORMAT, out array) == TwainReturnCode.TWRC_SUCCESS)
            {
                string pipe = "";
                _fileTransferFormats = new List<SourceImageFormat>();

                StringBuilder builder = new StringBuilder();
                foreach (object obj in array)
                {
                    switch ((SourceImageFormat)(ushort)obj)
                    {
                        case SourceImageFormat.Bmp:
                            builder.Append(pipe + "Bitmap (*.bmp)|*.bmp");
                            _fileTransferFormats.Add(SourceImageFormat.Bmp);
                            break;
                        case SourceImageFormat.Jfif:
                            builder.Append(pipe + "JPEG (*.jpg)|*.jpg");
                            _fileTransferFormats.Add(SourceImageFormat.Jfif);
                            break;
                        case SourceImageFormat.Jpeg2000:
                            builder.Append(pipe + "JPEG 2000 (*.jp2)|*.jp2");
                            _fileTransferFormats.Add(SourceImageFormat.Jpeg2000);
                            break;
                        case SourceImageFormat.Pdf:
                            builder.Append(pipe + "Adobe PDF (*.pdf)|*.pdf");
                            _fileTransferFormats.Add(SourceImageFormat.Pdf);
                            break;
                        case SourceImageFormat.Png:
                            builder.Append(pipe + "PNG (*.png)|*.png");
                            _fileTransferFormats.Add(SourceImageFormat.Png);
                            break;
                        case SourceImageFormat.Tiff:
                            builder.Append(pipe + "TIFF (*.tif)|*.tif");
                            _fileTransferFormats.Add(SourceImageFormat.Tiff);
                            break;
                    }

                    if (pipe.Length == 0 && builder.Length > 0) 
                        pipe = "|";
                }

                return builder.ToString();
            }

            return "";
        }

        private void ConvertTypeFromTwainToDotNet(Type netType, object[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                array[i] = Convert.ChangeType(array[i], netType);
            }
        }

        #endregion

        #region TWAIN Message Processing

        List<TwainEventMessage> _callbackMessages = new List<TwainEventMessage>();
        Timer _callbackTimer;

        void _callbackTimer_Tick(object sender, EventArgs e)
        {
            _callbackTimer.Stop();
            TwainEventMessage m;

            lock (_callbackMessages)
            {
                if (_callbackMessages.Count == 0)
                {
                    _callbackTimer.Dispose();
                    return;
                }

                m = _callbackMessages[0];
                _callbackMessages.RemoveAt(0);
            }

            ProcessSourceMessage(m);
        }

        private ushort OnTwainCallback(TwainController controller, TwainDeviceCallbackEventArgs e)
        {
            TwainEventMessage message = (TwainEventMessage)e.Message;
            switch (message)
            {
                case TwainEventMessage.MSG_NULL:
                    break;
                case TwainEventMessage.MSG_CLOSEDSOK:
                case TwainEventMessage.MSG_CLOSEDSREQ:
                case TwainEventMessage.MSG_DEVICEEVENT:
                case TwainEventMessage.MSG_XFERREADY:
                    // NOTE: The timer is used because we need the callback
                    //       to return right away.
                    lock (_callbackMessages)
                    {
                        _callbackMessages.Add(message);
                    }

                    if (_callbackTimer == null)
                    {
                        _callbackTimer = new Timer();
                        _callbackTimer.Tick += new EventHandler(_callbackTimer_Tick);
                        _callbackTimer.Interval = 100;
                    }

                    _callbackTimer.Start();
                    break;
                default:
                    return 1; // TWRC_FAILURE
            }

            return 0; // TWRC_SUCCESS
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public bool PreFilterMessage(ref Message m)
        {
            try
            {
                TwainEventMessage msg;
                TwainReturnCode ret = _controller.ProcessEvent(m, out msg);

                if (ret == TwainReturnCode.TWRC_DSEVENT)
                {
                    ProcessSourceMessage(msg);
                    return true;
                }
            }
            catch
            {
                // Try forcing the connection close to prevent driver lockups.
                ShutDownTwain();
            }

            return false;
        }

        private void ProcessSourceMessage(TwainEventMessage msg)
        {
            switch (msg)
            {
                case TwainEventMessage.MSG_NULL:
                    break;
                case TwainEventMessage.MSG_CLOSEDSOK:
                    // This fires when the interface is shown without scanning
                    // and the OK button is clicked.
                    DisableInterface();
                    break;
                case TwainEventMessage.MSG_CLOSEDSREQ:
                    // This fires when the interface Cancel button is clicked.
                    DisableInterface();

                    UpdateStatusInformation("Acquire Canceled");
                    break;
                case TwainEventMessage.MSG_DEVICEEVENT:
                    UpdateStatusInformation("Device Event");
                    break;
                case TwainEventMessage.MSG_XFERREADY:
                    // If we wanted to scan in a separate thread, 
                    // you could run InitiateDataTransfer in its
                    // own thread.
                    _controller.InitiateDataTransfer();
                    break;
            }
        }

        #endregion

        private void helpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AtalaDemos.AboutBox.About aboutBox = new AtalaDemos.AboutBox.About("About Atalasoft Low Level TWAIN Demo", "Low Level TWAIN Demo");
            aboutBox.Description = "This demo is a sort of 'deconstruction of scanning using DotTwain'.  It isn't really meant to provide an example of making user-oriented DotTwain scanning apps (for that, see Scan to File Demo and/or Acquisition Demo).  Instead, this demo provides direct access to the various stages/steps within the process of scanning.\r\n\r\n" +
                                   "Unlike the other aforementioned demos which use our Acquisition object, this uses our TwainController class to provide the lower-level access to TWAIN.  You Open the Source Manager, Select your source, Open communication with the source get/set capabilities, then perform the acquisition.  It populates the get/set capabilities with values obtained from querying the current device.\r\n\r\n" +
                                   "It can also be a very useful diagnostic tool to show what various capability settings are available, and to see how the TWAIN communication process is working between a given scanner and the TWAIN controller.";
            aboutBox.ShowDialog();
        }
    }
}

