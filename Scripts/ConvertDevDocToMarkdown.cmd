:: ConvertDevDocMarkdown.cmd
:: Convert the document ScaffoldDevelopment.docx to Development.md
:: Dependencies:
::  pandoc (https://github.com/jgm/pandoc)
::  FindAndReplace (https://github.com/danielanywhere/FindAndReplace)
::
:: Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
:: Copyright (c). 2020 Ascendant Design and Training, LLC
:: Released for public access under the MIT License.
:: https://opensource.org/licenses/MIT
@ECHO OFF
SET SOURCE=%USERPROFILE%\Documents\GitHub\Scaffold\Docs\ScaffoldDevelopment.docx
SET TARGET=%USERPROFILE%\Documents\GitHub\Scaffold\Development.md
SET POSTCONFIG=%USERPROFILE%\Documents\GitHub\Scaffold\Scripts\DevDocPostConversion.json
SET FAR=C:\Scripts\FindAndReplace\FindAndReplace.exe

PANDOC -f docx -t markdown_strict+pipe_tables -s "%SOURCE%" -o "%TARGET%"
"%FAR%" /files:"%TARGET%" /patternfile:"%POSTCONFIG%"
