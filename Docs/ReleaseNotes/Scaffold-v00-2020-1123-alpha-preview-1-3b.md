Scaffold v0.2020.1123-alpha.preview.1.3b
========================================

Alpha Testing and Technology Preview Release 1.3b
-------------------------------------------------

### Background

This revision is a hot patch to eliminate bugs found prior to a
presentation on November 23, 2020.

### Bugs Fixed

The following bugs were removed in this version.

-   **GIT0015 - Chatbot - Chatbot Emulator Answers Should Use Next
    Response Media If No Local Media Found**. When no media has been
    defined on the output socket, use the response media of the next
    node if that socket has media defined.

-   **GIT0014 - Chatbot - Path To Chatbot Emulator Resources Must Not Be
    Altered**. After working with the nodes file, adding bulk resources,
    editing assignments, and making other changes, an attempt to run the
    chatbot emulator on the loaded file was sometimes resulting in an
    exception noting that the chatbot script file could not be found in
    the last used data path. The chatbot emulator is only available at
    the application executable path and is never copied to any other
    folder.

-   **GIT0011 - Node Editor - Clicking Image Should Open Dialog**.
    Clicking the image of a node was resulting in the opening of that
    image in HTML editor. For consistency, the response has been changed
    to opening the corresponding dialog on a double-click.

-   **GIT0016 - Node Editor - Images Not Refreshed On Newly Duplicated
    Nodes**. When duplicating a node, its image was not displayed right
    away even though the image resource property was copied as expected.
    The node thumbnails and icons are now being rebuilt immediately
    after duplication.

-   **GIT0012 - Node Editor - New Unsaved Socket Record Unaware Of
    Loaded Resources**. When in the process of creating a new Socket
    record, before that record has been saved, the socket editor dialog
    was not aware of the loaded resources.

-   **GIT0013 - Node Editor - Unable To Delete Selected Question
    Media**. In the node properties dialog, the user was unable to
    delete the currently selected question media. The button was
    remaining disabled.

### Feature Tasks Completed

No feature tasks were handled in this version.
