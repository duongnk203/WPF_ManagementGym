// Updated by XamlIntelliSenseFileGenerator 7/18/2024 2:25:01 AM
#pragma checksum "..\..\..\..\Home\HomePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "741346DCE8344304A604CB9ED6A512CB662D122F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MainWindow.Home;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MainWindow.Home
{


    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MainWindow;V1.0.0.0;component/home/homepage.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Home\HomePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.DataGrid dgListClasses;
        internal System.Windows.Controls.Label lblListClasses;
        internal System.Windows.Controls.TextBox txtClassId;
        internal System.Windows.Controls.TextBox txtClassName;
        internal System.Windows.Controls.TextBox txtDescription;
        internal System.Windows.Controls.Button btnCreateClass;
        internal System.Windows.Controls.Button btnUpdateClass;
        internal System.Windows.Controls.Button btnDeleteClass;
        internal System.Windows.Controls.Label lblClassStatus;
        internal System.Windows.Controls.ComboBox cboStatus;
        internal System.Windows.Controls.Label lblTrainerId;
        internal System.Windows.Controls.ComboBox cboTrainer;
        internal System.Windows.Controls.ComboBox cboSchedule;
        internal System.Windows.Controls.Label lblNumber;
        internal System.Windows.Controls.TextBox txtNumber;
        internal System.Windows.Controls.Label lblGymManagement;
        internal System.Windows.Controls.Label lblNumberClass;
        internal System.Windows.Controls.Label lblNumberMember;
        internal System.Windows.Controls.Label lblNumberTrainer;
    }
}

