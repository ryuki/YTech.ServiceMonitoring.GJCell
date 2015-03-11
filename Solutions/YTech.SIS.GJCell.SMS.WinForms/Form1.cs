using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.smslib;
using org.smslib.modem;
using System.Threading;

namespace YTech.SIS.GJCell.SMS.WinForms
{
    public partial class Form1 : Form
    {
        Service srv;
        private Comm2IP.Comm2IP com1;
        private IPModemGateway gateway;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FillPorts();

            cboBaudRate.Items.Add("9600");
            cboBaudRate.Items.Add("19200");
            cboBaudRate.Items.Add("38400");
            cboBaudRate.Items.Add("57600");
            cboBaudRate.Items.Add("115200");
            ModemBaudRate = 115200;

            cboTimeout.Items.Add("150");
            cboTimeout.Items.Add("300");
            cboTimeout.Items.Add("600");
            cboTimeout.Items.Add("900");
            cboTimeout.Items.Add("1200");
            cboTimeout.Items.Add("1500");
            cboTimeout.Items.Add("1800");
            cboTimeout.Items.Add("2000");
            ModemTimeOut = 300;

            // Create new Service object - the parent of all and the main interface to you.
            srv = Service.getInstance();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (btnDisconnect.Enabled)
                btnDisconnect.PerformClick();
            //e.Cancel = false;
        }

        private void btnRefreshPort_Click(object sender, EventArgs e)
        {
            FillPorts();
        }

        #region Display all available COM Ports
        private void FillPorts()
        {
            cboPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();

            // Add all port names to the combo box:
            foreach (string p in ports)
            {
                this.cboPort.Items.Add(p);
            }
        }
        #endregion


        #region properties
        public string ModemPort
        {
            get
            {
                return cboPort.Text;
            }
            set
            {
                cboPort.Text = value;
            }
        }
        public int ModemBaudRate
        {
            get
            {
                return Convert.ToInt32(cboBaudRate.Text);
            }
            set
            {
                cboBaudRate.Text = value.ToString();
            }
        }
        public int ModemTimeOut
        {
            get
            {
                return Convert.ToInt32(cboTimeout.Text);
            }
            set
            {
                cboTimeout.Text = value.ToString();
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            PreparePort();
        }

        private void PreparePort()
        {
            try
            {
                Output("Persiapan port untuk kirim sms.");
                Output("loading ...........");
                //Output(Library.getLibraryDescription());
                //Output("Version: " + Library.getLibraryVersion());

                // *** The tricky part ***
                // *** Comm2IP Driver ***
                // Create (and start!) as many Comm2IP threads as the modems you are using.
                // Be careful about the mappings - use the same mapping in the Gateway definition.
                com1 = new Comm2IP.Comm2IP(new byte[] { 127, 0, 0, 1 }, 12000, ModemPort, ModemBaudRate);

                // Start the COM listening thread.
                new Thread(new ThreadStart(com1.Run)).Start();

                // Create the Gateway representing the serial GSM modem.
                // Due to the Comm2IP bridge, in SMSLib for .NET all modems are considered IP modems.
                gateway = new IPModemGateway("modem." + ModemPort, "127.0.0.1", 12000, "Wavecom", "M1306B");
                gateway.setIpProtocol(ModemGateway.IPProtocols.BINARY);

                // Set the modem protocol to PDU (alternative is TEXT). PDU is the default, anyway...
                gateway.setProtocol(AGateway.Protocols.PDU);

                // Do we want the Gateway to be used for Inbound messages?
                gateway.setInbound(true);

                // Do we want the Gateway to be used for Outbound messages?
                gateway.setOutbound(true);

                // Add the Gateway to the Service object.
                srv.addGateway(gateway);

                // Start! (i.e. connect to all defined Gateways)
                srv.startService();

                Output("Persiapan port sukses.");

                btnDisconnect.Enabled = true;
                btnOK.Enabled = false;
                gbSMS.Enabled = true;
            }
            catch (Exception ex)
            {
                Output("Persiapan port error : " + ex.GetBaseException().Message);
                Output("Langkah penanggulangan : ");
                Output("1. Cek koneksi modem anda, pastikan port yang dipilih benar.");
                Output("2. restart program ini dan pasang ulang modem anda");
                Output("3. Jika masih error, coba restart komputer ini");
            }

        }
        private void Output(string text)
        {
            //if (this.txtOutput.InvokeRequired)
            //{
            //    SetTextCallback stc = new SetTextCallback(Output);
            //    this.Invoke(stc, new object[] { text });
            //}
            //else
            {
                txtOutput.AppendText(text);
                txtOutput.AppendText("\r\n");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Output("-- Starting Stop Modem --");
                com1.Stop();
                if (srv.getServiceStatus() == org.smslib.Service.ServiceStatus.STARTED || srv.getServiceStatus() == org.smslib.Service.ServiceStatus.STARTING)
                    srv.stopService();

                Output("-- Stop Modem Success --");

                btnDisconnect.Enabled = false;
                btnOK.Enabled = true;
                gbSMS.Enabled = false;
            }
            catch (Exception ex)
            {
                Output("Stop Modem Error : " + ex.GetBaseException().Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string sep = ",." + Environment.NewLine;
            string[] recipents = txtRecipients.Text.Split(sep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < recipents.Length; i++)
            {
                try
                {
                    Output("-- Sent to : " + recipents[i]);
                    Output(SendMessage(txtMessage.Text, recipents[i]));
                    Output("-- Sent SMS Success --");
                }
                catch (Exception ex)
                {
                    Output("Sent SMS Error : " + ex.GetBaseException().Message);
                }
            }
        }

        private string SendMessage(string txt, string recipent)
        {
            //if (txt.Length >= 140)
            //{
            //    txt = txt.Substring(0, 100) + "...";
            //}
            OutboundMessage msg = new OutboundMessage(recipent, txt);
            srv.sendMessage(msg);
            string failure = msg.getFailureCause().toString();
            return string.IsNullOrEmpty(failure) ? "" : failure;
        }
    }
}
