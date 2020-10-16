:: ConvertMouseMenuKeyboardDocToMarkdown.cmd
:: Convert the document ScaffoldMenuMouseKeyboard.docx to Info/MouseMenuKeyboard.md
:: Dependencies:
::  pandoc (https://github.com/jgm/pandoc)
::  FindAndReplace (https://github.com/danielanywhere/FindAndReplace)
::
:: Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
:: Copyright (c). 2020 Ascendant Design and Training, LLC
:: Released for public access under the MIT License.
:: https://opensource.org/licenses/MIT
@ECHO OFF
SET SOURCE=%USERPROFILE%\Documents\GitHub\Scaffold\Docs\ScaffoldMenuMouseKeyboard.docx
SET TARGET=%USERPROFILE%\Documents\GitHub\Scaffold\Info\MouseMenuKeyboard.md
SET POSTCONFIG=%USERPROFILE%\Documents\GitHub\Scaffold\Scripts\MouseMenuKeyboardDocPostConversion.json
SET FAR=C:\Scripts\FindAndReplace\FindAndReplace.exe

PANDOC -f docx -t markdown_strict+pipe_tables -s "%SOURCE%" -o "%TARGET%"
"%FAR%" /files:"%TARGET%" /patternfile:"%POSTCONFIG%"
