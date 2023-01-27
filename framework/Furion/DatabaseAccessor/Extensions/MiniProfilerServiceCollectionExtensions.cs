﻿// MIT License
//
// Copyright (c) 2020-present 百小僧, Baiqian Co.,Ltd and Contributors
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

using Furion.DatabaseAccessor;
using StackExchange.Profiling.Internal;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// MiniProfiler 服务拓展类
/// </summary>
[SuppressSniffer]
public static class MiniProfilerServiceCollectionExtensions
{
    /// <summary>
    /// 添加 EF Core 进程监听拓展
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMiniProfilerBuilder AddRelationalDiagnosticListener(this IMiniProfilerBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.Services.AddSingleton<IMiniProfilerDiagnosticListener, RelationalDiagnosticListener>();

        return builder;
    }
}