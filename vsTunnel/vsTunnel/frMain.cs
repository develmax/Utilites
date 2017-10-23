using System;
using System.Threading;
using System.Windows.Forms;

namespace vsTunnel
{
    public partial class frMain : Form
    {
        private bool isStarted = false;
        private Logic logic = null;

        public frMain()
        {
            InitializeComponent();
        }

        

        private void frMain_Load(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void Stop()
        {
            isStarted = false;
            logic = null;
            UpdateView();
        }

        private void UpdateUI(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    action();
                });
                return;
            }
            action();
        }

        private void Log(string message)
        {
            var line = DateTime.Now.ToShortTimeString() + "> " + message;
            if (string.IsNullOrEmpty(txtLog.Text))
                txtLog.Text = line;
            else
                txtLog.Text = txtLog.Text + System.Environment.NewLine + line;

            if (cbScrollLog.Checked)
            {
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.ScrollToCaret();
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            isStarted = true;
            UpdateView();

            var settings = new Logic.Settings()
            {
                ip = txtIPAddress.Text,
                portVS = txtPortFromVS.Text,
                portDebugger = txtPortToRD.Text,
                useSessionsDirectory = rbSession.Checked,
                sessionsDirectory = txtSessionsDirectory.Text,
                vsToRDDirectory = txtVSToRDDirectory.Text,
                rdToVSDirectory = txtRDToVSDirectory.Text,
                watchDirectory = chWatchDirectory.Checked
            };
            
            logic = new Logic(() =>
            {
                UpdateUI(Stop);
            },
            message =>
            {
                UpdateUI(() => Log(message));
            });

            var isWaitVS = rbListenFromVS.Checked;

            if(isWaitVS)
                new Thread(() => logic.StartWaitFromVS(settings))
                {
                    IsBackground = true
                }.Start();
            else
                new Thread(() => logic.StartWaitToRD(settings)){
                    IsBackground = true
                }.Start();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            logic.Stop();
            logic = null;

            isStarted = false;
            UpdateView();
        }

        private void UpdateView()
        {
            gbDirectoryMode.Enabled = !isStarted;
            gbMode.Enabled = !isStarted;

            txtIPAddress.Enabled = !isStarted;

            txtPortFromVS.Enabled = !isStarted && rbListenFromVS.Checked;
            txtPortToRD.Enabled = !isStarted && !rbListenFromVS.Checked;

            txtSessionsDirectory.Enabled = !isStarted && rbSession.Checked;
            txtVSToRDDirectory.Enabled = !isStarted && !rbSession.Checked;
            txtRDToVSDirectory.Enabled = !isStarted && !rbSession.Checked;

            btStart.Enabled = !isStarted;
            btStop.Enabled = isStarted;

            Text = string.Format("vsTunel ({0}){1}", 
                (rbListenFromVS.Checked ? rbListenFromVS.Text : rbConnectToRD.Text), 
                (isStarted ? ": Started" : ""));
        }
        
        private void rbListenFromVS_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void rbConnectToRD_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void rbSession_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void rbSeparately_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void btClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Lines = new string[0];
        }
    }
}
