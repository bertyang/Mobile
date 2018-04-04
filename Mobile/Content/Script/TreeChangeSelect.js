var Loaddatas = {};
(function () {
    var index = 0;
    var index2 = 0;
    var ClickItem = 0;
    var data;
    function LoadTreeDate() {
        $.Ajax({ floor: false, loginstate: false, url: '/Admin/PowerManage/LoadLimiteTree', data: {}, type: 'post', cache: false, dataType: 'json', error: function (e) { }, success: function (dt) {
            data = dt;
            Loaddata('LimitList', dt);
        }
        });
    }
    function objData(arry) {
        if (data.length > 0) {
            $.each(data, function (i, m) {
                var count = 0;
                if (arry.length > 0) {
                    $.each(arry, function (j, n) {
                        if (m.id == n) {
                            m.checked = true;
                            count++;
                        }
                    });
                }
                if (count == 0) {
                    m.checked = false;
                }
                if (m.children != null) {
                    if (m.children.length > 0) {
                        EachNext(m.children, arry);
                    }
                }
            })
        }
        return data;
    }

    function EachNext(data, arry) {
        $.each(data, function (i, m) {
            var count = 0;
            if (arry.length > 0) {
                $.each(arry, function (j, n) {
                    if (m.id == n) {
                        m.checked = true;
                        count++;
                    }
                });
            }
            if (count == 0) {
                m.checked = false;
            }
            if (m.children != null) {
                if (m.children.length > 0) {
                    EachNext(m.children, arry);
                }
            }
        });
    }

    function ChangeStage(item, arry) {
        if (item != null && item.length > 0) {
            $.each(item, function (i, m) {
                var count = 0;
                var target = $(m).children('.tree-node').children('.tree-checkbox');
                if (arry != null && arry.length > 0) {
                    $.each(arry, function (j, n) {
                        var id = $(m).children('div').attr('node-id');
                        if (n == id) {
                            //item.splice(i, 1);
                            arry.splice(j, 1);
                            count++;
                            var target = $(m).children('.tree-node').children('.tree-checkbox');
                            if (!$(target).hasClass('tree-checkbox1')) {
                                var item1 = $(m).children('ul').children('li');
                                if (item1 != null && item1.length > 0) {
                                    var ct = NextChangeState(item1, arry)
                                    if (ct > 0) {
                                        if (ct == item1.length) {
                                            AddClass1(target)
                                        }
                                        else {
                                            AddClass2(target)
                                        }
                                    } else {
                                        AddClass0(target);
                                    }
                                }
                                else {
                                    AddClass1(target)
                                }
                            }
                            else {
                                var item1 = $(m).children('ul').children('li');
                                if (item1 != null && item1.length > 0) {
                                    var ct = NextChangeState(item1, arry)
                                    if (ct > 0) {
                                        if (ct == item1.length) {
                                            AddClass1(target)
                                        }
                                        else {
                                            AddClass2(target)
                                        }
                                    } else {
                                        AddClass0(target);
                                    }
                                }
                                else {
                                    AddClass1(target)
                                }
                            }

                        }
                    });
                }

                if (count == 0) {
                    var item1 = $(m).children('ul').children('li');
                    if (item1 != null && item1.length > 0) {

                        var ct = NextChangeState(item1, arry);
                        if (ct > 0) {
                            if (ct == item1.length) {
                                AddClass1(target)
                            }
                            else {

                                AddClass2(target)
                            }
                        } else {
                            AddClass0(target);
                        }
                    }
                }
            });
        }
    }
    function NextChangeState(item, arrs) {
        var NextCount = 0;
        var arry = new Array()
        arry = arrs
        if (item != null && item.length > 0) {
            $.each(item, function (i, m) {
                var count = 0;
                if (arry != null && arry.length > 0) {
                    $.each(arry, function (j, n) {
                        var id = $(m).children('div').attr('node-id');
                        if (n == id) {
                            // item.splice(i, 1);
                            arry.splice(j, 1);
                            NextCount++;
                            var target = $(m).children('.tree-node').children('.tree-checkbox');
                            if (!$(target).hasClass('tree-checkbox1')) {
                                var item1 = $(m).children('ul').children('li');
                                if (item1 != null && item1.length > 0) {
                                    var ct = NextChangeState(item1, arry)
                                    if (ct > 0) {
                                        if (ct == item1.length) {
                                            AddClass1(target)
                                        }
                                        else {
                                            AddClass2(target)
                                        }
                                    } else {
                                        AddClass0(target);
                                    }
                                }
                                else {
                                    AddClass1(target)
                                }
                            }
                            else {
                                var item1 = $(m).children('ul').children('li');
                                if (item1 != null && item1.length > 0) {
                                    var ct = NextChangeState(item1, arry)
                                    if (ct > 0) {
                                        if (ct == item1.length) {
                                            AddClass1(target)
                                        }
                                        else {
                                            AddClass2(target)
                                        }
                                    } else {
                                        AddClass0(target);
                                    }
                                }
                                else {
                                    AddClass1(target)
                                }
                            }
                            count++;

                        }
                    });
                }

                if (count == 0) {
                    var target = $(m).children('.tree-node').children('.tree-checkbox');
                    var item1 = $(m).children('ul').children('li');
                    if (item1 != null && item1.length > 0) {
                        var ct = NextChangeState(item1, arry)
                        if (ct > 0) {
                            if (ct == item1.length) {
                                AddClass1(target)
                            }
                            else {
                                AddClass2(target)
                            }
                        } else {
                            AddClass0(target);
                        }
                    }
                    else {
                        AddClass0(target)
                    }
                }

            });
        }
        return NextCount;
    }




    function ChangeStateTree(items, item, arry) {

        for (var i = 0; i < item.length; i++) {
            var m = item[i];
            var nextCount = 0;
            var id = $(m).attr('node-id');
            for (var j = 0; j < arry.length; j++) {
                if (arry[j] == id) {
                    AddClass1($(m).children('.tree-checkbox'));
//                     $('#LimitList').tree('check', $('#LimitList').tree('find',id).target);
                    arry.splice(j, 1);
                    nextCount++;
                }
            }

            if (nextCount == 0) {
//                $('#LimitList').tree('uncheck', $('#LimitList').tree('find',id).target)
                AddClass0($(m).children('.tree-checkbox'));
            }
        }
        ChangeParentNext(items);
    }

    function ChangeParentNext(item) {
        var CountChildren = 0;
        $.each(item, function (i, m) {
            var target = $(m).children('.tree-node').children('.tree-checkbox');
            if ($(target).hasClass('tree-checkbox1') == true) {
                CountChildren++;
            }
            var item1 = $(m).children('ul').children('li');
            if (item1 != null && item1.length > 0) {
                var count = ChangeParentNext(item1);
                if (count > 0) {
                    if (count == item1.length) {
                        AddClass1(target)
                    }
                    else {
                        AddClass2(target)
                    }
                }
                else {
                    AddClass0(target);
                }
            }
            ChangesParentState(m);
        });
        return CountChildren;
    }

    function ChangesParentState(option) {
        var target = $(option).children('.tree-node').children('.tree-checkbox');
        var item1 = $(option).children('ul').children('li');
        var CountChildren = 0;
        var Count2 = 0;
        if (item1 != null || item1.length > 0) {

            $.each(item1, function (i, m) {
                var target2 = $(m).children('.tree-node').children('.tree-checkbox');
                if ($(target2).hasClass('tree-checkbox1') == true) {
                    CountChildren++;
                }
                if ($(target2).hasClass('tree-checkbox2') == true) {
                    Count2++;
                }
            });
        }

        if (CountChildren > 0 && CountChildren != item1.length && item1.length > 0) {
            AddClass2(target);
        }
        else if (Count2 == item1.length && item1.length > 0) {
            AddClass2(target);
        }
    }

    function AddClass1(target) {
        $(target).removeClass('tree-checkbox1').addClass('tree-checkbox1').removeClass('tree-checkbox2').removeClass('tree-checkbox0')
    }
    function AddClass2(target) {
        $(target).removeClass('tree-checkbox2').addClass('tree-checkbox2').removeClass('tree-checkbox1').removeClass('tree-checkbox0')
    }
    function AddClass0(target) {
        $(target).removeClass('tree-checkbox0').addClass('tree-checkbox0').removeClass('tree-checkbox2').removeClass('tree-checkbox1')
    }

    function SelectTableindex(inde) {

        if (!isNaN(inde)) {
            index = inde;
        }
        else {
            return index;
        }
    }

    function SelectTableindex2(inde) {

        if (!isNaN(inde)) {
            index2 = inde;
        }
        else {
            return index2;
        }
    }
    function ClickitemS(item) {
        if (item == null || item == "") {
            return ClickItem;
        }
        else {
            ClickItem = item;
        }
    }

    function ExtendTreeParent(target, option) {
        var nodeparent = $(option).tree('getParent', target);
        if (nodeparent != null && nodeparent != undefined) {
            $(option).tree('expand', nodeparent.target);
            ExtendTreeParent(nodeparent.target, option);
        }
    }
    Loaddatas.TreeParentExtend = ExtendTreeParent;
    Loaddatas.data = LoadTreeDate;
    Loaddatas.Index = SelectTableindex;
    Loaddatas.Index2 = SelectTableindex2;
    Loaddatas.datachange = ChangeStateTree;
    Loaddatas.ClickitemS = ClickitemS;
})()

