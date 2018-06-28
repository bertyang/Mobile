$(function () {
    Get_Menu();
    InitLeftMenu();
    tabClose();
    tabCloseEven();
})
function Get_Menu() {
    $.ajax({
        type: 'POST',
        url: "../Home/GetMenu",
        data: "",
        async: false,
        dataType: "json",
        error: function (msg) { alert('获取数据错误！！！'); },
        success: function (msg) {
            if (msg.InfoID == "0") {
                msgShow("提示", msg.InfoMessage, "error");
            }
            else {
                _menus = msg;
            }
        }
    });
}

//初始化左侧
function InitLeftMenu() {
	$("#nav").accordion({animate:false});
	$.each(_menus.menus, function (i, n) {
	    var menulist = '';
	    menulist += '<ul>';
	    $.each(n.children, function (j, o) {
	        menulist += '<li><div><a ref="' + o.id + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.iconCls + '" >&nbsp;</span><span class="nav">' + o.text + '</span></a></div></li> ';
	    })
	    menulist += '</ul>';

	    $('#nav').accordion('add', {
	        title: n.text,
	        content: menulist,
	        iconCls: 'icon ' + n.iconCls
	    });

	});
	$('.easyui-accordion li a').click(function () {
	    var tabTitle = $(this).children('.nav').text();

	    var url = $(this).attr("rel");
	    var menuid = $(this).attr("ref");

	    if (url.indexOf("http://") == -1) { //reportserver报表不加ActionId
	        if (url.indexOf("?") != -1) {
	            url += "&ActionId=" + menuid;
	        }
	        else {
	            if (url.substring(url.length - 1) == "/") {
	                url += "?ActionId=" + menuid;
	            }
	            else {
	                url += "/?ActionId=" + menuid;
	            }
	        }
	    }


	    var icon = getIcon(menuid, icon);

	    addTab(tabTitle, url, icon);
	    $('.easyui-accordion li div').removeClass("selected");
	    $(this).parent().addClass("selected");
	}).hover(function () {
	    $(this).parent().addClass("hover");
	}, function () {
	    $(this).parent().removeClass("hover");
	});

	//选中第一个
	var panels = $('#nav').accordion('panels');
	var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}
//获取左侧导航的图标
function getIcon(menuid){
	var icon = 'icon ';
	$.each(_menus.menus, function(i, n) {
	    $.each(n.children, function (j, o) {
		 	if(o.id==menuid){
				icon += o.iconCls;
			}
		 })
	})

	return icon;
}

function addTab(subtitle, url, icon) {
    //    $('#tabs').css("display", "block");

    //为链接老版FA页面加参数
    if (url.indexOf("#V7#") > -1) {
        $.ajax({
            type: 'POST',
            url: "/Account/GetEmpNoAndPassWord/",
            data: "",
            async: false,
            dataType: "json",
            error: function (msg) { alert('获取数据错误！！！'); },
            success: function (msg) {
                url = url.replace("#V7#", "WorkID=" + msg.EmpNo + "&PassWord=" + msg.PassWord);
            }
        });
    }
//    debugger;
    if (url.indexOf("open=1") > -1) {
        window.open(encodeURI(url)); //打开新窗口
        return;
    }
   
	if(!$('#tabs').tabs('exists',subtitle)){
		$('#tabs').tabs('add',{
			title:subtitle,
			content:createNewFrame(url),
			closable:true,
			icon:icon
		});
	}else{
		$('#tabs').tabs('select',subtitle);
		$('#mm-tabupdate').click();
	}
	tabClose();
}



function createNewFrame(url) {

    var s = '<iframe scrolling="auto" frameborder="0"  src="' + ReplaceNetworkSegment(url) + '" style="width:100%;height:100%;"></iframe>';

    return s;
}


function tabClose()
{
	/*双击关闭TAB选项卡*/
	$(".tabs-inner").dblclick(function(){
		var subtitle = $(this).children(".tabs-closable").text();
		$('#tabs').tabs('close',subtitle);
	})
	/*为选项卡绑定右键*/
	$(".tabs-inner").bind('contextmenu',function(e){
		$('#mm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle =$(this).children(".tabs-closable").text();

		$('#mm').data("currtab",subtitle);
		$('#tabs').tabs('select',subtitle);
		return false;
	});
}
//绑定右键菜单事件
function tabCloseEven()
{
	//刷新
	$('#mm-tabupdate').click(function(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createNewFrame(url)
			}
		})
	})
	//关闭当前
	$('#mm-tabclose').click(function(){
		var currtab_title = $('#mm').data("currtab");
		$('#tabs').tabs('close',currtab_title);
	})
	//全部关闭
	$('#mm-tabcloseall').click(function(){
		$('.tabs-inner span').each(function(i,n){
			var t = $(n).text();
			$('#tabs').tabs('close',t);
		});
	});
	//关闭除当前之外的TAB
	$('#mm-tabcloseother').click(function(){
		$('#mm-tabcloseright').click();
		$('#mm-tabcloseleft').click();
	});
	//关闭当前右侧的TAB
	$('#mm-tabcloseright').click(function(){
		var nextall = $('.tabs-selected').nextAll();
		if(nextall.length==0){
			//msgShow('系统提示','后边没有啦~~','error');
			//alert('后边没有啦~~');
			return false;
		}
		nextall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});
	//关闭当前左侧的TAB
	$('#mm-tabcloseleft').click(function(){
		var prevall = $('.tabs-selected').prevAll();
		if(prevall.length==0){
			//alert('到头了，前边没有啦~~');
			return false;
		}
		prevall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});

	//退出
	$("#mm-exit").click(function(){
		$('#mm').menu('hide');
	})
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}
