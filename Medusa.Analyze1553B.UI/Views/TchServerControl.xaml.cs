using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medusa.Analyze1553B.UI.Views
{
    /// <summary>
    /// Interaction logic for TcpServerControl.xaml
    /// </summary>
    public partial class TcpServerControl : UserControl
    {
        public TcpServerControl()
        {
            InitializeComponent();
        }

        private void OutputWriteLine(string text)
        {
            output.Text = output.Text + "\n" + text;
            //outputScroll.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //OutputWriteLine("Button_Click");
            Task _ = ConnectAsTcpClient("127.0.0.1", 1234);
        }

        private async Task ConnectAsTcpClient(string ip, int port)
        {
            for (; ; )
            {
                try
                {
                    await Task.Delay(millisecondsDelay: 1000);
                    using (var tcpClient = new TcpClient())
                    {
                        OutputWriteLine("[Client] Attempting connection to server " + ip + ":" + port);
                        Task connectTask = tcpClient.ConnectAsync(ip, port);
                        Task timeoutTask = Task.Delay(millisecondsDelay: 100);
                        if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                        {
                            throw new TimeoutException();
                        }

                        //OutputWriteLine("[Client] Connected to server");
                        using (var networkStream = tcpClient.GetStream())
                        using (var reader = new StreamReader(networkStream))
                        using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
                        {
                            //OutputWriteLine(string.Format("[Client] Writing request '{0}'", ClientRequestString));
                            await writer.WriteLineAsync(ClientRequestString);

                            try
                            {
                                for (; ; )
                                {
                                    var response = await reader.ReadLineAsync();
                                    if (response == null) { break; }
                                    OutputWriteLine(string.Format("[Client] Server response was '{0}'", response));
                                }
                                //OutputWriteLine("[Client] Server disconnected");
                            }
                            catch (IOException)
                            {
                                //OutputWriteLine("[Client] Server disconnected");
                            }
                        }
                    }
                }
                catch (TimeoutException)
                {
                    // reconnect
                }
            }
        }

        private static readonly string ClientRequestString = "Hello Mr server";


        private void output_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            outputScroll.ScrollToEnd();
        }
    }
}
