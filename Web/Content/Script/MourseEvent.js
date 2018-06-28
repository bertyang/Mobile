// JScript 文件

//鼠标移动到每行时触发事件
function mOver(src)		//鼠标样式：手型
{			
	src.style.backgroundColor='NavajoWhite';
	src.style.cursor='hand';
}
/*****************************************************************/
//鼠标移开每行时触发事件
function mOut(src,type) {

    if ($(src).attr("cRow") != "1") {
        if (type == 0) {
            src.style.backgroundColor = '#ECF3FA';
        }
        else {
            src.style.backgroundColor = '#AFD3F5';
        }
    }

	src.style.cursor='arrow';
}
/******************************************************************/
function _mOver(src)	//鼠标样式：正常
{			
	src.style.backgroundColor='NavajoWhite';
	src.style.cursor='arrow';
}
function _mClick(src)	//鼠标样式：正常
{
    src.style.backgroundColor = 'FBEC88';
    src.style.cursor = 'arrow';
    if ($(src).attr("cRow") != "1") {
        $(src).attr("cRow", "1");
    }
    else {
        $(src).attr("cRow", "");
    }
    
}