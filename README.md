# RandomSiteControlsMVC
RandomSiteControls re-written in pure MVC for Sitefinity Feather

## Installation
* Add the RandomSiteControlsMVC Nuget package
> RandomSiteControlsMVC.dll is not compatible with the previous RandomSiteControls.dll, it's one or the other.

## Featuers
### Widgets
* Content html
* Content markdown
* Tabstrip (Kendo, Bootstrap)

### Modules
* RemoveHttpHeadersModule

### Helpers
* TODO: Document

## Troubleshooting
#### My tabstrip is unstyled
I am not injecting any CSS, if your theme is bootstrap you must add the CSS, if you are using kendo, you need KendoCommon and the theme you want

#### I can't find the tabstrip layouts
They are under the "Controls" toolbox menu below your standard grid layouts

#### My Markdown in the preview doesn't match what's on the page
I'm using a Javascript converter in the designer, but using the ServiceStack markdown parser on the server widget.  You'd need to post the question to ServiceStack via Stackoverflow.

## Roadmap
* Modal popup\Fancybox?
* Document Tree List
* Page Title

## Author
[Created by Sitefinity Steve](https://www.sitefinitysteve.com)

Thanks to Progress\Telerik for making this all possible