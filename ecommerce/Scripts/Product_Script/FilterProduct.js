//filter price slider
window.onload = function () {
    slideOne();
    slideTwo();
}

let sliderOne = document.getElementById("slider-1");
let sliderTwo = document.getElementById("slider-2");
let displayValOne = document.getElementById("range1");
let displayValTwo = document.getElementById("range2");
let minGap = 0;
let sliderTrack = document.querySelector(".slider-track");
let sliderMaxValue = document.getElementById("slider-1").max;

function slideOne() {
    if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
        sliderOne.value = parseInt(sliderTwo.value) - minGap;
    }
    displayValOne.textContent = "$" + sliderOne.value;
    fillColor();
}
function slideTwo() {
    if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
        sliderTwo.value = parseInt(sliderOne.value) + minGap;
    }
    displayValTwo.textContent = "$" + sliderTwo.value;
    fillColor();
}
function fillColor() {
    percent1 = (sliderOne.value / sliderMaxValue) * 100;
    percent2 = (sliderTwo.value / sliderMaxValue) * 100;
    sliderTrack.style.background = `linear-gradient(to right, #e7e7e7 ${percent1}% , #56cfe1 ${percent1}% , #56cfe1 ${percent2}%, #e7e7e7 ${percent2}%)`;
}

//handle ajax
const parseParams = (querystring) => {

    // parse query string
    const params = new URLSearchParams(querystring);

    const obj = {};

    // iterate over all keys
    for (const key of params.keys()) {
        if (params.getAll(key).length > 1) {
            obj[key] = params.getAll(key);
        } else {
            obj[key] = params.get(key);
        }
    }

    return obj;
};

const stringifySearchParams = (obj) => {
    var str = [];
    for (var p in obj)
        if (obj.hasOwnProperty(p)) {
            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
        }
    return str.join("&");
}

const onlyUnique = (value, index, self) => {
    return self.indexOf(value) === index;
}

const params = parseParams(window.location.search);

const filterObj = {
    filter_color: params.filter_color?.split(',').filter(x => x != "") || [],
    filter_size: params.filter_size?.split(',').filter(x => x != "") || []
};

function filter() {
    $.ajax({
        url: "/Products/Filter?" + stringifySearchParams(filterObj),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

const handleOnChange = (e) => {
    debugger;


    let arr = filterObj[e.name];

    if (e.checked) arr = [...arr, e.value].filter(onlyUnique);

    else arr = arr.filter(x => x !== e.value);

    filterObj[e.name] = arr;

    const newFilterObj = { ...filterObj };

    for (let key in newFilterObj) {
        if (newFilterObj[key].length === 0) delete newFilterObj[key];
    }

    var newurl = stringifySearchParams(newFilterObj) !== "" ?
        `${window.location.pathname}?${stringifySearchParams(newFilterObj)}`
        : `${window.location.pathname}`;

    window.history.pushState({ path: newurl }, '', newurl);
}

$("#filter_button").click(filter);


