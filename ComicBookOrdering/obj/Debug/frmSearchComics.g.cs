﻿#pragma checksum "..\..\frmSearchComics.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1E9DACAFE6FF23A27BCAA2C8BA3115D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ComicBookOrdering;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace ComicBookOrdering {
    
    
    /// <summary>
    /// frmSearchComics
    /// </summary>
    public partial class frmSearchComics : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchTitle;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearchComicTitle;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchAuthor;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearchComicAuthor;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchPublisher;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\frmSearchComics.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearchPublisher;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ComicBookOrdering;component/frmsearchcomics.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmSearchComics.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtSearchTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnSearchComicTitle = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\frmSearchComics.xaml"
            this.btnSearchComicTitle.Click += new System.Windows.RoutedEventHandler(this.btnSearchComicTitle_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtSearchAuthor = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnSearchComicAuthor = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\frmSearchComics.xaml"
            this.btnSearchComicAuthor.Click += new System.Windows.RoutedEventHandler(this.btnSearchComicAuthor_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtSearchPublisher = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnSearchPublisher = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\frmSearchComics.xaml"
            this.btnSearchPublisher.Click += new System.Windows.RoutedEventHandler(this.btnSearchPublisher_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

