## TODO list step-by-step

* handle all possible p4 errors 
* **DONE** add popup with login request and "P4USER, P4PORT and P4CLIENT should be set"
* think abouit separate toolbar. For now a few buttons can be added: "P4 Connect"(Refresh), "P4 Disconnect", "Set P4 port", "Set workspace", "Set user", "Login". Buttons should call corresponding dialogs with 1 field.
    + **DONE** Implement dummy buttons
    + Impleemnt dummy handlers
    + Implement all dialogs and add them to handlers
* StartExternalDiff using p4 print. Find an example in temporary_changes_start_develop branch
* implement settings? Use as an example "Open in VS Code" project.
* Rename Git to Perforce for every item in project (think about separate brunch or local commits and git squash)
* Remove useless code
* Fix README.md