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
* Content html (literal)
* Content markdown
* Google map
* Page title
* Print to PDF
* Tabstrip (Kendo, Bootstrap): Change the site default at /Sitefinity/Administration/Settings/Advanced/SitefinitySteveMVC 

### Modules
* RemoveHttpHeadersModule

### Helpers
* TODO: Document

### Demo Setup
## Restore the database 

* Restore from from /Demo/App_Data/_database using SQL 2019
* Update the /Demo/App_Data/Configuration/Data.config connection string to be your database


## Troubleshooting
#### The widgets views are not loading *as of* 12.2 
Sitefinity added a new feature, a powershell script under \Builds called ScanControllerContainerAssemblies.ps1.  The idea is that on build it scans all the DLLs in /bin and looks for any that are SF Controllers, 
then knows to load just those instead of everything like it used to. It's outlined in [this blog post](https://www.progress.com/blogs/performance-optimizations-in-sitefinity-12-2_).  The problem is
if you aren't using the EXACT version of Sitefinity this DLL was built against, RandomSiteControlsMVC doesn't get picked up and added to that .json file, and thus SF doesn't load any of the views.

The fix is to replace the default ScanControllerContainerAssemblies.ps1 with mine here: https://github.com/sitefinitysteve/RandomSiteControlsMVC/blob/master/RandomSiteControlsMVC/Build/ScanControllerContainerAssemblies.ps1

All it does differently is pick up any DLLs having this assembly mismatch against a Telerik.Sitefinity .dll and just add it to this .json file.

I'm trying to get Telerik\Progress to fix the script so it'll do something to this effect automatically.


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
* Placeholder

## Migrating from the old hybrid version
* Delete the DLL
* Remove the reference in VS (If it's there)
* Search your project for "RandomSiteControls" and remove all the old refs.  Toolbox.config and VirtualPaths.config will need to be edited
* Find\Replace for "RandomSiteControlsUtil" to "RSCUtil"

## Setting up the demo project
* Database is in \App_Data\_database, attach it to SQL
* Create the Data config file in \App_Data\Sitefinity\Configuration\Data.Config
```
<?xml version="1.0" encoding="utf-8"?>
<dataConfig xmlns:config="urn:telerik:sitefinity:configuration" xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="13.0.7327.0">
	<connectionStrings>
		<add connectionString="data source=(local);UID=rscdemo;PWD=rscdemo;initial catalog=rscdemo" name="Sitefinity" />
	</connectionStrings>
</dataConfig>
```
* Change the login to whatever suits you


## Author
[Created by Sitefinity Steve](https://www.sitefinitysteve.com)

Thanks to Progress\Telerik for making this all possible
