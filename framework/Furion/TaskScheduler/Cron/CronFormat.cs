using Furion.DependencyInjection;
using System;
using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// Cron ���ʽ֧������
    /// </summary>
    [SkipScan, Flags]
    public enum CronFormat
    {
        /// <summary>
        /// ֻ�� 5 ���ַ������ӣ�Сʱ����/�죬�죬��/��
        /// </summary>
        [Description("ֻ�� 5 ���ַ������ӣ�Сʱ����/�죬�죬��/��")]
        Standard = 0,

        /// <summary>
        /// ֧���������Ҳ���� 6 ���ַ�
        /// </summary>
        [Description("֧���������Ҳ���� 6 ���ַ�")]
        IncludeSeconds = 1
    }
}