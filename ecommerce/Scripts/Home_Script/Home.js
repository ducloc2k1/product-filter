//sidebar close click
function openSidebar() {
    $('#sidebar').addClass('sidebar-active');
    $('#mask-overlay').addClass('mask-overlay-active');
}

function closeSidebar() {
    $('#sidebar').removeClass('sidebar-active');
    $('#mask-overlay').removeClass('mask-overlay-active');
}

//open event
$('.sidebar-open').click(openSidebar);

//close event
$('.close-sidebar, .mask-overlay').click(closeSidebar);



