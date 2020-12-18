Scaffold v0.2020.1214-alpha.preview.1.4b
========================================

Alpha Testing and Technology Preview Release 1.4b
-------------------------------------------------

### Background

This revision is an intermediate release for the **Adobe Flash To HTML**
sprint of December 2020, the major focus of which is to increase
automation capabilities between Scaffold, Adobe Flash, and Microsoft
PowerPoint.

### Bugs Fixed

The following bugs have been repaired in this release.

-   **ADO1944 - Color Palette - Recent Colors Not Being Maintained** /
    [Local Issue 33](https://github.com/AscendantDesign/Scaffold/issues/33). The recent colors list now displays the eight most recently selected colors.

-   **ADO1936 - Node Editor - Alignment Not Working When Items Are
    Duplicated** / [Local Issue 25](https://github.com/AscendantDesign/Scaffold/issues/25). The alignment functions are now working with duplicated nodes.

-   **ADO1946 - Node Editor - Distribute Horizontally Is Pulling The Last Item Inward** / [Local Issue 35](https://github.com/AscendantDesign/Scaffold/issues/35). Horizontal and vertical distribution now work as expected.

-   **ADO1942 - Node Editor - Distribute Selected Vertical Middle Moves All Selected Nodes To A Single Y Coordinate** / [Local Issue 31](https://github.com/AscendantDesign/Scaffold/issues/31). This item was repaired by **ADO1936** / [Local Issue 25](https://github.com/AscendantDesign/Scaffold/issues/25).

-   **ADO1943 - Node Editor - Duplicated Node Can Not Be Re-Duplicated** / [Local Issue 32](https://github.com/AscendantDesign/Scaffold/issues/32). Any number of pre-duplicated nodes can now be re-duplicated in any group or any order.

-   **ADO1937 - Node Editor - Landscape Thumbnail Wider Than Node When Node Text Is Short** / [Local Issue 26](https://github.com/AscendantDesign/Scaffold/issues/26). Node thumbnails are now correctly sized inside the node display area.

-   **ADO1941 - Node Editor - Setting Background Color On Properties Dialog Has No Effect** / [Local Issue 30](https://github.com/AscendantDesign/Scaffold/issues/30). Color changes on the Properties tabs of node and socket dialogs are now properly handled.

### Feature Tasks Completed

The following tasks were completed for this release.

-   **ADO1945 - Color Palette - Set Starting Color When Loading From Property Dialog** / [Local Issue 34](https://github.com/AscendantDesign/Scaffold/issues/34). Current color is now preset on the color dialog when displaying with an existing value.

-   **ADO1932 - Node Editor - Allow User To Set Output Text On All Selected Items To Next Question Text**. You can now set all of the output sockets to the question text of the next connected node on all selected nodes through the use of the menu option **Edit / Node / Set Output Sockets On Selected To Following Question**.

-   **ADO1938 - Node Editor - Ending Conversation On An Output Socket** / [Local Issue 27](https://github.com/AscendantDesign/Scaffold/issues/27). You can end the conversation on an output socket. If no connection is made to a following node, a user response is allowed, but no server response follows.
