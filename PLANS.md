## TODO list step-by-step

* **DONE** handle all possible p4 errors.
* **DONE** add popup with login request and "P4USER, P4PORT and P4CLIENT should be set"
* **DONE** think abouit separate toolbar. For now a few buttons can be added: "P4 Connect"(Refresh), "P4 Disconnect", "P4 Settings". "P4 Settings" should call dialog with all setting and password 
    + **DONE** Implement dummy buttons
    + **DONE** Impleemnt connect/disconnect
    + **DONE** Implement settings dialog and add it to buttons
* **DONE** StartExternalDiff using p4 print. Find an example in temporary_changes_start_develop branch
* **WONT FIX** We need diff not vs latest but vs have revision????
* **Can't reproduce for now** Try fix hang
* **DONE** Remove useless code
* **DONE** Fix warnings and check corresponding versions
* **DONE** Fix dialog according to TODO in the code, p4 get ariables each time on open
* **DONE** Fix plugin file - name, version etc
* **DONE** fix very slow set get in case P4PORT is unknown.

Actual list:
* **DONE** Rename Git to Perforce for every item in project (code)
* **DONE** Fix unit tests and run them
* Fix README.md
	+ add correct links
	+ **DONE** add instructions for connect/disconnect/p4 set
* **DONE** Fix README-marketplace.md
* **DONE** Scroll in the settings dialog
* **DONE** edit AssemblyInfo.cs
* Final: add to marketplace + links


// add to TODO:
line 1: # Perforce Diff Margin [![VS Marketplace](https://vsmarketplacebadge.apphb.com/version/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Rating(Short)](https://vsmarketplacebadge.apphb.com/rating-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Installs(Short))](https://vsmarketplacebadge.apphb.com/installs-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Downloads(Short)))](https://vsmarketplacebadge.apphb.com/downloads-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin)
line 57: Grab it from inside of Visual Studio's Extension Manager searching for **Perforce Diff Margin**, or via the [Extension Gallery link](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin).
line 80-82: 
## Feedback

* Write a [**review**](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin#review-details)
