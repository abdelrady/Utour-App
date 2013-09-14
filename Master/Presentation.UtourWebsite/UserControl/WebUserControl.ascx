<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl.ascx.cs" Inherits="UserControl_WebUserControl" %>

<script language="javascript" type="text/javascript">
    function weatherFunc(value) {
        alert(value);
                $.ajax({
                    url: 'http://en.egypt.travel/ajax/ajax_main/weather',
                    data: { 'city': value },
                    type: "post",
                    success: function (Data) { document.getElementById("MyDiv").innerHTML = Data; }
                });

//        $.post('http://en.egypt.travel/ajax/ajax_main/weather', { city: value }, function (data) {
//            document.getElementById("MyDiv").innerHTML = Data;
//        });
    }
</script>
<form method="post" action="http://en.egypt.travel/ajax/ajax_main/weather">
<select class="input onchange" name="city" >
<option value="abu-simbel" >Abu-Simbel</option>
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

<input type="submit" value="Submit" />
<div id="MyDiv">
</div>

</form>
