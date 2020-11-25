:: UpdateSetupFileList.cmd
:: Copyright(c) 2020. Ascendant Design and Training, LLC
:: This file is licensed under the MIT License.
:: Please see the LICENSE file in this project.
:: ------
:: Creates a refreshed list of <File> elements for the ScaffoldSetup/Product.wxs file.
:: The Wix Heat command is valid for this task, if desired. This approach allows for
:: finer detail focus on the discrete files.
:: @ECHO OFF

C:\Scripts\XmlManage\XmlManage.exe /config:XmlManageSetupFiles.json
