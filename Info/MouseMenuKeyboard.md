Scaffold Menu, Mouse, and Keyboard
==================================

Last updated by Daniel Patterson, Friday, October 16, 2020

This page describes all of the menu choices, mouse behaviors, and
keyboard shortcuts currently defined for Scaffold.

In this document, when a keyboard shortcut includes commas, the comma
symbol should be interpreted as a separate sequential keystroke. For
example \[Alt\]\[E\], \[D\] can be re-worded as press \[Alt\] and \[E\]
together, then release those keys and press \[D\].

Click a link to jump to that section.

-   [Menu Options](#menu-options).

-   [Keyboard Shortcuts](#keyboard-shortcuts).

-   [Mouse Behaviors](#mouse-behaviors).

Menu Options
------------

Following is the current state of the Scaffold menu.

<table>
<thead>
<tr class="header">
<th><strong>Name</strong></th>
<th><strong>Description</strong></th>
<th><strong>Shortcuts</strong></th>
<th><strong>Notes</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>Edit / Duplicate Selected Nodes</td>
<td>Create a clone of all of the currently selected decision tree nodes.</td>
<td>[Alt][E], [D] or<br />
[Ctrl][D]</td>
<td></td>
</tr>
<tr class="even">
<td>File / Convert / PowerPoint To HTML</td>
<td>Convert a Microsoft PowerPoint slide series to interactive e-learning lesson and publish as universal HTML / SVG.</td>
<td>[Alt][F], [C], [H]</td>
<td>Not yet implemented.</td>
</tr>
<tr class="odd">
<td>File / Convert / PowerPoint To TinyLMS</td>
<td>Convert a Microsoft PowerPoint slide series to interactive e-learning lesson and publish as HTML / SVG for the Ascendant Design TinyLMS learning management system.</td>
<td>[Alt][F], [C], [T]</td>
<td>Not yet implemented.</td>
</tr>
<tr class="even">
<td>File / Export / Decision Tree To PowerPoint</td>
<td>Export the currently loaded decision tree layout to dialog captions on the specified Microsoft PowerPoint slide deck.</td>
<td>[Alt][F], [E], [P]</td>
<td></td>
</tr>
<tr class="odd">
<td>File / Open Node File</td>
<td>Open a .node.json file containing node definitions.</td>
<td>[Alt][F], [O] or<br />
[Ctrl][O]</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>File / Open SVG File</td>
<td>Open an .svg file containing scalable vector graphics.</td>
<td>[Alt][F], [O] or<br />
[Ctrl][O]</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>File / Save Node File</td>
<td>Save changes to the current node file if a file had already been opened or saved. Otherwise, initiate the Save Node File As menu option.</td>
<td>[Alt][F], [S] or<br />
[Ctrl][S]</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>File / Save Node File As</td>
<td>Save current node layout as a new .node.json file.</td>
<td>[Alt][F], [A] or<br />
[Ctrl][Shift][S]</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="odd">
<td>File / Save SVG File</td>
<td>Save changes to the current SVG file if a file had already been opened or saved. Otherwise, initiate the Save SVG File As menu option.</td>
<td>[Alt][F], [S] or<br />
[Ctrl][S]</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="even">
<td>File / Save SVG File As</td>
<td>Save current SVG as a new .svg file.</td>
<td>[Alt][F], [A] or<br />
[Ctrl][Shift][S]</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>Tools / Animation / Draw Frame [N] to HTML View</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame in a user-specified range using the built-in HTML viewer.</td>
<td>[Alt][T], [A], [F]</td>
<td></td>
</tr>
<tr class="even">
<td>Tools / Animation / Draw Frame [N] to SVG View</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame in a user-specified range using the built-in SVG renderer (slide editor).</td>
<td>[Alt][T], [A], [S]</td>
<td></td>
</tr>
<tr class="odd">
<td>Tools / Animation / Save Animation Frames to Disk</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame to the specified output file pattern.</td>
<td>[Alt][T], [A], [A]</td>
<td>Use this method for creating stitched animation scenes and movies.</td>
</tr>
<tr class="even">
<td>Tools / Animation / Timeline File Report</td>
<td>Display a report about characteristics of the specified .timeline.json animation data file.</td>
<td>[Alt][T], [A], [R]</td>
<td><p>Report sections include</p>
<p>number of keyframes; total number of frames; number of scenes and marks; scene and mark listing; scenes, marks, and characters not used in a keyframe; keyframes referencing non-existent marks; discrete timeline where marks have been resolved to keyframes.</p></td>
</tr>
<tr class="odd">
<td>Tools / base64 Data Packing / To CSS URL</td>
<td>Convert a binary file of any kind to a base64 encoded data URI for use with a CSS3 URL attribute.</td>
<td>[Alt][T], [P], [U]</td>
<td></td>
</tr>
<tr class="even">
<td>Tools / base64 Data Packing / To HTML IMG SRC</td>
<td>Convert a binary file of any kind to a base64 encoded data URI for use with a SRC attribute in an HTML &lt;img&gt; element.</td>
<td>[Alt][T], [P], [S]</td>
<td></td>
</tr>
<tr class="odd">
<td>Tools / base64 Data Packing / To Raw String</td>
<td>Convert a binary file of any kind to a base64 string for use in any setting.</td>
<td>[Alt][T], [P], [R]</td>
<td></td>
</tr>
<tr class="even">
<td>Tools / base64 Data Unpacking / From Clipboard</td>
<td>Convert a base64 string to a binary file from the contents of the clipboard.</td>
<td>[Alt][T], [U], [C]</td>
<td></td>
</tr>
<tr class="odd">
<td>Tools / base64 Data Unpacking / From File</td>
<td>Convert a base64 string in a source file to a binary destination file.</td>
<td>[Alt][T], [U], [F]</td>
<td></td>
</tr>
<tr class="even">
<td>Tools / Clipboard / Clear</td>
<td>Clear the contents of the clipboard.</td>
<td>[Alt][T], [L], [C]</td>
<td></td>
</tr>
<tr class="odd">
<td>Tools / Clipboard / Load From File...</td>
<td>Load the clipboard from a previously stored binary clipboard data file.</td>
<td>[Alt][T], [L], [L]</td>
<td></td>
</tr>
<tr class="even">
<td>Tools / Clipboard / Save As File...</td>
<td>Save the current clipboard contents as a binary file.</td>
<td>[Alt][T], [L], [S]</td>
<td></td>
</tr>
<tr class="odd">
<td>Tools / Color Palette</td>
<td>Display the color palette dialog.</td>
<td>[Alt][T], [C]</td>
<td>Useful for working with and selecting colors for other applications.</td>
</tr>
<tr class="even">
<td>View / Controls / Node Control Info</td>
<td>Display a report of node control and related mouse positions, sizes, and properties.</td>
<td>[Alt][V], [C], [N]</td>
<td>Mostly for form and control developer use.</td>
</tr>
<tr class="odd">
<td>View / Zoom / 100%</td>
<td>Zoom to 1:1 view.</td>
<td>[Alt][V], [Z], [1] or<br />
[Ctrl][Shift][Z]</td>
<td></td>
</tr>
<tr class="even">
<td>View / Zoom / In</td>
<td>Zoom to 120% of the current zoom factor.</td>
<td>[Alt][V], [Z], [I] or<br />
[Ctrl][Shift][I]</td>
<td></td>
</tr>
<tr class="odd">
<td>View / Zoom / Out</td>
<td>Zoom to 83.3% of the current zoom factor.</td>
<td>[Alt][V], [Z], [O] or<br />
[Ctrl][Shift][O]</td>
<td></td>
</tr>
<tr class="even">
<td>Window / Decision Tree</td>
<td>Activate the decision tree node editor.</td>
<td><p>[Alt][W], [D] or</p>
<p>[Ctrl][1]</p></td>
<td>Also commonly referred to as the Node Editor.</td>
</tr>
<tr class="odd">
<td>Window / HTML Viewer</td>
<td>Activate the HTML preview control.</td>
<td><p>[Alt][W], [H] or</p>
<p>[Ctrl][3]</p></td>
<td></td>
</tr>
<tr class="even">
<td>Window / Slide Editor</td>
<td>Activate the slide editor and SVG viewer.</td>
<td><p>[Alt][W], [S] or</p>
<p>[Ctrl][2]</p></td>
<td></td>
</tr>
</tbody>
</table>

Keyboard Shortcuts
------------------

Note. All menu items have corresponding shortcuts, but some of the
following menu names have multiple shortcuts. The following list is
sorted by preferred shortcut style first, followed by natural shortcut
definition.

<table>
<thead>
<tr class="header">
<th><strong>Shortcut</strong></th>
<th><strong>Menu Name</strong></th>
<th><strong>Description</strong></th>
<th><strong>Notes</strong></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td>[Ctrl][1]</td>
<td>Window / Decision Tree</td>
<td>Activate the decision tree node editor.</td>
<td>Also commonly referred to as the Node Editor.</td>
</tr>
<tr class="even">
<td>[Ctrl][2]</td>
<td>Window / Slide Editor</td>
<td>Activate the slide editor and SVG viewer.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Ctrl][3]</td>
<td>Window / HTML Viewer</td>
<td>Activate the HTML preview control.</td>
<td></td>
</tr>
<tr class="even">
<td>[Ctrl][D]</td>
<td>Edit / Duplicate Selected Nodes</td>
<td>Create a clone of all of the currently selected decision tree nodes.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Ctrl][O]</td>
<td>File / Open Node File</td>
<td>Open a .node.json file containing node definitions.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>[Ctrl][O]</td>
<td>File / Open SVG File</td>
<td>Open an .svg file containing scalable vector graphics.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>[Ctrl][S]</td>
<td>File / Save Node File</td>
<td>Save changes to the current node file if a file had already been opened or saved. Otherwise, initiate the Save Node File As menu option.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>[Ctrl][S]</td>
<td>File / Save SVG File</td>
<td>Save changes to the current SVG file if a file had already been opened or saved. Otherwise, initiate the Save SVG File As menu option.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>[Ctrl][Shift][I]</td>
<td>View / Zoom / In</td>
<td>Zoom to 120% of the current zoom factor.</td>
<td></td>
</tr>
<tr class="even">
<td>[Ctrl][Shift][O]</td>
<td>View / Zoom / Out</td>
<td>Zoom to 83.3% of the current zoom factor.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Ctrl][Shift][S]</td>
<td>File / Save Node File As</td>
<td>Save current node layout as a new .node.json file.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>[Ctrl][Shift][S]</td>
<td>File / Save SVG File As</td>
<td>Save current SVG as a new .svg file.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>[Ctrl][Shift][Z]</td>
<td>View / Zoom / 100%</td>
<td>Zoom to 1:1 view.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][E], [D]</td>
<td>Edit / Duplicate Selected Nodes</td>
<td>Create a clone of all of the currently selected decision tree nodes.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][F], [A]</td>
<td>File / Save Node File As</td>
<td>Save current node layout as a new .node.json file.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="even">
<td>[Alt][F], [A]</td>
<td>File / Save SVG File As</td>
<td>Save current SVG as a new .svg file.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="odd">
<td>[Alt][F], [C], [H]</td>
<td>File / Convert / PowerPoint To HTML</td>
<td>Convert a Microsoft PowerPoint slide series to interactive e-learning lesson and publish as universal HTML / SVG.</td>
<td>Not yet implemented.</td>
</tr>
<tr class="even">
<td>[Alt][F], [C], [T]</td>
<td>File / Convert / PowerPoint To TinyLMS</td>
<td>Convert a Microsoft PowerPoint slide series to interactive e-learning lesson and publish as HTML / SVG for the Ascendant Design TinyLMS learning management system.</td>
<td>Not yet implemented.</td>
</tr>
<tr class="odd">
<td>[Alt][F], [E], [P]</td>
<td>File / Export / Decision Tree To PowerPoint</td>
<td>Export the currently loaded decision tree layout to dialog captions on the specified Microsoft PowerPoint slide deck.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][F], [O]</td>
<td>File / Open Node File</td>
<td>Open a .node.json file containing node definitions.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="odd">
<td>[Alt][F], [O]</td>
<td>File / Open SVG File</td>
<td>Open an .svg file containing scalable vector graphics.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="even">
<td>[Alt][F], [S]</td>
<td>File / Save Node File</td>
<td>Save changes to the current node file if a file had already been opened or saved. Otherwise, initiate the Save Node File As menu option.</td>
<td>Only visible when decision tree viewer is active.</td>
</tr>
<tr class="odd">
<td>[Alt][F], [S]</td>
<td>File / Save SVG File</td>
<td>Save changes to the current SVG file if a file had already been opened or saved. Otherwise, initiate the Save SVG File As menu option.</td>
<td>Only visible when slide editor is active.</td>
</tr>
<tr class="even">
<td>[Alt][T], [A], [A]</td>
<td>Tools / Animation / Save Animation Frames to Disk</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame to the specified output file pattern.</td>
<td>Use this method for creating stitched animation scenes and movies.</td>
</tr>
<tr class="odd">
<td>[Alt][T], [A], [F]</td>
<td>Tools / Animation / Draw Frame [N] to HTML View</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame in a user-specified range using the built-in HTML viewer.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [A], [R]</td>
<td>Tools / Animation / Timeline File Report</td>
<td>Display a report about characteristics of the specified .timeline.json animation data file.</td>
<td>Report sections include<br />
number of keyframes; total number of frames; number of scenes and marks; scene and mark listing; scenes, marks, and characters not used in a keyframe; keyframes referencing non-existent marks; discrete timeline where marks have been resolved to keyframes.</td>
</tr>
<tr class="odd">
<td>[Alt][T], [A], [S]</td>
<td>Tools / Animation / Draw Frame [N] to SVG View</td>
<td>Load a .timeline.json animation data file and the accompanying SVG, then animate each frame in a user-specified range using the built-in SVG renderer (slide editor).</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [C]</td>
<td>Tools / Color Palette</td>
<td>Display the color palette dialog.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][T], [L], [C]</td>
<td>Tools / Clipboard / Clear</td>
<td>Clear the contents of the clipboard.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [L], [L]</td>
<td>Tools / Clipboard / Load From File...</td>
<td>Load the clipboard from a previously stored binary clipboard data file.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][T], [L], [S]</td>
<td>Tools / Clipboard / Save As File...</td>
<td>Save the current clipboard contents as a binary file.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [P], [R]</td>
<td>Tools / base64 Data Packing / To Raw String</td>
<td>Convert a binary file of any kind to a base64 string for use in any setting.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][T], [P], [S]</td>
<td>Tools / base64 Data Packing / To HTML IMG SRC</td>
<td>Convert a binary file of any kind to a base64 encoded data URI for use with a SRC attribute in an HTML &lt;img&gt; element.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [P], [U]</td>
<td>Tools / base64 Data Packing / To CSS URL</td>
<td>Convert a binary file of any kind to a base64 encoded data URI for use with a CSS3 URL attribute.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][T], [U], [C]</td>
<td>Tools / base64 Data Unpacking / From Clipboard</td>
<td>Convert a base64 string to a binary file from the contents of the clipboard.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][T], [U], [F]</td>
<td>Tools / base64 Data Unpacking / From File</td>
<td>Convert a base64 string in a source file to a binary destination file.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][V], [C], [N]</td>
<td>View / Controls / Node Control Info</td>
<td>Display a report of node control and related mouse positions, sizes, and properties.</td>
<td>Mostly for form and control developer use.</td>
</tr>
<tr class="even">
<td>[Alt][V], [Z], [1]</td>
<td>View / Zoom / 100%</td>
<td>Zoom to 1:1 view.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][V], [Z], [I]</td>
<td>View / Zoom / In</td>
<td>Zoom to 120% of the current zoom factor.</td>
<td></td>
</tr>
<tr class="even">
<td>[Alt][V], [Z], [O]</td>
<td>View / Zoom / Out</td>
<td>Zoom to 83.3% of the current zoom factor.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][W], [D]</td>
<td>Window / Decision Tree</td>
<td>Activate the decision tree node editor.</td>
<td>Also commonly referred to as the Node Editor.</td>
</tr>
<tr class="even">
<td>[Alt][W], [H]</td>
<td>Window / HTML Viewer</td>
<td>Activate the HTML preview control.</td>
<td></td>
</tr>
<tr class="odd">
<td>[Alt][W], [S]</td>
<td>Window / Slide Editor</td>
<td>Activate the slide editor and SVG viewer.</td>
<td></td>
</tr>
</tbody>
</table>

Mouse Behaviors
---------------

At present, there are three common mouse behaviors.

| **Behavior**        | **Description**  | **Notes** |
|---------------------|------------------|-----------|
| \[Scroll\]          | Pan Up / Down    |           |
| \[Shift\]\[Scroll\] | Pan Left / Right |           |
| \[Ctrl\]\[Scroll\]  | Zoom In / Out    |           |
