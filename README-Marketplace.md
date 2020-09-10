# Perforce Diff Margin

Perforce Diff Margin displays live Perforce changes of the currently edited file on Visual Studio margin and scroll bar.

## Perforce connection setup

To configure Perforce connection Helix Server system variables mechanism is used.
You can set up them from the plugin. Just open settings in the "Perforce Diff Margin Configuration" toolbar.

[p4 set](https://www.perforce.com/manuals/v18.1/cmdref/Content/CmdRef/p4_set.html) is called under the hood to configure client, workspace and username.
These 3 things are required to connect. 
1. [P4PORT](https://www.perforce.com/manuals/v17.1/cmdref/Content/CmdRef/P4PORT.html) is the address of the repository.
2. [P4USER](https://www.perforce.com/manuals/v18.1/cmdref/Content/CmdRef/P4USER.html) is your username
3. [P4CLIENT](https://www.perforce.com/manuals/v18.1/cmdref/Content/CmdRef/P4CLIENT.html) is workspace name. A few workspaces can exist on 1 machine, so this information is also needed for correct margins.

You can set up the variables manually and connect via P4V desktop client. In this case, the plugin will connect automatically.
Usually, workspace(P4CLIENT variable) is not set by default.

You can use connect/disconnect in case something get wrong or you want to change the user for example.
One more usual case: if you lose internet connection the plugin can't work because perforce require a connection for p4 diff. But when you go online again plugin can't know that connection is OK. Here connect button can help.

## Features

*   Supports Visual Studio 2012 through Visual Studio 2019
*   Quickly view all current file changes on
    *   Left margin
    *   Scroll Bars in map and bar mode with and without source overview
        *   blue rectangle for modifications
        *   green rectangles for new lines
        *   red triangles for deletions
        *   all colors configurable through Visual Studio Fonts and Colors options
*   Undo the change
*   Copy the old code into the clipboard
*   Copy a part of the old code by selecting it in the popup
*   Show the diff in Visual Studio Diff window
*   Navigate to previous/next change on file using user defined keyboard shortcuts or the popup icons
*   Open popup with user defined keyboard shortcuts, close with esc key 
*   Support Visual Studio 2013 Dark, Light, and Blue Theme
*   Support zoom

# Sponsor

If you use and 💗 Perforce Diff Margin and Git Diff Margin extensions you can [become a sponsor](https://github.com/sponsors/laurentkempe) now!
He is the author of Git Diff Margin and without his work Perforce Diff Margin would not exists! Thank you! 

# Get the code

[https://github.com/laurentkempe/GitDiffMargin](https://github.com/laurentkempe/GitDiffMargin)

# Report Issue

* To report a bug, please use the [**Issue Tracker**](https://github.com/laurentkempe/GitDiffMargin/issues/new?assignees=&labels=bug&template=bug_report.md&title=)
* To suggest an idea, please use the [**Issue Tracker**](https://github.com/laurentkempe/GitDiffMargin/issues/new?assignees=&labels=&template=feature_request.md&title=)

# Credits

Thanks to

* Laurent Kempé [@laurentkempe](https://github.com/laurentkempe) The auther of Git Diff Margin

And other contributors of Git Diff Margin:
*   Sam Harwell [@sharwell](https://github.com/sharwell) for all the improvements
*   Rick Sladkey [@ricksladkey](https://github.com/ricksladkey) for the fixes and features
*   [@Iristyle](https://github.com/Iristyle) for the chocolatey package
*   Yves Goergen [@](https://github.com/dg9ngf)[dg9ngf](https://github.com/dg9ngf)
*   [@heinzbeinz](https://github.com/heinzbeinz) for the support of Visual Studio 15 preview
*   Jamie Cansdale [@jcansdale](https://github.com/jcansdale) for bugfix
*   Charles Milette [sylveon](https://github.com/sylveon) for bugfix
*   Gary Ewan Park [@gep13](https://github.com/gep13) for the new chocolatey package
*   Duncan Smart [@duncansmart](https://github.com/duncansmart) for bug fix
