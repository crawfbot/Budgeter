function getUserTime(time) {
    var userTime = new Date(Date.parse(time)) //This new date object accounts for the user’s time zone
    var hours = userTime.getHours();
    var ampm = "AM";
    if (hours > 12) {
        hours = hours - 12;
        ampm = "PM";
    }
    if (hours == 12) {
        ampm = "PM"
    }
    if (hours == 0) {
        hours = 12;
    }
    var minutes = userTime.getMinutes();
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    var seconds = userTime.getSeconds();
    if (seconds < 10) {
        seconds = "0" + seconds;
    }
    var correctTime = (userTime.getMonth() + 1) + "/" + (userTime.getDate()) + "/" + userTime.getFullYear() + " " + hours + ":" + minutes + ":" + seconds + " " + ampm
    return (correctTime)
}
