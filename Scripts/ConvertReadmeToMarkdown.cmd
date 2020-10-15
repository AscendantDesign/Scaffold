:: ConvertReadmeToMarkdown.cmd
:: Convert the document Readme.docx to README.md
:: Dependencies:
::  pandoc (https://github.com/jgm/pandoc)
::  FindAndReplace (https://github.com/danielanywhere/FindAndReplace)
::
:: Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
:: Released for public access under the GNU General Public License v3.0.
:: https://opensource.org/licenses/GPL-3.0
@ECHO OFF
SET SOURCE=C:\Users\Daniel\Documents\GitHub\Scaffold\Docs\Wiki\ReadMe.docx
SET TARGET=C:\Users\Daniel\Documents\GitHub\Scaffold\README.md
SET POSTCONFIG=C:\Users\Daniel\Documents\GitHub\Scaffold\Scripts\ReadmePostConversion.json
SET FAR=C:\Users\Daniel\Documents\Visual Studio 2015\Projects\FindAndReplace\FindAndReplace\bin\Debug\FindAndReplace.exe

PANDOC -f docx -t markdown_strict+pipe_tables -s "%SOURCE%" -o "%TARGET%"
"%FAR%" /files:"%TARGET%" /patternfile:"%POSTCONFIG%"
