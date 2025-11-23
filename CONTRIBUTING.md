<!--
SPDX-FileCopyrightText: 2024-2025 Friedrich von Never <friedrich@fornever.me>

SPDX-License-Identifier: MIT
-->

Contributor Guide
=================

Prerequisites
-------------
To develop the project, you'll need [.NET SDK][dotnet.sdk] 10.0 or later installed.

Build
-----
```console
$ dotnet build
```

Test
----
```console
$ dotnet test
```

File Encoding Changes
---------------------
If the automation asks you to update the file encoding (line endings or UTF-8 BOM) in certain files, run the following PowerShell script ([PowerShell Core][powershell] is recommended to run this script):
```console
$ pwsh -c "Install-Module VerifyEncoding -Repository PSGallery -RequiredVersion 2.2.1 -Force && Test-Encoding -AutoFix"
```

The `-AutoFix` switch will automatically fix the encoding issues, and you'll only need to commit and push the changes.

[dotnet.sdk]: https://dot.net/
[powershell]: https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell
