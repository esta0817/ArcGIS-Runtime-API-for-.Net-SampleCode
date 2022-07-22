using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleCode
{
    internal class AccessServicesWithOAuth : INotifyPropertyChanged
    {
        public AccessServicesWithOAuth()
        {
            SetupMap();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                OnPropertyChanged();
            }
        }

        private void SetupMap()
        {

            /* Display a Map 
            // Create a new map with a 'topographic vector' basemap.
            Map = new Map(BasemapStyle.ArcGISTopographic);
            */

            // Create a new map with a 'topographic vector' basemap.
            var trafficMap = new Map(BasemapStyle.ArcGISTopographic);

            // Create a layer to display the ArcGIS World Traffic service.
            var trafficServiceUrl = "https://traffic.arcgis.com/arcgis/rest/services/World/Traffic/MapServer";
            var trafficLayer = new ArcGISMapImageLayer(new Uri(trafficServiceUrl));

            // Add the traffic layer to the map's data layer collection.
            trafficMap.OperationalLayers.Add(trafficLayer);

            // Set the view model Map property with the new map.
            this.Map = trafficMap;

        }
    }
}
