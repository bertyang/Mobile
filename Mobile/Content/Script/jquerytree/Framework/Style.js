var Effect = {};
Effect.setAlpha = function(element, opacity){//����͸����
	opacity=Math.round(opacity * 1000)/1000;
	var	style = getElement(element).style;
	style.opacity = style.MozOpacity = style.KhtmlOpacity = opacity / 100;
	style.filter = "alpha(opacity=" + opacity + ")";
}

Effect.clear = function(element){//���ָ��Ԫ�صĶ���Ч��
	var	interval = ["size", "scroll", "move", "fade", "color"],
		index = interval.length;
	while(index--)
		clearInterval(getElement(element).effect[interval[index]]);
}

Effect.color = function(element, style, start, end, speed, callback){//��ɫ���䣺style:Ҫ�ı��style���� "color"��"backgroundColor"��start:��ʼ��ɫ ��#000000��end:������ɫ ��#FFFFFF��speed:�ٶ� 1-100 Խ��Խ�죻
	var speed=speed||10;
	end = Effect.hex2RGB(end);
	clearInterval(getElement(element).effect.color);
	element.effect.color = setInterval(function(){
		var	color = Effect.hex2RGB(start),
			index = 3;
		while(index--)
			color[index] = getEnd(color[index], end[index], speed);
		element.style[style] = start = Effect.RGB2hex(color);
		if("" + color == "" + end)
			getCallback(element, "color", callback);
	}, 10);
}

Effect.fade = function(element, start, end, speed, callback){//͸���Ƚ��䣺start:��ʼ͸���� 0-100��end:����͸���� 0-100��speed:�ٶ�0.1-100
	var speed=speed||1;
	clearInterval(getElement(element).effect.fade);
	element.effect.fade = setInterval(function(){
		start = getEnd(start, end, speed);
		Effect.setAlpha(element, start);
		//Console.log("����Ԫ��",element.id," ͸���ȵ�",element.style.opacity);
		if(start == end)
			getCallback(element, "fade", callback);
	}, 10);
}

Effect.move = function(element, position, speed, callback){//�ƶ���ָ��λ�ã�position:�ƶ���ָ��left��top ��ʽ{x:200, y:250}��speed:�ٶ� 1-100
	var speed=speed||10;
	var	start = Effect.getPosition(getElement(element));
	getSetInterval(element, "move", speed / 100, start, position, ["x", "y"], "setPosition", callback);
}

Effect.setPosition = function(element, position){//����λ��
	var	style = getElement(element).style;
	style.position = "absolute";
	style.left = Math.round(position.x) + "px";
	style.top = Math.round(position.y) + "px";
}

Effect.scroll = function(element, speed, callback){//���ڹ�����ָ��Ԫ�ش���speed:�ٶ� 1-100
	var speed=speed||10;
	function scroll(position){
		return document.documentElement ? document.documentElement[position] : document.body[position];
	};
	var	start = Effect.getScroll(),
		end = {x:start.x, y:Math.min(Effect.getPosition(element).y, Math.max(scroll("offsetHeight"), document.body.offsetHeight) - Math.min(scroll("clientHeight"), document.body.clientHeight))};
	getSetInterval(getElement(effect), "scroll", speed / 100, start, end, ["x", "y"], "setScroll", callback ? function(){callback.call(element)} : null);
}

Effect.size = function(element, size, speed, callback){//�����䣺size:Ҫ�ı䵽�ĳߴ� ��ʽ {width:400, height:250}��{width:400}��{height:250}��speed:�ٶ� 1-100
	var speed=speed||2;
	var	start = Effect.getSize(getElement(element)),
		tmp = window.opera;
	if(!/msie/i.test(navigator.userAgent) || (tmp && parseInt(tmp.version()) >= 9)){//���ie��border-contentʽ��ģ�ͣ�ʹ�ø��ӵ�$width��$height����
		if(size.$width)
			start.width -= size.$width;
		if(size.$height)
			start.height -= size.$height;
		if(size.width$)
			size.width -= size.width$;
		if(size.height$)
			size.height -= size.height$;
	};
	element.style.overflow = "hidden";
	var arrStyle=[];
	if(size.width)arrStyle.push("width");
	if(size.height)arrStyle.push("height");
	getSetInterval(element, "size", speed / 100, start, size, arrStyle, "setSize", callback);
}

Effect.RGB2hex = function(color){//��Effect.RGB2hex([255, 0, 0])  ���� #FF0000
	function tmp(index){
		var	tmp = color[index].toString(16);
		return tmp.length == 1 ? "0" + tmp : tmp;
	};
	return "#" + tmp(0) + tmp(1) + tmp(2);
}
Effect.hex2RGB = function(color){//��Effect.RGB2hex("#FF0000")  ���� [255, 0, 0]
	function tmp(index){
		return color.charAt(index);
	};
	color = color.substring(1);
	if(color.length == 3)
		color = tmp(0) + tmp(0) + tmp(1) + tmp(1) + tmp(2) + tmp(2);
	return [parseInt(tmp(0) + tmp(1), 16), parseInt(tmp(2) + tmp(3), 16), parseInt(tmp(4) + tmp(5), 16)];
}

Effect.getPosition = function(element){//ȡԪ�����꣬��Ԫ�ػ����ϲ�Ԫ������position relative��Ӧ��getpos(��Ԫ��).y-getpos(��Ԫ��).y
	return $E.getPosition(element);
}

Effect.getScroll = function(){
	function scroll(position, scroll){
		return (document.documentElement ? document.documentElement[position] : w[scroll] || document.body[position]) || 0;
	};
	return {x:scroll("scrollLeft", "pageXOffset"), y:scroll("scrollTop", "pageYOffset")};
}

Effect.setScroll = function(element, position){
	window.scrollTo(position.x, position.y);
}

Effect.getSize = function(element){
	return {width:element.offsetWidth, height:element.offsetHeight};
}

Effect.setSize = function(element, size){
	var	style = element.style;
	if(size.width)style.width = Math.round(size.width) + "px";
	if(size.height)style.height = Math.round(size.height) + "px";
}

function getCallback(element, interval, callback){
	clearInterval(element.effect[interval]);
	if(callback)
		callback.call(element);
};

function getElement(element){
	if(!element.effect)
		element.effect = {color:0, drag:{}, fade:0, move:0, scroll:0, size:0};
	return element;
};

function getEnd(x, y, speed){
	return x < y ? Math.min(x + speed, y) : Math.max(x - speed, y);
};

function getSetInterval(element, interval, speed, start, position, style, tmp, callback){
	clearInterval(element.effect[interval]);
	element.effect[interval] = setInterval(function(){
		for(i=0;i<style.length;i++){
			start[style[i]] += (position[style[i]] - start[style[i]]) * speed;
		}
		Effect[tmp](element, start);
		for(i=0;i<style.length;i++){
			if(Math.round(start[style[i]]) == position[style[i]]){
				if(i!=style.length-1)continue;
			}else{
				break;
			}
			Effect[tmp](element, position);
			getCallback(element, interval, callback);
		}

	}, 10);
};

Effect.NextID = 0;
Effect.initCtrlStyle = function(ele){
  ele = $(ele);
	if(!ele.InitCtrlStyleFlag){//�����γ�ʼ��
		var eletype = ele.type;
	  switch (eletype) {
	  case 'textarea':
	    if (ele.disabled == true) {
	      ele.addClassName("disabled");
	    };
	    break;  
	  case 'text':
	  case 'password':
	  case '':
	  	ele.addClassName("inputText");
	    ele.onmouseenter = function() {
	      this.style.borderColor = "#00aaee";
	    };
	    ele.onmouseleave = function() {
	      this.style.borderColor = "";
	    };
	 
		ele.onfocusFunc=ele.onfocus;
	    ele.onfocus = function() {
				if (typeof (ele.onfocusFunc) == "function"){
					try{
						ele.onfocusFunc();
					} catch(e){}
				}
	      this.style.borderColor = "#ff8800";
		  this.addClassName("inputTextFocus");
	      this.onmouseenter = null;
	      this.onmouseleave = null;
	    };
	
	    ele.onblurFunc=ele.onblur;
	    ele.onblur = function() {
			if (typeof (ele.onblurFunc) == "function"){
				try{
					ele.onblurFunc();
				} catch(e){}
			}
	      this.style.borderColor = "";
		  this.removeClassName("inputTextFocus");
	      this.onmouseenter = function() {
	        this.style.borderColor = "#00aaee";
	      };
	      this.onmouseleave = function() {
	        this.style.borderColor = "";
	      };
	    };
	    if (ele.disabled == true) {
	      ele.addClassName("disabled");
	    };
	
	    break;
	  case 'submit':
	  case 'reset':
	  case 'button':
	  	if(ele.className==""&&!(/border|background/).test(ele.style.cssText)){//��û�ж���class��û�ж���background������¶԰�ťӦ��Բ����ʽ
			ele.addClassName("inputButton");
			ele.hideFocus = true;
			if (ele.parentElement.tagName != "A") {
			  ele.outerHTML = "<a href='javascript:void(0);' ztype='zInputBtnWrapper' class='zInputBtn' hidefocus='true' tabindex='-1'>" + ele.outerHTML + "<\/a>";
			}
		}
	    break;
	  case 'checkbox':
	  	ele.addClassName("inputCheckbox");
	    break;
	  case 'radio':
	  	ele.addClassName("inputRadio");
	    break;
	  case 'file':
	  	ele.addClassName("inputFile");
	    break;
	  case 'image':
	 	 ele.addClassName("inputImage");
	    break;
	  default:
	    ;
	  }
	  ele.InitCtrlStyleFlag = true;
	}
}

//���еĿؼ���ʼ���붼Ҫд��һ�����������
Effect.initChildren = function(ele){
	ele = $(ele);
	var arr = ele.$T("div");
	var sarr = [];
	arr.each(function(div){
		var ztype = $(div).$A("ztype");
		if(ztype&&ztype.toLowerCase()=="select"){
			Selector.initHtml(div);
			sarr.push(div);
		}
	});
	sarr.each(function(ele){
		try{
			Selector.initMethod(ele);
		}catch(ex){
			Console.log(ex);
		}
		if(ele.Items.length>10){
			$(ele.id+"_ul").style.height = "15em";
		}
	});
	arr = ele.$T("input").concat(ele.$T("textarea"));
	arr.each(Effect.initOneCtrl);
}

Effect.initOneCtrl = function(ele){
	Effect.initCtrlStyle(ele); //Gecko��Ҳ����������,��Ȼ��ǰ��focus�¼��ᱻ�ĸ�
	DateTime.initCtrl(ele);
	Verify.initCtrl(ele);
}

Page.onReady(function(){
	Effect.initChildren(document.body);
},1);

/********************************************
IE6�£��ڡ��ϸ�ģʽ���º����������ͬ���������һ����֣����´�������������
1��ҳ������ʱǿ������ҳ��overflowX="hidden"
2��ҳ���������ʱ�����ҳ�������Ƿ���ڴ��ڿ�ȣ�Ȼ��overflowX="auto"�����ֺ��������
3��ҳ��������ɺ�50���룬�ٴμ���Ƿ���Ҫ���ֺ��������
4����ie6��ҳ��ߴ�仯ʱ�ἤ��window.resize�¼�����ʱ�ٴμ���Ƿ���Ҫ���ֺ��������
********************************************/
if(window.frameElement && isIE6 && !isQuirks){
	var htmlDom=document.getElementsByTagName('HTML')[0];
	if(htmlDom.style.overflow=="")htmlDom.style.overflow="auto";
	if(htmlDom.style.overflowY=="")htmlDom.style.overflowY="auto";
	if(htmlDom.style.overflowX=="")htmlDom.style.overflowX="hidden";//1
}
Effect.scrollFixOnresize=false;
Effect.ie6ScrollFix=function(){
	if(isIE6 && !isQuirks){
		var htmlDom=document.getElementsByTagName('HTML')[0];
		var sw = Math.max(document.documentElement.scrollWidth, document.body.scrollWidth);
		if((window.frameElement&&sw<window.frameElement.offsetWidth+30)||(!window.frameElement&&sw<document.documentElement.clientWidth+30)){
			htmlDom.style.overflowX="hidden";
			if(!Effect.scrollFixOnresize){
				if(window.frameElement){
					onWindowResize(Effect.runIe6ScrollFix);//4
				}
				Effect.scrollFixOnresize==true;
			}
		}else{//2
			htmlDom.style.overflowX="auto";
		}
		var sh = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight);
		if((window.frameElement&&sh > window.frameElement.offsetHeight)||(!window.frameElement&&sh>document.documentElement.clientHeight)){
			htmlDom.style.overflowY="scroll";
			htmlDom.style.overflowX="hidden";
		}else{
			htmlDom.style.overflowY="auto";
		}
	}
}
Effect.runIe6ScrollFix=function(){
	setTimeout(Effect.ie6ScrollFix, 50)//3
}
Page.onLoad(function(){
	Effect.ie6ScrollFix();
	Effect.runIe6ScrollFix();
},1);
