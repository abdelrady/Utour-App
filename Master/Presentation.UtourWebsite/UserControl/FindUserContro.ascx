<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FindUserContro.ascx.cs" Inherits="UserControl_FindUserContro" %>

<div class="tabs sub-block ui-tabs ui-widget ui-widget-content ui-corner-all" id="get-ideas">
          <div class="subcontent">
            <div id="to-do" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
              <form name="ideas_todo_form" id="ideas-todo-form" class="" method="post" action="http://en.egypt.travel/directories/attractions"><ol class="fields"><li><select rel="ajax/ajax_main/filter_product/" target="#todo-products" class="input filter-next" id="todo-cities-filter" name="city">
<option value="23">Abu-Simbel</option>
<option value="29">Ain Sukhna</option>
<option value="3">Alexandria</option>
<option value="4">Aswan</option>
<option value="15">Bahariya Oasis</option>
<option value="13">Cairo</option>
<option value="5">Dahab</option>
<option value="16">Dakhla Oasis</option>
<option value="7">El Gouna</option>
<option value="6">El-Alamein</option>
<option value="412">El-Quseir</option>
<option value="17">Farafra Oasis</option>
<option value="30">Gilf El-Kebir</option>
<option value="8">Hurghada City</option>
<option value="18">Kharga Oasis</option>
<option value="12">Luxor</option>
<option value="27">Makadi Bay</option>
<option value="14">Marsa Alam</option>
<option value="9">Marsa Matrouh</option>
<option value="31">North Coast</option>
<option value="24">Nuweiba</option>
<option value="22">Safaga</option>
<option value="25">Sahl Hasheesh</option>
<option value="10">Sharm El Sheikh</option>
<option value="19">Siwa Oasis</option>
<option value="28">Soma Bay</option>
<option value="11">Taba</option>
<option value="26">Taba Heights</option>
<option selected="selected" value="0">All Destinations</option>
</select></li><li id="todo-products"><select rel="ajax/ajax_main/filter_subproduct/" target="#todo-subproducts" source="#todo-cities-filter" class="input filter-next" id="todo-products-filter" name="product">
<option value="7">Ancient Egypt</option>
<option value="8">Coptic Egypt</option>
<option value="14">Desert and Oases</option>
<option value="22">Diving</option>
<option value="21">Fun &amp; Leisure</option>
<option value="11">Islamic Egypt</option>
<option value="24">Jewish Egypt</option>
<option value="13">Modern Egypt</option>
<option value="20">Nature Exploration</option>
<option value="23">Nile Cruise</option>
<option value="17">Spa &amp; Wellness</option>
<option value="12">Water Activities</option>
<option selected="selected" value="0">All Categories</option>
</select></li><li id="todo-subproducts"></li><li class="buttons"><button class="button" type="submit" name="">Find</button></li></ol><div class="clear"></div></form>            
</div>    
          </div>
        </div>