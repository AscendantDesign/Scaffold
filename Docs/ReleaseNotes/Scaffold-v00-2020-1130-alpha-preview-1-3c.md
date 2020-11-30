Scaffold v0.2020.1130-alpha.preview.1.3c
========================================

Alpha Testing and Technology Preview Release 1.3c
-------------------------------------------------

### Background

This revision concludes the **Chatbot Interface** sprint of November
2020, the major focus of which was to improve operations of the decision
tree editor and to allow interaction between the Scaffold application
and cloud-based chat service hosted on another framework. During this
effort, Slack was selected as the target remote service since they
advertise some level of compatibility with automated chat services.

Local chatbot emulation has also been improved and is available in the
**Tools / Chatbot Emulator** menu option.

### Bugs Fixed

The following bugs have been fixed.

-   **ADO1919 - Chatbot Emulator - Flow Stopped When Blank Resource Is
    Encountered**.
    <https://github.com/AscendantDesign/Scaffold/issues/20>. While using
    the chatbot emulator, the current card would load but no choices
    would be available if any of the media references on that card
    happened to point to blank resources. The new behavior is to treat
    blank and null resources as resources not present. In the current
    version, no error will be displayed if there is a missing resource,
    but the workflow will continue to progress as expected.

-   **ADO1917 - Chatbot Emulator - Links Don't Work**.
    <https://github.com/AscendantDesign/Scaffold/issues/18>. Links were
    not operational anywhere due to an issue that had been introduced
    while migrating node functions into their own class library.

### Feature Tasks Completed

The following tasks were completed during the **Chatbot Interface**
sprint.

-   **ADO1914 - Chatbot - Remove Published Course**. A previously
    published course can now be removed from the slack server using the
    menu option **File / Unpublish / Slack Chatbot Conversation**.

-   **ADO1918 - Chatbot Emulator - Start From Specified Node**. The user
    can now start the chatbot emulator from the selected node or from
    the beginning.

-   **ADO1916 - Infrastructure - Hide Slack Token On Server**. To keep
    the Slack tokens as secure as possible, they are not shared directly
    in the project. Instead, they are now loaded from a local-only file
    named **slackserverkeys.user.json**. Each developer must maintain
    his or her own server token library when publishing his or her own
    server. Courses published to the official Scaffold repository don't
    need to take any notice of this condition.

-   **ADO1906 - Node Editor - Add Thumbnails To Socket Media**.
    <https://github.com/AscendantDesign/Scaffold/issues/1>. Each socket
    with media attached will now display a small thumbnail of the
    highest priority type of asset available.

-   **ADO1915 - Node Editor - Allow \[Esc\] To Cancel Current Drag
    Operation**. \[Esc\] press while dragging objects now results in
    cancellation of the drag action.

-   **ADO1910 - Node Editor - Option To Carry Output Socket Media To
    Connected Response**.
    <https://github.com/AscendantDesign/Scaffold/issues/10>. If no media
    has been defined on a node's response socket and the corresponding
    answer socket of the previous node has an applicable asset, the
    asset found in the previous node's connected answer socket is used.

-   **ADO1908 - Resource Gallery - Allow Gallery Dialog To Load And
    Delete File-Level Resources**.
    <https://github.com/AscendantDesign/Scaffold/issues/2>. You can now
    add and remove files directly from the resource gallery dialog.

-   **ADO1921 - Resource Gallery - Provide Feedback On Audio And Link
    Icons**. Audio and link icons now have filename and URI information
    displayed, respectively.

-   **ADO1920 - User Interface - Add Search Tool To Allow Search For Any
    Element Kind Or Text In File**. A primitive find dialog has been
    added that allows searching for any type of media attachments, with
    and without filenames, and any kind of search text.

-   **ADO1922 - User Interface - Scroll Into View**. A newly loaded file
    is now scrolled into view immediately after loading, and the user
    can manually scroll the layout into view.
