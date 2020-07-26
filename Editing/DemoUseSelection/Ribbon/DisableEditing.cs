/*

   Copyright 2020 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using ArcGIS.Desktop.Mapping;

namespace DemoUseSelection.Ribbon
{
	internal class DisableEditing : Button
	{
		protected override void OnClick()
		{
			//Are we editing?
			if (Project.Current.IsEditingEnabled)
			{
				var res = MessageBox.Show("Do you want to disable editing? The demo tools will be disabled",
												"Disable Editing?", System.Windows.MessageBoxButton.YesNoCancel);
				if (res == System.Windows.MessageBoxResult.No ||
						res == System.Windows.MessageBoxResult.Cancel)
				{
					return;
				}
				//we must check for edits
				if (Project.Current.HasEdits)
				{
					res = MessageBox.Show("Save edits?",
												"Save Edits?", System.Windows.MessageBoxButton.YesNoCancel);
					if (res == System.Windows.MessageBoxResult.Cancel)
						return;
					else if (res == System.Windows.MessageBoxResult.No)
						Project.Current.DiscardEditsAsync();
					else
					{
						Project.Current.SaveEditsAsync();
					}
				}
				Project.Current.SetIsEditingEnabledAsync(false);
			}
			else
			{
				MessageBox.Show("Editing is already disabled",
												"Editing Disabled", System.Windows.MessageBoxButton.OK);
			}
		}
	}
}
