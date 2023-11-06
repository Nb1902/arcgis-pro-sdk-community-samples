﻿using ArcGIS.Desktop.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfigurationPathsAddIn
{
    //   Copyright 2019 Esri
    //   Licensed under the Apache License, Version 2.0 (the "License");
    //   you may not use this file except in compliance with the License.
    //   You may obtain a copy of the License at

    //       https://www.apache.org/licenses/LICENSE-2.0

    //   Unless required by applicable law or agreed to in writing, software
    //   distributed under the License is distributed on an "AS IS" BASIS,
    //   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    //   See the License for the specific language governing permissions and
    //   limitations under the License. 
    /// <summary>
    /// Interaction logic for ConfigurationPath.xaml
    /// </summary>
    public partial class ConfigurationPaths : ArcGIS.Desktop.Framework.Controls.ProWindow
    {
        private ObservableCollection<string> _chosenConfiguration = null;

        public ConfigurationPaths()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ConfigurationPaths(string currentValue, List<string>availablePaths)
        {
            InitializeComponent();
            cboConfigurationValues.ItemsSource = availablePaths;

            if (currentValue != null && currentValue != "")
            {
                cboConfigurationValues.SelectedItem = currentValue;

                btnDone.IsEnabled = false;
            }

            DataContext = this;
        }


        private RelayCommand _doneCommand = null;
        public ICommand DoneCommand => _doneCommand ?? (_doneCommand = new RelayCommand(() => Done()));

        private void Done()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            result.Add(cboConfigurationValues.SelectedItem.ToString());
            ChosenConfiguration = result;

            Close();
        }

        private void cboConfigurationValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDone.IsEnabled = true;
        }

        public ObservableCollection<string> ChosenConfiguration
        {
            get => _chosenConfiguration;
            set => _chosenConfiguration = value;
        }
    }
}
