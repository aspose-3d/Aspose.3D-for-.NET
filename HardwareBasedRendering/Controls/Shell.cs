﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetBrowser.Controls
{
    class Shell
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
        public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        public static uint SHGFI_SELECTED = 0x10000;
        public static uint SHGFI_OPENICON = 0x2;


        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public static Icon LargeIconFromPath(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();

            SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            Icon myIcon = Icon.FromHandle(shinfo.hIcon);
            return myIcon;
        }
        public static Icon SmallIconFromPath(string path, bool selected)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            var flags = SHGFI_ICON | SHGFI_SMALLICON;
            if (selected)
                flags |= SHGFI_OPENICON;

            SHGetFileInfo(path, FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
            Icon myIcon = Icon.FromHandle(shinfo.hIcon);
            return myIcon;
        }
    }
}
