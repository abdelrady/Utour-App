<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl2.ascx.cs" Inherits="UserControl_WebUserControl2" %>

<script language="javascript" type="text/javascript">
    function weatherFunc(value) {
        alert(value);
        //        $.Post({
        //            url: 'http://en.egypt.travel/ajax/ajax_main/weather',
        //            data: { 'city': value },
        //            type: "post",
        //            success: function (Data) { document.getElementById("MyDiv").innerHTML = Data; }
        //        });

        $.post('http://en.egypt.travel/ajax/ajax_main/weather', { city: value }, function (data) {
            document.getElementById("MyDiv").innerHTML = Data;
        });
    }
    function weatherData(data) {
        document.getElementById("MyDiv").innerHTML = data;
    }
</script>
<select class="input onchange" name="city" onchange="weatherFunc(this.value)" >
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

<div id="MyDiv">
</div>

</form>
