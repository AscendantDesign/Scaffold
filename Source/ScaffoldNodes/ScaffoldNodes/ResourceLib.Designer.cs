﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Scaffold {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceLib {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceLib() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Scaffold.ResourceLib", typeof(ResourceLib).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Audio32 {
            get {
                object obj = ResourceManager.GetObject("Audio32", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Link32 {
            get {
                object obj = ResourceManager.GetObject("Link32", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?s:(?i:(?&lt;filenameextension&gt;(?&lt;filename&gt;[^\\/\?]+)(?!.*(\\|/))\.(?&lt;extension&gt;[^\\/\?]+))(?&lt;parameters&gt;\?.*){0,1})).
        /// </summary>
        internal static string rxFilenameInPath {
            get {
                return ResourceManager.GetString("rxFilenameInPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?i:(?&lt;g&gt;^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$)).
        /// </summary>
        internal static string rxIsGUID {
            get {
                return ResourceManager.GetString("rxIsGUID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?s:(?i:^\s*\[)).
        /// </summary>
        internal static string rxJSONStartArray {
            get {
                return ResourceManager.GetString("rxJSONStartArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?i:(?&lt;pattern&gt;-{0,1}[0-9]+(\.[0-9]+){0,1}(e-{0,1}[0-9]+){0,1})).
        /// </summary>
        internal static string rxNumeric {
            get {
                return ResourceManager.GetString("rxNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 24.
        /// </summary>
        internal static string SocketMediaIconSize {
            get {
                return ResourceManager.GetString("SocketMediaIconSize", resourceCulture);
            }
        }
    }
}
