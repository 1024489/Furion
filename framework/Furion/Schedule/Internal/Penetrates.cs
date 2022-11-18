﻿// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Furion.Schedule;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 获取默认的序列化对象
    /// </summary>
    /// <returns><see cref="JsonSerializerOptions"/></returns>
    internal static JsonSerializerOptions GetDefaultJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            AllowTrailingCommas = true
        };
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns><see cref="string"/></returns>
    internal static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, GetDefaultJsonSerializerOptions());
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <returns>T</returns>
    internal static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, GetDefaultJsonSerializerOptions());
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    /// <returns><see cref="DateTime"/></returns>
    internal static DateTime GetNowTime(bool useUtcTimestamp)
    {
        return useUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
    }

    /// <summary>
    /// 获取当前时间（Unspecified）格式
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    /// <returns><see cref="DateTime"/></returns>
    internal static DateTime GetUnspecifiedNowTime(bool useUtcTimestamp)
    {
        // 获取当前时间作为检查时间
        var nowTime = GetNowTime(useUtcTimestamp);

        // 采用 DateTimeKind.Unspecified 转换当前时间并忽略毫秒之后部分（用于减少误差）
        return new DateTime(nowTime.Year
            , nowTime.Month
            , nowTime.Day
            , nowTime.Hour
            , nowTime.Minute
            , nowTime.Second
            , nowTime.Millisecond);
    }

    /// <summary>
    /// 将属性名切割成多个单词
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <returns>单词数组</returns>
    internal static string[] SplitToWords(string propertyName)
    {
        if (propertyName == null) return Array.Empty<string>();

        if (string.IsNullOrWhiteSpace(propertyName)) return new string[] { propertyName };
        if (propertyName.Length == 1) return new string[] { propertyName };

        return Regex.Split(propertyName, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
            .Where(u => u.Length > 0)
            .ToArray();
    }

    /// <summary>
    /// 设置字符串首字母大/小写
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="isUpper">是否大写</param>
    /// <returns><see cref="string"/></returns>
    internal static string SetFirstLetterCase(string str, bool isUpper = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;

        var firstLetter = str.First().ToString();
        return string.Concat(isUpper
            ? firstLetter.ToUpper()
            : firstLetter.ToLower(), str.AsSpan(1));
    }

    /// <summary>
    /// 根据属性名获取指定的命名法
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="bool"/></returns>
    internal static string GetNaming(string propertyName, NamingConventions naming = NamingConventions.Pascal)
    {
        var words = SplitToWords(propertyName);
        var tempWords = new List<string>();

        // 遍历每一个单词
        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i];

            switch (naming)
            {
                case NamingConventions.CamelCase:
                    tempWords.Add(SetFirstLetterCase(word));
                    continue;
                case NamingConventions.Pascal:
                    tempWords.Add(SetFirstLetterCase(word, i != 0));
                    continue;
                case NamingConventions.UnderScoreCase:
                    tempWords.Add(SetFirstLetterCase(word, false));
                    continue;
            }
        }

        return string.Join(naming == NamingConventions.UnderScoreCase ? "_" : string.Empty, tempWords);
    }

    /// <summary>
    /// 获取 SQL 的值
    /// </summary>
    /// <param name="obj">对象值</param>
    /// <returns><see cref="string"/></returns>
    internal static string GetSqlValueOrNull(object obj)
    {
        return obj == null ? "NULL" : $"'{obj}'";
    }

    /// <summary>
    /// 高性能创建 JSON 字符串
    /// </summary>
    /// <param name="writeAction"><see cref="Utf8JsonWriter"/></param>
    /// <returns><see cref="string"/></returns>
    internal static string Write(Action<Utf8JsonWriter> writeAction)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Indented = true,
            // 解决中文乱码问题
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        writeAction?.Invoke(writer);

        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// 获取 JsonElement 实际的值
    /// </summary>
    /// <param name="value">对象值</param>
    /// <returns><see cref="object"/></returns>
    internal static object GetJsonElementValue(object value)
    {
        if (value is not JsonElement ele) return value;

        // 处理 Array 类型的值
        if (ele.ValueKind == JsonValueKind.Array)
        {
            var arrEle = ele.EnumerateArray();
            var length = ele.GetArrayLength();
            var arr = new object[length];

            var i = 0;
            foreach (var item in arrEle)
            {
                arr[i] = GetJsonElementValue(item);
                i++;
            }

            return arr;
        }

        // 处理单个值
        object actValue = ele.ValueKind switch
        {
            JsonValueKind.String => ele.GetString(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            JsonValueKind.Number => ele.GetInt32(),
            _ => throw new ArgumentException("Only int, string, boolean and null types or array types constructed by them are supported.")
        };

        return actValue;
    }
}