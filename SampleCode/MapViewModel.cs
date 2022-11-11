using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;

namespace SampleCode
{
    internal class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            // License Key
            string licenseKey = "runtimelite,1000,rud2041528204,none,4N5X0H4AH76HF5KHT183";
            ArcGISRuntimeEnvironment.SetLicense(licenseKey);


            // Do not have to Login to see traffic layer (use token to save crediential)
            //AuthenticationHelper.ApplyTemporaryToken("https://www.arcgis.com/sharing/rest", "Zjdx-AzyG17UV2JptjOgAXi-cCXRrCb5pKG8MJz8Db2ZhtNZ4HfJCR6SvrwwHGNDByF0ZZevp9Hs1S9jVaBiToGLYHeKCJLbWUfikuf6g0DQPIA2j8J5iqeqKnMXDapX5hEbsE5Ik7-5jXb3TkVNkw..");
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
            // Add the ArcGIS Online URL to the authentication helper.
            AuthenticationHelper.RegisterSecureServer("https://www.arcgis.com/sharing/rest");

            /* Display a Map 
            // Create a new map with a 'topographic vector' basemap.
            Map = new Map(BasemapStyle.ArcGISTopographic);
            */

            // Create a new map with a 'topographic vector' basemap.
            var trafficMap = new Map(BasemapStyle.ArcGISTopographic);

            // Create a layer to display the ArcGIS World Traffic service.
            var trafficServiceUrl = "https://traffic.arcgis.com/arcgis/rest/services/World/Traffic/MapServer";
            var trafficLayer = new ArcGISMapImageLayer(new Uri(trafficServiceUrl));


            // Handle changes in the traffic layer's load status
            trafficLayer.LoadStatusChanged += TrafficLayer_LoadStatusChanged;

            // Add the traffic layer to the map's data layer collection.
            trafficMap.OperationalLayers.Add(trafficLayer);

            // Set the view model Map property with the new map.
            this.Map = trafficMap;

        }

        private void TrafficLayer_LoadStatusChanged(object sender, Esri.ArcGISRuntime.LoadStatusEventArgs e)
        {
            // Report the error message
            if (e.Status == Esri.ArcGISRuntime.LoadStatus.FailedToLoad)
            {
                var trafficLayer = (ArcGISMapImageLayer)sender;
                System.Windows.MessageBox.Show(trafficLayer?.LoadError?.Message, "Traffic Layer Load Error");
            }
        }
    }
}
