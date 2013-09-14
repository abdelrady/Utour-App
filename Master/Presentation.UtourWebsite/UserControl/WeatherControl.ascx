<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeatherControl.ascx.cs" Inherits="UserControl_WeatherControl" %>

<head>
<style type="text/css">
#weather .intro {
    font-size: 0.9em;
}
#weather .intro .date {
    text-transform: uppercase;
}
#weather .content-in {
    position: relative;
}
#weather form .fields {
    font-size: 0.9em;
    position: absolute;
    right: 1em;
    top: 1em;
    width: 90px;
}
#weather form .buttons {
    display: none;
}
#water-temp .label, #sea_temperature {
    float: left;
    margin: 0 0.5em 0 0;
}
#sea_temperature td {
    font-size: 12px !important;
}
.sea_temperature1 {
    line-height: 1.2em;
}
.sea_temperature1, .sea_temperature1 tr, .sea_temperature1 tbody, .sea_temperature1 td {
    display: block;
    float: left;
    line-height: 1em;
}
#sea_temperature .sea_temperature2 {
    display: block;
    line-height: 1.2em;
}
.sidebar-block .view-all {
    background: url("../images/ltr/arrow.png") no-repeat scroll left center transparent;
    float: right;
    outline: medium none;
    padding-left: 1em;
}
#weather-list {
    padding: 8px 0;
}
.weather-info {
    margin: 0;
    min-height: 46px;
    overflow: hidden;
    padding: 0 1em;
}
.weather-info .weather-icon {
    border-right: 1px solid #CEC7BD;
    float: left;
    height: 32px;
    padding: 0 0.5em 0 0;
    width: 32px;
}
.weather-info .weather-temps {
    float: left;
    margin: 0;
    padding: 0 0.5em;
}
.weather-info .weather-temps > span {
    background: url("../images/weather/sprite-highlow-temp.png") no-repeat scroll left top transparent;
    display: block;
    line-height: 15px;
    padding: 0 0 0 15px;
}
.weather-info .weather-temps > span.low {
    background-position: left bottom;
}
.weather-info .weather-temps .low .value {
    color: #1A7699;
}
.weather-info .weather-temps .high .value {
    color: #C72E10;
}
.weather-info .day {
    clear: both;
    font-size: 11px;
    margin: 0;
    padding: 0;
    position: relative;
    top: 5px;
}
#weather-day-0.weather-info .day {
    display: none;
    margin-top: 8px;
}
#weather .ajax-result {
    background: url("../images/weather-result-bg.jpg") repeat-y scroll center top #E4DED3;
    border: 1px solid #C5B6A9;
    height: 46px;
    margin: 1em 0 0;
    min-height: 46px;
    overflow: hidden;
    padding: 0;
}
#weather .ajax-result.expanded {
    height: auto;
}
#weather .view-all .less, #weather .view-all.expanded .more {
    display: none;
}
#weather .view-all.expanded .less {
    display: inline;
}
</style>
</head>

<select class="input onchange" name="city">
<option value="abu-simbel">Abu-Simbel</option>
<option value="alexandria">Alexandria</option>
<option value="aswan">Aswan</option>
<option value="bahariya-oasis">Bahariya Oasis</option>
<option selected="selected" value="cairo">Cairo</option>
<option value="dakhla-oasis">Dakhla Oasis</option>
<option value="farafra-oasis">Farafra Oasis</option>
<option value="hurghada-city">Hurghada City</option>
<option value="kharga-oasis">Kharga Oasis</option>
<option value="luxor">Luxor</option>
<option value="marsa-matrouh">Marsa Matrouh</option>
<option value="sharm-el-sheikh">Sharm El Sheikh</option>
<option value="siwa-oasis">Siwa Oasis</option>
<option value="taba">Taba</option>
</select>




<div id="weather" class="sidebar-block">
<span class="corner"></span>
<h2 class="title grad1">
<span class="grad2">
<span class="grad3">Weather</span>
</span>
</h2>
<div class="content grad1">
<span class="arrow"></span>
<div class="grad2">
<div class="content-in grad3">
<div class="contents">
<p class="intro">
Today |
<span class="date">04-06-2012</span>
</p>
<form id="weather-form" class="ajax" name="weather_form" rel="#weather-result" method="post" action="http://en.egypt.travel/ajax/ajax_main/weather">
<ol class="fields">
<li> 
<%--$("#city").val
$("#div1").html(data);--%>
<%--    <asp:DropDownList ID="DropDownList1" runat="server" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>--%>

<%--<select class="input onchange" name="city">
<option value="abu-simbel">Abu-Simbel</option>
<option value="alexandria">Alexandria</option>
<option value="aswan">Aswan</option>
<option value="bahariya-oasis">Bahariya Oasis</option>
<option selected="selected" value="cairo">Cairo</option>
<option value="dakhla-oasis">Dakhla Oasis</option>
<option value="farafra-oasis">Farafra Oasis</option>
<option value="hurghada-city">Hurghada City</option>
<option value="kharga-oasis">Kharga Oasis</option>
<option value="luxor">Luxor</option>
<option value="marsa-matrouh">Marsa Matrouh</option>
<option value="sharm-el-sheikh">Sharm El Sheikh</option>
<option value="siwa-oasis">Siwa Oasis</option>
<option value="taba">Taba</option>
</select>--%>
</li>
<li class="buttons">

</li>
</ol>
<div id="weather-result" class="ajax-result expanded">
<ul id="weather-list">
<li id="weather-day-0" class="weather-info">
<h4 class="day">Sunday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 43°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 28°C </span>
</span>
</p>
</li>
<li id="weather-day-1" class="weather-info">
<h4 class="day">Monday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 43°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 27°C </span>
</span>
</p>
</li>
<li id="weather-day-2" class="weather-info">
<h4 class="day">Tuesday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 41°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 23°C </span>
</span>
</p>
</li>
<li id="weather-day-3" class="weather-info">
<h4 class="day">Wednesday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 38°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 23°C </span>
</span>
</p>
</li>
<li id="weather-day-4" class="weather-info">
<h4 class="day">Thursday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 40°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 24°C </span>
</span>
</p>
</li>
<li id="weather-day-5" class="weather-info">
<h4 class="day">Friday</h4>
<img class="weather-icon" title="Clear" alt="Clear" src="http://en.egypt.travel/includes/images/weather/clear.png">
<p class="weather-temps">
<span class="high">
High Temp
<span class="value"> 41°C </span>
</span>
<span class="low">
Low Temp
<span class="value"> 23°C </span>
</span>
</p>
</li>
</ul>
</div>
</form>
<a class="view-all expanded" href="http://en.egypt.travel/whether/city/cairo/week">
<span class="more">more</span>
<span class="less">less</span>
</a>
</div>
<div class="clear"></div>
</div>
</div>
</div>
</div>