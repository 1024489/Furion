using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// ���ݿ���־��¼������ѡ��
/// </summary>
[SuppressSniffer]
public sealed class DatabaseLoggerSettings
{
    /// <summary>
    /// �����־��¼����
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;
}