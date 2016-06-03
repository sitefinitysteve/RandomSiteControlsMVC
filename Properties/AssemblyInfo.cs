using RandomSiteControlsMVC;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RandomSiteControlsMVC")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RandomSiteControlsMVC")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ControllerContainer]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("5d958857-8a85-415b-b1db-97dc647af92c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("9.0.6000.0")]
[assembly: AssemblyFileVersion("9.0.6000.0")]

//INSTALLER
[assembly: PreApplicationStartMethod(typeof(Installer), "PreApplicationStart")]

//Scripts
[assembly: WebResource("RandomSiteControlsMVC.Scripts.Angular.angular-showdown.js", "text/javascript")]
[assembly: WebResource("RandomSiteControlsMVC.Scripts.Markdown.ng-showdown.min.js", "text/javascript")]
[assembly: WebResource("RandomSiteControlsMVC.Scripts.Markdown.showdown.min.js", "text/javascript")]
[assembly: WebResource("RandomSiteControlsMVC.Scripts.test.js", "text/javascript")]