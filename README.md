 # Perforce Diff Margin [![VS Marketplace](https://vsmarketplacebadge.apphb.com/version/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Rating(Short)](https://vsmarketplacebadge.apphb.com/rating-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Installs(Short))](https://vsmarketplacebadge.apphb.com/installs-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) [![Downloads(Short)))](https://vsmarketplacebadge.apphb.com/downloads-short/LaurentKempe.GitDiffMargin.svg)](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin)

Perforce Diff Margin displays live Perforce changes of the currently edited file on Visual Studio margin and scroll bar.

## Features

* Supports Visual Studio 2012 through Visual Studio 2019. Tested on Visual Studio 2015 and Visual Studio 2017.
* Quickly view all current file changes on
    * Left margin
    * Scroll Bars in map and bar mode with and without source overview
        * blue rectangle for modifications
        * green rectangles for new lines
        * red triangles for deletions
        * all colors configurable through Visual Studio Fonts and Colors options
* Undo the change
* Copy the old code into the clipboard
* Copy a part of the old code by selecting it in the popup
* Show the diff in Visual Studio Diff window
* Navigate to previous/next change on the file using user defined keyboard shortcuts or the popup icons
* Open popup with user defined keyboard shortcuts, close with esc key 
* Support Visual Studio 2013 Dark, Light and Blue Theme
* Support zoom
* Diff against something other than HEAD (Advanced users)

![Screenshot](https://farm4.staticflickr.com/3893/15335334635_a88dc1f271.jpg)

Perforce Diff Margin version 1.0.0 is the latest release supporting Visual Studio 2012 it uses p4api.net version 2020.1.193.5313. You can [download it here](https://github.com/laurentkempe/GitDiffMargin/releases/tag/v3.2.2).

## Project idea and code base

The project is a clone of [Git Diff Margin](https://github.com/laurentkempe/GitDiffMargin)
I've adopted existing Git plugin to Perforce control system with some modifications.

## Sponsor

If you use and 💗 Perforce Diff Margin and Git Diff Margin extensions you can [become a sponsor](https://github.com/sponsors/laurentkempe) now!
He is the author of Git Diff Margin and without his work Perforce Diff Margin would not exists! Thank you! 

## Installation

Grab it from inside of Visual Studio's Extension Manager searching for **Perforce Diff Margin**, or via the [Extension Gallery link](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin).

## Video

You might see a short video showing some Git Diff Margin features on the [following page](https://www.flickr.com/photos/laurentkempe/14879945429/).

## Contributing

Visit the [Contributor Guidelines](CONTRIBUTING.md) for details on how to contribute as well as the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md) for details on how to participate.

### Build requirements

* Visual Studio 2017
* Visual Studio SDK
* The built VSIX will work with Visual Studio 2012 to Visual Studio 2019

### Build

Clone the repository and using a git GUI client or via the command line:

```txt
git clone https://github.com/laurentkempe/GitDiffMargin.git
cd GitDiffMargin
```

Open the `GitDiffMargin.sln` solution with Visual Studio 2017+, build and run!

## Feedback

* Write a [**review**](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin#review-details)
* Tweet me [![Follow on Twitter](https://img.shields.io/twitter/url/http/realvizu.svg?style=social&label=@laurentkempe)](https://twitter.com/laurentkempe)

## Credits

Thanks to

* Laurent Kempé [@laurentkempe](https://github.com/laurentkempe) The auther of Git Diff Margin

And other contributors of Git Diff Margin:
* Sam Harwell [@sharwell](https://github.com/sharwell) for all the improvements
* Rick Sladkey [@ricksladkey](https://github.com/ricksladkey) for the fixes
* Ethan J. Brown [@Iristyle](https://github.com/Iristyle) for the first chocolatey package
* [@heinzbeinz](https://github.com/heinzbeinz) for the support of Visual Studio 15 preview
* Jamie Cansdale [@jcansdale](https://github.com/jcansdale) for bugfix
* Charles Milette [sylveon](https://github.com/sylveon) for bugfix
* Gary Ewan Park [@gep13](https://github.com/gep13) for the new chocolatey package
* Duncan Smart [@duncansmart](https://github.com/duncansmart) for bug fix

## Copyright

Copyright 2012 - 2019 Laurent Kempé

Licensed under the [MIT License](LICENSE.md)
