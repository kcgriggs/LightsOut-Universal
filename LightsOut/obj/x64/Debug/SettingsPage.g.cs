﻿#pragma checksum "C:\Users\kcgym\OneDrive\Documents\GitHub\LightsOut-Universal\LightsOut\SettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C15DF5905ACF544CF17339C396A5B6EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightsOut
{
    partial class SettingsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // SettingsPage.xaml line 13
                {
                    this.x3RadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.x3RadioButton).Click += this.X3RadioButton_Click;
                }
                break;
            case 3: // SettingsPage.xaml line 14
                {
                    this.x4RadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.x4RadioButton).Click += this.X4RadioButton_Click;
                }
                break;
            case 4: // SettingsPage.xaml line 15
                {
                    this.x5RadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.x5RadioButton).Click += this.X5RadioButton_Click;
                }
                break;
            case 5: // SettingsPage.xaml line 16
                {
                    this.TileColorPicker = (global::Windows.UI.Xaml.Controls.ColorPicker)(target);
                    ((global::Windows.UI.Xaml.Controls.ColorPicker)this.TileColorPicker).ColorChanged += this.ColorPicker_ColorChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

