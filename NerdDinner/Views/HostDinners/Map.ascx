<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdDinner.Models.Domains.HostDinner>" %>

<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>

<script src="/Scripts/NerdDinner.js" type="text/javascript"></script>

<div id="theMap" style="width:520px"></div>
<script type="text/javascript">
//<![CDATA[   
    $(document).ready(function() {
        var latitude = <%: Convert.ToString(Model.DinnerMapLocation.Latitude, CultureInfo.InvariantCulture) %>;
        var longitude = <%: Convert.ToString(Model.DinnerMapLocation.Longitude, CultureInfo.InvariantCulture) %>;
                
        if ((latitude == 0) || (longitude == 0))
            NerdDinner.LoadMap();
        else
            NerdDinner.LoadMap(latitude, longitude, mapLoaded);
    });
      
    function mapLoaded() {
        var title = "<%: Model.DinnerDetail.Title %>";
        var address = "<%: Model.DinnerDetail.Address.Address1 %>";
        
        NerdDinner.LoadPin(NerdDinner._map.GetCenter(), title, address);
        NerdDinner._map.SetZoomLevel(14);
    } 
//]]>
</script>
