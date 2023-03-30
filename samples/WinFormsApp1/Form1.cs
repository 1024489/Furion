using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1(IServer server)    // ע�� IServer ���񣬻�ȡ Web ������ַ/�˿�
    {
        InitializeComponent();
        Resize += Form1_Resize;

        webview.Size = this.ClientSize;
        webview.Source = new Uri($"{server.GetServerAddress()}/Home");
    }

    private void Form1_Resize(object? sender, EventArgs e)
    {
        webview.Size = this.ClientSize;
    }
}