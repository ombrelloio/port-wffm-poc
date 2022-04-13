using Sitecore.Forms.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebActivatorEx;

//[assembly: Extension]
[assembly: PostApplicationStartMethod(typeof (RazorGeneratorMvcStart), "Start")]
[assembly: ComVisible(false)]
[assembly: AssemblyTitle("Web Forms for Marketers")]
[assembly: AssemblyCompany("Sitecore Corporation")]
[assembly: AssemblyProduct("Web Forms for Marketers 9.0.rev. 180503")]
[assembly: AssemblyCopyright("© Sitecore Corporation 2018")]
[assembly: AssemblyTrademark("Sitecore")]
[assembly: AssemblyInformationalVersion("9.0 rev. 180503")]
[assembly: AssemblyFileVersion("9.0.6240.0")]
[assembly: InternalsVisibleTo("Sitecore.Forms.Core.UnitTests")]
[assembly: AssemblyVersion("9.0.0.0")]
