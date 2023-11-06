/*

   Copyright 2023 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       https://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapToolWithEmbeddableControl
{
  /// <summary>
  /// This sample demonstrates a MapTool that is using an embeddable control to input wgs84 coordinates that can be searched for an the map.  The found location is marked on the map.
  /// </summary>
  /// <remarks>
  /// 1. Download the Community Sample data (see under the 'Resources' section for downloading sample data)
  /// 1. Make sure that the Sample data is unzipped in c:\data 
  /// 1. The project used for this sample is 'C:\Data\FeatureTest\FeatureTest.aprx'
  /// 1. In Visual Studio click the Build menu. Then select Build Solution.
  /// 1. Launch the debugger to open ArcGIS Pro.
  /// 1. ArcGIS Pro will open, select the FeatureTest.aprx project
  /// 1. Click on the 'Add-ins' tab.  
  /// 1. Click on the 'MapTool With Embeddable' button to enable the map tool.  Click on the search button in the embeddable control.
  /// ![UI](Screenshots/Screen1.png)
  /// 1. The found location is marked on the map.
  /// </remarks>
  internal class Module1 : Module
  {
    private static Module1 _this = null;

    /// <summary>
    /// Retrieve the singleton instance to this module here
    /// </summary>
    public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("MapToolWithEmbeddableControl_Module");

    public static MapToolWithEmbeddableControl MapToolWithEmbeddableControl { get; internal set; }

    #region Overrides
    /// <summary>
    /// Called by Framework when ArcGIS Pro is closing
    /// </summary>
    /// <returns>False to prevent Pro from closing, otherwise True</returns>
    protected override bool CanUnload()
    {
      //TODO - add your business logic
      //return false to ~cancel~ Application close
      return true;
    }

    #endregion Overrides

  }
}
