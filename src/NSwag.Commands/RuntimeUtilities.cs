﻿//-----------------------------------------------------------------------
// <copyright file="RuntimeUtilities.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using Microsoft.Extensions.PlatformAbstractions;

namespace NSwag.Commands
{
    /// <summary>Provides runtime utilities.</summary>
    public class RuntimeUtilities
    {
        /// <summary>Gets the current runtime.</summary>
        public static Runtime CurrentRuntime
        {
            get
            {
#if !NETCOREAPP && !NETSTANDARD
                return IntPtr.Size == 4 ? Runtime.WinX86 : Runtime.WinX64;
#else
                var framework = PlatformServices.Default.Application.RuntimeFramework;
                if (framework.Identifier == ".NETCoreApp")
                {
                    if (framework.Version.Major == 2 && framework.Version.Minor == 0)
                    {
                        return Runtime.NetCore20;
                    }
                    else if (framework.Version.Major == 2 && framework.Version.Minor == 1)
                    {
                        return Runtime.NetCore21;
                    }
                    else if (framework.Version.Major == 2 && framework.Version.Minor > 1)
                    {
                        return Runtime.NetCore22;
                    }
                    else if (framework.Version.Major >= 6)
                    {
                        return Runtime.Net60;
                    }
                    else if (framework.Version.Major >= 5)
                    {
                        return Runtime.Net50;
                    }
                    else if (framework.Version.Major >= 3 && framework.Version.Minor < 1)
                    {
                        return Runtime.NetCore30;
                    }
                    else if (framework.Version.Major >= 3 && framework.Version.Minor >= 1)
                    {
                        return Runtime.NetCore31;
                    }
                    else if (framework.Version.Major == 1 && framework.Version.Minor == 1)
                    {
                        return Runtime.NetCore11;
                    }

                    return Runtime.NetCore10;
                }
                return IntPtr.Size == 4 ? Runtime.WinX86 : Runtime.WinX64;
#endif
            }
        }
    }
}