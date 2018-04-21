var W = screen.width;//取得屏幕分辨率宽度
var H = screen.height;//取得屏幕分辨率高度

function M(id){
	return document.getElementById(id);//用M()方法代替document.getElementById(id)
}
function MC(t){
	return document.createElement(t);//用MC()方法代替document.createElement(t)
};

//判断浏览器是否为IE
function isIE(){
   return (document.all && window.ActiveXObject && !window.opera) ? true : false;
} 

//取得页面的高宽
function getBodySize(){
    var bodySize = [];
    var currentScrollWidth = document.body.scrollWidth;
    var currentClientWidth = document.body.clientWidth;
    var currentScrollHeight = document.body.scrollHeight;
    var currentClientHeight = document.body.clientHeight;
   
	with(document.documentElement) {
	    bodySize[0] = (currentScrollWidth > currentClientWidth) ? currentScrollWidth : currentClientWidth; //如果滚动条的宽度大于页面的宽度，取得滚动条的宽度，否则取页面宽度
	    //如果滚动条的高度大于页面的高度，取得滚动条的高度，否则取高度
	    bodySize[1] = (currentScrollHeight > currentClientHeight) ? currentScrollHeight : currentClientHeight;
    }    
	return bodySize;
}

//创建遮盖层
function popCoverDiv(){
	if (M("cover_div")) 
	{
		//如果存在遮盖层，则让其显示 
		M("cover_div").style.display = 'block';
	} 
	else
	{				
		//否则创建遮盖层
		var coverDiv = MC('div');
		document.body.appendChild(coverDiv);
		coverDiv.id = 'cover_div';		
		with(coverDiv.style) 
		{
		    position = 'absolute';
		    background = '#CCCCCC';
		    left = '0px';
		    top = '0px';
		    var bodySize = getBodySize();
		    width = bodySize[0] + 'px';
		    height = bodySize[1] + 'px';
		    zIndex = 10000;
		    if (isIE()) {
		        filter = "Alpha(Opacity=20)"; //IE逆境
		    }
		    else {
		        opacity = 0.2;
		    }
		}
		
		/*var frame = MC('iframe');
		M('cover_div').appendChild(frame);
		frame.id = 'cover_iframe';
		frame.src = "about:blank";
		frame.frameborder = 0;
		with (frame.style) {
		    position = 'absolute';		    
		    left = '0px';
		    top = '0px';
		    width = "100%";
		    height = "100%";
		    zIndex = "-1";		    
		}	*/			
	}
}

//打开DIV层
function openCover()
{
	popCoverDiv();
	void(0);
}

//关闭DIV层
function closeCover()
{    
	M("cover_div").style.display = 'none';
	void(0);
}

function setHeight()
	{
		/*
		var content = document.all.t1.value;
		alert(content);
		var arrRows = content.split("\n");
		alert(arrRows.length);
		document.all.t1.rows = arrRows.length ;
		*/	
		
		var arrTextAreas = document.getElementsByTagName("textArea");
		for ( i = 0; i < arrTextAreas.length; i++)
		{		
			//var arrRows = arrTextAreas[i].value.split("\n");			
			//arrTextAreas[i].rows = arrRows.length ;					
			arrTextAreas[i].style.height = arrTextAreas[i].scrollHeight;						
		}
	}
	
function removeDisable()
{		
	var arrInputs = document.getElementsByTagName("input");
	for ( i = 0; i < arrInputs.length; i++)
	{			
		if (arrInputs[i].disabled)
			arrInputs[i].disabled = false;
	}
	
	//select 
	var arrSelectElements = document.getElementsByTagName("select");
	for ( i = 0; i < arrSelectElements.length; i++)
	{			
		if (arrSelectElements[i].disabled)
			arrSelectElements[i].disabled = false;
	}
	
	//textarea
	var arrTextArea = document.getElementsByTagName("TextArea");
	for ( i = 0; i < arrTextArea.length; i++)
	{			
		if (arrTextArea[i].disabled)
			arrTextArea[i].disabled = false;
	}

	//span(主要针对radiobutton checkbox)
	var arrSpan = document.getElementsByTagName("span");
	for (i = 0; i < arrSpan.length; i++) 
	{
	    if (arrSpan[i].disabled)
	        arrSpan[i].disabled = false;
	}

}

function removeSubEditArea() 
{
    try 
    {
        var elements = $("tr")
        for (var i = 0; i < elements.length; i++) 
        {
            var trID = elements[i].id;
            if (trID.indexOf("_EditArea") != -1) 
            {
                document.getElementById(trID).style.display = "none";
                document.getElementById(trID).nextSibling.style.display = "none";
            }
        }    
    }
    catch (ex) 
    {
    }
}

function replaceDropDownList() 
{
    try 
    {
        var elements = $("select");
        for (var i = 0; i < elements.length; i++) 
        {
            //var node = elements[i];
            if ($(elements[i]).parent().length > 0) 
            {
                var parNode = $(elements[i]).parent()[0];
                var lableNodesLength = $("#" + elements[i].id + "_Label").length;
                if (lableNodesLength == 0) 
                {
                    //alert($(parNode).contains("Label"));
                    var labelNode = $("<Label>" + elements[i].value + "</Label>")
												.attr("id", elements[i].id + "_Label")
												.css("font-size", "9pt")
												.css("font-family","Arial")
												.appendTo(parNode);
                    elements[i].style.display = "none";
                }
                else 
                {
                    var labelNode = $("#" + elements[i].id + "_Label")[0];
                    labelNode.style.display = "inline";
                    elements[i].style.display = "none";
                }
            }
        }
    }
    catch (ex) 
    {
    }
}

function getInternetExplorerVersion() 
{
    var rv = -1; // Return value assumes failure
    if (navigator.appName == 'Microsoft Internet Explorer') 
    {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

function isIE6() 
{
    if (!(getInternetExplorerVersion() > -1 && getInternetExplorerVersion() > 6.0)) {
        var allTR = document.getElementsByTagName("tr");
        for (k = 0; k < allTR.length; k++) {
            if (allTR[k].getAttribute("region") != null && allTR[k].getAttribute("region").indexOf("_") == -1) {
                var allElements = allTR[k].getElementsByTagName("*");
                for (var i = 0; i < allElements.length; i++) {
                    var widthValue = "";
                    if (allElements[i].getAttribute("width") != null && allElements[i].getAttribute("width") != "" && allElements[i].getAttribute("width") != 0) {
                        try { widthValue = allElements[i].getAttribute("width").replace("px", ""); } catch (ex) { }
                    }
                    else {
                        widthValue = allElements[i].style.width.replace("px", "");
                    }
                    if (widthValue.indexOf("%") == -1 && widthValue > 0) {
                        if (allElements[i].type == "text") {
                            allElements[i].style.width = parseInt(widthValue) * 5 / 9 + "px";
                        }
                        else {
                            allElements[i].style.width = parseInt(widthValue) * 2 / 3 + "px";
                        }
                    }
                    else if (widthValue != "") {
                        widthValue = widthValue.replace("%", "");
                        if (allElements[i].type == "text") {
                            allElements[i].style.width = widthValue * 5 / 9 + "%";
                        }
                        else {
                            allElements[i].style.width = widthValue * 2 / 3 + "%";
                        }
                    }
                }
            }
            else if (allTR[k].getAttribute("region") != null && allTR[k].getAttribute("region").indexOf("_") != -1) {
                var allTable = allTR[k].getElementsByTagName("table");
                for (var j = 0; j < allTable.length; j++) {
                    if (allTable.length > 0) {
                        allTable[0].style.width = 100 * 2 / 3 + "%";
                    }
                }
            }
        }

//        document.getElementById("FlowER_FORM_HEADER").setAttribute("align", "left");
//        document.getElementById("FlowER_FORM_FOOTER").setAttribute("align", "left");
//        document.getElementById("FlowER_FORM_HEADER").style.width = 100 * 2 / 3 + "%";
//        document.getElementById("FlowER_FORM_FOOTER").style.width = 100 * 2 / 3 + "%";
    }
}

function printFunction() {
    isIE6();     
	setHeight();							
	openCover();
	removeDisable();
	removeSubEditArea();
	replaceDropDownList();
	closeCover();
	window.print();
	openCover();	
}



    

