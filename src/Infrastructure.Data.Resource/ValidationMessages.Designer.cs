﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Data.Resource {
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
    public class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Data.Resource.ValidationMessages", typeof(ValidationMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A {0} deve existir..
        /// </summary>
        public static string AEntidadeDeveExistir {
            get {
                return ResourceManager.GetString("AEntidadeDeveExistir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data inicial deve ser menor do que a data final..
        /// </summary>
        public static string DataInicialMenorFinal {
            get {
                return ResourceManager.GetString("DataInicialMenorFinal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O campo {PropertyName} não pode ser nulo..
        /// </summary>
        public static string NaoPodeSerNulo {
            get {
                return ResourceManager.GetString("NaoPodeSerNulo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O {0} deve existir..
        /// </summary>
        public static string OEntidadeDeveExistir {
            get {
                return ResourceManager.GetString("OEntidadeDeveExistir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O Campo  {PropertyName} deve ter no máximo {MaxLength} caracteres..
        /// </summary>
        public static string TamanhoMaximo {
            get {
                return ResourceManager.GetString("TamanhoMaximo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O Campo  {PropertyName} deve ter no máximo {MinLength} caracteres..
        /// </summary>
        public static string TamanhoMinimo {
            get {
                return ResourceManager.GetString("TamanhoMinimo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O Campo  {PropertyName} excedeu o tamanho máximo permitido..
        /// </summary>
        public static string ValorMaximo {
            get {
                return ResourceManager.GetString("ValorMaximo", resourceCulture);
            }
        }
    }
}