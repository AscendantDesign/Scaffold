Scaffold v0.2020.1230-alpha.preview.1.4d
========================================

Alpha Testing and Technology Preview Release 1.4d
-------------------------------------------------

### Background

This revision is an intermediate release for sprint 4 of December 2020, the major focus of which was to increase automation capabilities between Scaffold and Microsoft PowerPoint.

Originally, an Adobe Flash converter had been scheduled for this sprint, but given the volume of that library, and the little use it would serve for conversions of old existing files only, it was later decided that many other things can be completed in the same time frame to result greater overall impact.

### Bugs Fixed

The following bugs have been repaired in this release.

-   **ADO1973 - Chatbot Emulator - Images Not Displaying In Emulator When Starting From That Node** / [Local Issue 58](https://github.com/AscendantDesign/Scaffold/issues/58). The allowance for output media on buttons has now been specified in the start-up routine.

-   **ADO1977 - Node Editor - Setting Output Value To Blank Causes Null Object Exception** / [Local Issue 63](https://github.com/AscendantDesign/Scaffold/issues/63). An output value can now be set to blank without causing an error.

-   **ADO1976 - Slack - Delay Node Not Working** / [Local Issue 62](https://github.com/AscendantDesign/Scaffold/issues/62). Delay nodes are now working in the Slack interface.

### Feature Tasks Completed

The following tasks were completed for this release.

-   **ADO1974 - Slack - ${user.name} Variable To Be Replaced Dynamically With Current Username** / [Local Issue 60](https://github.com/AscendantDesign/Scaffold/issues/60). Variable names can now be defined and used inline with the node content. At present, only ${user.name} is defined.
