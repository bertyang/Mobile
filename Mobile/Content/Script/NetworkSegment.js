function getHost(url) {
    var host = "null";
    if (typeof url == "undefined"
                        || null == url)
        url = window.location.href;
    var regex = /.*\:\/\/([^\/\:]*).*/;
    var match = url.match(regex);
    if (typeof match != "undefined"
                        && null != match)
        host = match[1];
    return host;
}



 //不同网段的处理。
function ReplaceNetworkSegment(url) {

    if (url.indexOf("ReportServer") == -1) {
        return url.replace(getHost(url), top.location.hostname);
    }
    else {
        return url.replace(getHost(url).substring(3, 5), top.location.hostname.substring(3, 5));
    }

}
