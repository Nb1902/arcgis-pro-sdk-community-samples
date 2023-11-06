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
using ArcGIS.Desktop.Internal.Mapping;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINApiSamples.ProximityAnalysis
{
  internal class GetNearestNode : MapTool
  {
    private TinLayer _tinLayer;
    private GraphicsLayer _graphicsLayer;

    public GetNearestNode()
    {
      IsSketchTool = true;
      SketchType = SketchGeometryType.Point;
      SketchOutputMode = SketchOutputMode.Map;
    }

    protected override Task OnToolActivateAsync(bool active)
    {
      _tinLayer = MapView.Active.Map.GetLayersAsFlattenedList().OfType<TinLayer>().FirstOrDefault();
      _graphicsLayer = MapView.Active.Map.TargetGraphicsLayer;
      return base.OnToolActivateAsync(active);
    }

    protected async override Task<bool> OnSketchCompleteAsync(Geometry geometry)
    {
      var selectedPoint = geometry as MapPoint;
      await QueuedTask.Run(() => {
        if (_tinLayer == null) return;
        if (_graphicsLayer == null) return;
        if (!Module1.IsView2D()) return;

        using (var tinDataset = _tinLayer.GetTinDataset())
        {
          using (var node = tinDataset.GetNearestNode(selectedPoint))
          {
            var edgeGeometry = MapPointBuilderEx.CreateMapPoint(node.Coordinate3D, MapView.Active.Map.SpatialReference);
            var nodeSymbol = SymbolFactory.Instance.ConstructPointSymbol(ColorFactory.Instance.RedRGB, 6);
            nodeSymbol.UseRealWorldSymbolSizes = true;
            _graphicsLayer.AddElement(edgeGeometry, nodeSymbol, "Nearest Node");
          }            
        }         
      });
      return true;
    }
  }
}
