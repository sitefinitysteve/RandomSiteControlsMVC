$(document).ready(function () {
    var wrapper = $("#twitter-feed");
    
    $.getJSON("/playground/test/twitterfeed").then(function (data) {
        if (data.Items.length > 0) {
            var tweet = data.Items[0].dynamicProperties;
            debugger;
            //var username = tweet.filter(tweet => tweet.Key === "Username")[0].Value;
            //var date = wcftimeconvert(tweet.filter(tweet => tweet.Key === "LastModified")[0].Value);
            //var time = wcfformatAMPM(date);
            //var dateString = date.toLocaleDateString();

            //var content = tweet.filter(tweet => tweet.Key === "Content")[0].Value;
            //var link = tweet.filter(tweet => tweet.Key === "Link")[0].Value;

            //var html = "<b class='sfTweetAuthor'>" + username + "</b> <i class='sfTweetDate'>" + dateString + " " + time + "</i>\
            //            <p class='sfTweetContent'>"+ content + "</p>\
            //            <a href='"+ link + "' class='sfExpand'>Expand</a>";

            //wrapper.find(".sfTweetWrp").html(html);
        } else {
            wrapper.hide();
        }
    });
});

function wcftimeconvert(ds) {
    var D, dtime, T, tz, off,
        dobj = ds.match(/(\d+)|([+-])|(\d{4})/g);
    T = parseInt(dobj[0]);
    tz = dobj[1];
    off = dobj[2];
    if (off) {
        off = (parseInt(off.substring(0, 2), 10) * 3600000) +
            (parseInt(off.substring(2), 10) * 60000);
        if (tz == '-') off *= -1;
    }
    else off = 0;
    return new Date(T += off);
}

function wcfformatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}