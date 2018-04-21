/**
 * 关闭Tab 。杨铭
 * 
 */
function CloseCurrentTab() {
    var tab = parent.$('#tabs').tabs('getSelected');
    var index = parent.$('#tabs').tabs('getTabIndex', tab);
    parent.$('#tabs').tabs('close', index);
}

function createFrame(url)
{
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
	return s;
}

function AddTab(subtitle, url,icon) { 

    subtitle=unescape(subtitle);      
         
    if(!this.parent.$('#tabs').tabs('exists',subtitle)){
		this.parent.$('#tabs').tabs('add',{
			title:  subtitle,
            content: createFrame(url),
            closable: true,
            icon:'icon '+icon
		});
	}else{
		this.parent.$('#tabs').tabs('select', subtitle);
		//this.parent.$('#mm-tabupdate').click(); 
	}       
}


//不同网段的处理。
function ReplaceNetworkSegment(url){
    return url;
}

        //#region 日期转换
        function renderTime(data) {
            if (data != null) {
                var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
                var Month = da.getMonth() + 1 < 10 ? "0" + (da.getMonth() + 1) : da.getMonth() + 1;
                var CurrentDate = da.getDate() < 10 ? "0" + da.getDate() : da.getDate();
                var Hours = da.getHours();
                if (Hours < 10) {
                    Hours = "0" + Hours;
                }
                var Minutes = da.getMinutes();
                if (Minutes < 10) {
                    Minutes = "0" + Minutes;
                }
                var getSeconds = da.getSeconds();
                if (getSeconds < 10) {
                    getSeconds = "0" + getSeconds;
                }
                //return da.getFullYear()+"年"+ (da.getMonth()+1)+"月" +da.getDate()+"日" +da.getHours()+":"+da.getMinutes()+":"+da.getSeconds();
                return da.getFullYear() + "-" + Month + "-" + CurrentDate + " " + Hours + ":" + Minutes + ":" + getSeconds;
            }
        }
        //#endregion
        //#region 日期转换 只需要日期
        function renderDate(data) {
            if (data != null) {
                var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
                var Month = da.getMonth() + 1 < 10 ? "0" + (da.getMonth() + 1) : da.getMonth() + 1;
                var CurrentDate = da.getDate() < 10 ? "0" + da.getDate() : da.getDate();
                //                var Hours = da.getHours();
                //                if (Hours < 10) {
                //                    Hours = "0" + Hours;
                //                }
                //                var Minutes = da.getMinutes();
                //                if (Minutes < 10) {
                //                    Minutes = "0" + Minutes;
                //                }
                //                var getSeconds = da.getSeconds();
                //                if (getSeconds < 10) {
                //                    getSeconds = "0" + getSeconds;
                //                }
                //return da.getFullYear()+"年"+ (da.getMonth()+1)+"月" +da.getDate()+"日" +da.getHours()+":"+da.getMinutes()+":"+da.getSeconds();
                return da.getFullYear() + "-" + Month + "-" + CurrentDate;
            }
        }
        //#endregion
    function EUIcombobox(jqS,o)
    {
        $.ajax({
            type: "POST",
            //async: false, 去掉此处默认异步加载更快
            url: o.url,
            dataType: "JSON",
            success: function (data1, textStatus, jqXHR) {
            o.data=o.OneOption.concat(data1);
            delete o.url;
            delete o.OneOption;
                $(jqS).combobox(o);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(o.url);
            }
        });
    }



    jQuery.fn.extend({
        //扩展方法写这里
        //check: function() {
        //    return this.each(function() { this.checked = true; });
        //},
        EUIcombobox: function (o) {
            var jqS = this;
            $.ajax({
                type: "POST",
                //async: false, //去掉此处默认异步加载更快
                url: o.url,
                dataType: "JSON",
                success: function (data1, textStatus, jqXHR) {
                    o.data = o.OneOption.concat(data1);
                    delete o.url;
                    delete o.OneOption;
                    jqS.combobox(o);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(o.url);
                }
            });
        }

    });

//#region 页面加载
$(document).ready(function () {

    //#region 解决jquery-easyui1.3.3combobox多选模式不兼容IE8问题
    //IE8下，数组不支持indexOf方法 
    //扩展Array的原型对象，加入indexOf方法
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (target) {
            for (var i = 0, l = this.length; i < l; i++) {
                if (this[i] === target)
                    return i;
            }
            return -1;
        };
    }
    //#endregion
})
$.extend($.fn.linkbutton.methods, {
    /**
     * 激活选项（覆盖重写）
     * @param {Object} jq
     */
    enable: function (jq) {
        return jq.each(function () {
            var state = $.data(this, 'linkbutton');
            if ($(this).hasClass('l-btn-disabled')) {
                var itemData = state._eventsStore;
                //恢复超链接
                if (itemData.href) {
                    $(this).attr("href", itemData.href);
                }
                //回复点击事件
                if (itemData.onclicks) {
                    for (var j = 0; j < itemData.onclicks.length; j++) {
                        $(this).bind('click', itemData.onclicks[j]);
                    }
                }
                //设置target为null，清空存储的事件处理程序
                itemData.target = null;
                itemData.onclicks = [];
                $(this).removeClass('l-btn-disabled');
            }
        });
    },
    /**
     * 禁用选项（覆盖重写）
     * @param {Object} jq
     */
    disable: function (jq) {
        return jq.each(function () {
            var state = $.data(this, 'linkbutton');
            if (!state._eventsStore)
                state._eventsStore = {};
            if (!$(this).hasClass('l-btn-disabled')) {
                var eventsStore = {};
                eventsStore.target = this;
                eventsStore.onclicks = [];
                //处理超链接
                var strHref = $(this).attr("href");
                if (strHref) {
                    eventsStore.href = strHref;
                    $(this).attr("href", "javascript:void(0)");
                }
                //处理直接耦合绑定到onclick属性上的事件
                var onclickStr = $(this).attr("onclick");
                if (onclickStr && onclickStr != "") {
                    eventsStore.onclicks[eventsStore.onclicks.length] = new Function(onclickStr);
                    $(this).attr("onclick", "");
                }
                //处理使用jquery绑定的事件
                var eventDatas = $(this).data("events") || $._data(this, 'events');
                if (eventDatas["click"]) {
                    var eventData = eventDatas["click"];
                    for (var i = 0; i < eventData.length; i++) {
                        if (eventData[i].namespace != "menu") {
                            eventsStore.onclicks[eventsStore.onclicks.length] = eventData[i]["handler"];
                            $(this).unbind('click', eventData[i]["handler"]);
                            i--;
                        }
                    }
                }
                state._eventsStore = eventsStore;
                $(this).addClass('l-btn-disabled');
            }
        });
    }
});
