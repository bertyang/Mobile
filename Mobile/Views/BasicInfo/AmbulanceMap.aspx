<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>AmbulanceMap.aspx</title>
    <script type="text/javascript" src="http://api.map.baidu.com/api?key=l3nrBgOxyK602381jVviTxiMbaOyZ3c8&v=1.0&services=false"></script>
  
</head>
<body>
    <div style="width:520px;height:340px;border:1px solid gray" id="container"></div>
</body>
</html>
  <script type="text/javascript">
      var map = new BMap.Map("container");
      map.clearOverlays();//清空原来的标注
      var point = new BMap.Point(116.404, 39.915);
      map.centerAndZoom(point, 19);
      var marker = new BMap.Marker(point);  // 创建标注，为要查询的地址对应的经纬度
      map.addOverlay(marker);
 </script>