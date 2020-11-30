:: ConvertReleaseNoteToMarkdown.cmd
:: Convert the specified release note file to *.md
:: Dependencies:
::  pandoc (https://github.com/jgm/pandoc)
::  FindAndReplace (https://github.com/danielanywhere/FindAndReplace)
::
:: Copyright (c). 2020 Ascendant Design and Training, LLC
:: Released for public access under the MIT License.
:: https://opensource.org/licenses/MIT
@ECHO OFF
SET NOTE=Scaffold-v00-2020-1130-alpha-preview-1-3c
SET SOURCE=%USERPROFILE%\Documents\GitHub\Scaffold\Docs\ReleaseNotes\%NOTE%.docx
SET TARGET=%USERPROFILE%\Documents\GitHub\Scaffold\Docs\ReleaseNotes\%NOTE%.md
::SET POSTCONFIG=%USERPROFILE%\Documents\GitHub\Scaffold\Scripts\ReleaseNotePostConversion.json
::SET FAR=C:\Scripts\FindAndReplace\FindAndReplace.exe

PANDOC -f docx -t markdown_strict+pipe_tables -s "%SOURCE%" -o "%TARGET%"
::"%FAR%" /files:"%TARGET%" /patternfile:"%POSTCONFIG%"
