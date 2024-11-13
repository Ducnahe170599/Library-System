﻿#pragma checksum "..\..\..\..\..\Views\admin\BookManagement.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "001B4A72475528B7E379CA21AE73C9922B5CF3A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ManagerLibrary.Views.admin;
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


namespace ManagerLibrary.Views.admin {
    
    
    /// <summary>
    /// BookManagement
    /// </summary>
    public partial class BookManagement : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\Views\admin\BookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCategory;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\Views\admin\BookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbStatus;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Views\admin\BookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Views\admin\BookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBookCount;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\Views\admin\BookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewBooks;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ManagerLibrary;V1.0.0.0;component/views/admin/bookmanagement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cbCategory = ((System.Windows.Controls.ComboBox)(target));
            
            #line 22 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            this.cbCategory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCategory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            this.cbStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbStatus_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            this.txtSearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtSearch_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 33 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnHome_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 39 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnAddBook_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtBookCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.listViewBooks = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            
            #line 71 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnUpdateBook_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 72 "..\..\..\..\..\Views\admin\BookManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnDeleteBook_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

