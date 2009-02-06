// JavaScript Document
var leftHeight = document.getElementById("left").offsetHeight;
var rightHeight = document.getElementById("right").offsetHeight;
if (leftHeight>rightHeight) {
	document.getElementById("right").style.height=leftHeight+"px";
	}
	else
	{
	document.getElementById("left").style.height=rightHeight+"px";
		}