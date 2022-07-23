using Microsoft.Extensions.Logging;

namespace Furion.Logging;

/// <summary>
/// �ļ���־��¼������ѡ��
/// </summary>
[SuppressSniffer]
public sealed class FileLoggerSettings
{
    /// <summary>
    /// ��־�ļ�����·�����ļ������Ƽ� .log ��Ϊ��չ��
    /// </summary>
    public string FileName { get; set; } = null;

    /// <summary>
    /// ׷�ӵ��Ѵ�����־�ļ��򸲸�����
    /// </summary>
    public bool Append { get; set; } = true;

    /// <summary>
    /// ����ÿһ����־�ļ����洢��С��Ĭ��������
    /// </summary>
    /// <remarks>���ָ���˸�ֵ����ô��־�ļ���С�����˸����þͻᴴ������־�ļ����´�������־�ļ����������ļ���+[�������].log</remarks>
    public long FileSizeLimitBytes { get; set; } = 0;

    /// <summary>
    /// ������󴴽�����־�ļ�������Ĭ�������ƣ���� <see cref="FileSizeLimitBytes"/> ʹ��
    /// </summary>
    /// <remarks>���ָ���˸�ֵ����ô������ֵ���������־�ļ��д�ͷд�븲��</remarks>
    public int MaxRollingFiles { get; set; } = 0;

    /// <summary>
    /// �����־��¼����
    /// </summary>
    public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;
}