using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1(IServer server)    // ע�� IServer ���񣬻�ȡ Web ������ַ/�˿�
    {
        InitializeComponent();

        webview.Dock = DockStyle.Fill;
        webview.Source = new Uri(server.GetServerAddress());
    }
}