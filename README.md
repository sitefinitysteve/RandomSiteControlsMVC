# RandomSiteControlsMVC
RandomSiteControls re-written in pure MVC for Sitefinity Feather

## Installation
* Add the RandomSiteControlsMVC Nuget package (Install-Package RandomSiteControlsMVC)
* Run the site, load your toolbox.config and move the widgets and layouts around as needed
> RandomSiteControlsMVC.dll is not compatible with the previous RandomSiteControls.dll, it's one or the other.

## Features
[Watch Video](https://www.youtube.com/watch?v=4pOJaRzoFJM)
[![Feature Preview](https://img.youtube.com/vi/4pOJaRzoFJM/0.jpg)](https://www.youtube.com/watch?v=4pOJaRzoFJM)
### Widgets
* Content html
* Content markdown
* Tabstrip (Kendo, Bootstrap): Change the site default at /Sitefinity/Administration/Settings/Advanced/SitefinitySteveMVC 

### Modules
* RemoveHttpHeadersModule

### Helpers
* TODO: Document

## Troubleshooting
#### How do I modify your views
Feather has you covered, just make the controller name in ~/MVC/Views/Widget and go nuts.  Example ~/MVC/Views/TabStrip/Bootstrap.cshtml

#### My tabstrip is unstyled
I am not injecting any CSS, if your theme is bootstrap you must add the CSS, if you are using kendo, you need KendoCommon and the theme you want

#### I can't find the tabstrip layouts
They are under the "Controls" toolbox menu below your standard grid layouts

#### My Markdown in the preview doesn't match what's on the page
I'm using a Javascript converter in the designer, but using the ServiceStack markdown parser on the server widget.  You'd need to post the question to ServiceStack via Stackoverflow.

## Roadmap
* Disqus
* Modal popup\Fancybox?
* Document Tree List
* Page Title
* Placeholder

## Migrating from the old hybrid version
* Delete the DLL
* Remove the reference in VS (If it's there)
* Search your project for "RandomSiteControls" and remove all the old refs.  Toolbox.config and VirtualPaths.config will need to be edited
* Find\Replace for "RandomSiteControlsUtil" to "RSCUtil"

## Author
[Created by Sitefinity Steve](https://www.sitefinitysteve.com)

Thanks to Progress\Telerik for making this all possible