// JavaScript Document
function tabsShow(myObj,Num) {
if (myObj.className == "active") return;
var tabObj = myObj.parentNode.id;
var tabLi = document.getElementById(tabObj).getElementsByTagName("li");
for (i=0; i<tabLi.length; i++) {
if (i==Num) {
myObj.className="active";
document.getElementById(tabObj+"conList"+i).style.display="block";
}
else {
tabLi[i].className="";
document.getElementById(tabObj+"conList"+i).style.display="none";
}
}
}