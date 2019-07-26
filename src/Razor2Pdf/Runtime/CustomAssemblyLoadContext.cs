using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Razor2Pdf
{
    /// <summary>
    /// Custom AssemblyLoadContext to load a native library from a specified path and taking into account the library version for the current operating system.
    /// </summary>
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Loads the unmanaged library.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <returns></returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        /// <summary>
        /// Loads the unmanaged DLL.
        /// </summary>
        /// <param name="unmanagedDllName">Name of the unmanaged DLL.</param>
        /// <returns></returns>
        protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
        {
            var extension = "dll";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                extension = "so";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                extension = "dylib";

            return LoadUnmanagedDllFromPath($"{unmanagedDllName}.{extension}");
        }

        /// <summary>
        /// Loads the specified assembly name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }
    }
}
