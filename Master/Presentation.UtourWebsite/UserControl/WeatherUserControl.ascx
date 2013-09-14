<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeatherUserControl.ascx.cs" Inherits="UserControl_WeatherUserControl" %>


<%--<%@ Register Assembly="Manic.Services.Weather" Namespace="Manic.Services.Weather.Controls" TagPrefix="manic" %>--%>
<div id="wx_module_3258">
    <select id="Select1" onchange="fun(this)">
        <option></option>
        <option value="USAR0169">Cairo</option>
        <option>Alex</option>

        <option>Aswan</option>

    </select>
</div>

<div id="wx_module_6114">
   
</div>

<script type="text/javascript">
    function fun(obj) {
        /* Locations can be edited manually by updating 'wx_locID' below.  Please also update */
        /* the location name and link in the above div (wx_module) to reflect any changes made. */
        var wx_locID = obj.value;  //'USAR0169';

        /* If you are editing locations manually and are adding multiple modules to one page, each */
        /* module must have a unique div id.  Please append a unique # to the div above, as well */
        /* as the one referenced just below.  If you use the builder to create individual modules  */
        /* you will not need to edit these parameters. */
        var wx_targetDiv = 'wx_module_6114';

        /* Please do not change the configuration value [wx_config] manually - your module */
        /* will no longer function if you do.  If at any time you wish to modify this */
        /* configuration please use the graphical configuration tool found at */
        /* https://registration.weather.com/ursa/wow/step2 */
        var wx_config = 'SZ=180x150*WX=FHW*LNK=SSNL*UNT=F*BGC=00bff3*MAP=null|null*DN=fcis.somee.com*TIER=0*PID=1316858424*MD5=ddd756b583d5425ca263d6c8d2c8a480';

        document.write('<scr' + 'ipt src="' + document.location.protocol + '//wow.weather.com/weather/wow/module/' + wx_locID + '?config=' + wx_config + '&proto=' + document.location.protocol + '&target=' + wx_targetDiv + '"></scr' + 'ipt>');
    }
    </script>
    <%--<div class="weather-frame">
        <manic:CurrentConditions ID="mcc1" runat="server" Location="EGXX0004" PartnerID="1316858424" Key="FHW" Units="Metric" IconFolder="../Icons" MoonIconType="Gif" CssClass="weather-box" />
    </div>--%>