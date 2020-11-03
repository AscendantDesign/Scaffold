Scaffold v0.2020.1102-alpha.preview.1.0
=======================================

Alpha Testing and Technology Preview Release 1.0
------------------------------------------------

### Background

This revision concludes the **Conversational** sprint of late October
2020, the major focus of which was to bring the operational capabilities
of the application up to the level of practical creation and testing of
the following types of interactive learning modules.

-   Branching.

-   Conversational.

-   Scenario-based.

-   Situational.

Support for the following media has been added on Nodes and Sockets.

-   Audio.

-   Image.

-   Link.

-   Video.

A chatbot emulation script has been added to allow you to test your node
layout in real time.

### Bugs Fixed

No bugs were reported from the previous version.

### Feature Tasks Completed

The following tasks were completed during the **Conversational** sprint.

-   **ADO1888 - Chatbot - Conversation Emulator**. The first version of
    the conversation emulator is running.

-   **ADO1900 - Chatbot - Audio Delivery**. Audio resources can now be
    delivered with chatbot output.

-   **ADO1854 - Chatbot - Image Delivery**. All common image formats can
    now be delivered by direct reference to their tickets on the
    resources collection of the node file.

-   **ADO1901 - Chatbot - Link Delivery**. Link content defined on nodes
    and sockets can now be delivered with chatbot content.

-   **ADO1851 - Chatbot - Video Delivery**. The video can now be
    delivered directly by reference to its ticket on the resources
    collection.

-   **ADO1894 - Deployment - Add FFMPEG Runtime to Program Files
    Folder**. For purposes of Frame &lt;-&gt; Video &lt;-&gt; Thumbnail
    conversion, FFMPEG runtime has been added to the Scaffold project.
    XmlManage application has also been adopted as the main application
    for maintaining the Wix setup configuration files.

-   **ADO1886 - Documentation - Describe All Menus And Keyboard
    Shortcuts**. The menus, mouse behaviors, and keyboard shortcuts have
    been documented and printed to the github page.

-   **ADO1887 - Documentation - Installation And Configuration
    Process**. The installation instructions for pre-release versions
    have been posted on the site
    <https://github.com/ascendantdesign/Scaffold>

-   **ADO1892 - Documentation - Node Data Structure**. Current node data
    structure has been documented.

-   **ADO1878 - Infrastructure - Autosave**. Open SVG or node files can
    now be saved every 5 minutes. However, that feature will be
    temporarily disabled while the application is still in the early
    phases of testing.

-   **ADO1897 - Infrastructure - Embedded Resource Node Serialization
    Mode**. When saving for publication, the serializer is now able to
    convert all URI references into Data URIs.

-   **ADO1856 - Node Editor - Add Image And Thumbnail To Node**. The
    image and its thumbnail have been added to the node. The image
    resource is stored in a separate resources table, and the record is
    referenced by ticket under the property *MediaImage*. During
    runtime, the thumbnail is stored as a non-static property in the
    node named *ThumbImage*.

-   **ADO1881 - Node Editor - Add Properties Grid To Node Dialog**. A
    Property / Control association handler has been written to
    synchronize values between controls and the Universal properties
    list. The node dialog now allows full properties editing of
    permanent and non-permanent values.

-   **ADO1882 - Node Editor - Add Properties Grid To Socket Dialog**.
    The socket dialog properties grid has been added and tested.

-   **ADO1841 - Node Editor - Add Storyboard Properties to Node and
    Socket Dialogs**. Additional storyboard properties
    *StoryPageHorizontalPlacement*, *StoryPageVerticalPlacement*, and
    *StoryPageWidth* have been added to the storyboard pages of the node
    and socket dialogs.

-   **ADO1839 - Node Editor - Alignment and Distribution Functions**.
    The following basic shape alignment functions have been added to the
    **Edit / Align and Distribute** menu.

    -   Align bottom.

    -   Align center horizontal.

    -   Align left.

    -   Align middle vertical.

    -   Align right.

    -   Align top.

    -   Distribute evenly on horizontal.

    -   Distribute evenly on vertical.

-   **ADO1855 - Node Editor - Create New Node Socket Type: Delay and
    Continue**. A new node type named *Delay* is available. A delay
    amount, in seconds, can be set on the node dialog.

-   **ADO1891 - Node Editor - Double-click or \[Edit\] on Property Opens
    Associated Editor**. Properties grid values can now be set using
    multiple editor types. Currently, *Name/Value* and *Color* are
    supported.

-   **ADO1869 - Node Editor - Hero Card Generator**. The user will be
    able to experience the same type of functionality as offered by the
    Microsoft HERO card. The media types Audio, Image, Link, and Video
    are all now directly supported.

-   **ADO1857 - Node Editor - Node Color**. The user is now able to set
    node text and background color on every node.

-   **ADO1898 - Node Editor - Resource Gallery Form**. The Resource
    Gallery dialog is now available for selecting resources.

-   **ADO1880 - Node Editor - Undo Button**. The Undo feature is now
    functional for version 1. It relies upon time-sensitive captures of
    multiple actions into a series of stacks that are acted upon as a
    group for each undoable action. This tends to make the undo action
    for the user much easier to see while providing handling for an
    enumerable number of possible objects and properties per user
    action.
