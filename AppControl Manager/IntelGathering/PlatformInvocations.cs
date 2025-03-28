// MIT License
//
// Copyright (c) 2023-Present - Violet Hansen - (aka HotCakeX on GitHub) - Email Address: spynetgirl@outlook.com
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
// See here for more information: https://github.com/HotCakeX/Harden-Windows-Security/blob/main/LICENSE
//

using System;
using System.Runtime.InteropServices;

namespace AppControlManager.IntelGathering;

internal static partial class PlatformInvocations
{

	// https://learn.microsoft.com/en-us/windows/win32/api/mssip/nf-mssip-cryptsipretrievesubjectguid
	[LibraryImport("crypt32.dll", EntryPoint = "CryptSIPRetrieveSubjectGuid", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static partial bool CryptSIPRetrieveSubjectGuid(
		string FileName,
		IntPtr hFileIn,
		out Guid pgActionID);

	// https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew
	[LibraryImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr CreateFileW(
		string lpFileName,
		uint dwDesiredAccess,
		uint dwShareMode,
		IntPtr lpSecurityAttributes,
		uint dwCreationDisposition,
		uint dwFlagsAndAttributes,
		IntPtr hTemplateFile);

	// https://learn.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle
	[LibraryImport("kernel32.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static partial bool CloseHandle(IntPtr hObject);

	// https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createfilemappinga
	[LibraryImport("kernel32.dll", EntryPoint = "CreateFileMappingW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr CreateFileMapping(
		IntPtr hFile,
		IntPtr pFileMappingAttributes,
		uint flProtect,
		uint dwMaximumSizeHigh,
		uint dwMaximumSizeLow,
		string lpName);

	// https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfilesize
	[LibraryImport("kernel32.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial uint GetFileSize(IntPtr hFile, ref uint lpFileSizeHigh);

	// https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-mapviewoffile
	[LibraryImport("kernel32.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr MapViewOfFile(
		IntPtr hFileMappingObject,
		uint dwDesiredAccess,
		uint dwFileOffsetHigh,
		uint dwFileOffsetLow,
		IntPtr dwNumberOfBytesToMap);

	// https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-imagedirectoryentrytodataex
	[LibraryImport("DbgHelp.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr ImageDirectoryEntryToDataEx(
		IntPtr Base,
		int MappedAsImage,
		ushort DirectoryEntry,
		ref uint Size,
		ref IntPtr FoundHeader);

	// https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-unmapviewoffile
	[LibraryImport("kernel32.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial int UnmapViewOfFile(IntPtr lpBaseAddress);

	// https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-imagentheader
	[LibraryImport("DbgHelp.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr ImageNtHeader(IntPtr ImageBase);

	// https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-imagervatova
	[LibraryImport("DbgHelp.dll", SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static partial IntPtr ImageRvaToVa(
		IntPtr NtHeaders,
		IntPtr Base,
		uint Rva,
		IntPtr LastRvaSection);

}
