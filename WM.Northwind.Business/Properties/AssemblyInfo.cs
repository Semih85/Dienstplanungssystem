using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.ExceptionAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.PerformansCounterAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("WM.Northwind.Business")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("WM.Northwind.Business")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ExceptionLogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "WM.Northwind.Business.Concrete.Managers.*")]
[assembly: ExceptionLogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "WM.Northwind.Business.Concrete.OptimizationManagers.*")]
//[assembly: PerformansCounterAspect(AttributeTargetTypes = "WM.Northwind.Business.Concrete.Managers.*")]
//[assembly: PerformansCounterAspect(AttributeTargetTypes = "WM.Northwind.Business.Concrete.OptimizationManagers.*")]

//[assembly: LogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "WM.Northwind.Business.Concrete.Managers.EczaneNobet.*")]
//[assembly: CacheRemoveAspect(typeof(MemoryCacheManager), AttributeTargetTypes = "WM.Northwind.Business.Concrete.Managers.*.Insert")]

//kendimiz de saniye cinsinden parametre verebiliriz. Varsayılan değer 5 saniye. 
//[assembly: PerformansCounterAspect(2, AttributeTargetTypes = "WM.Northwind.Business.Concerete.Managers.*")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f30e4964-f6ff-4d3c-a30e-07f2df0d71e9")]

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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
