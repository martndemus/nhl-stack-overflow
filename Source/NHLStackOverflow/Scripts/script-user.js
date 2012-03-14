

var pjaxify = function (id) {
    id = id || 'content';

    var links = document.getElementById(id).getElementsByTagName('a');

    for (var i = 0; i < links.length; i++) {
        if ((/user/).test(links[i].href)) {
            λ.pjax.set(links[i], { container: 'content', preproc: 'pjaxPreProc', postproc: 'pjaxify' });
        }
    }
};

var initpjax = function () {
    λ.pjax.registerProc('pjaxify', pjaxify);
    λ.pjax.registerProc('pjaxPreProc', pjaxPreProc);

    var links = document.getElementsByTagName('a');

    for (var i = 0; i < links.length; i++) {
        if ((/user/).test(links[i].href)) {
            λ.pjax.set(links[i], { container: 'content', preproc: 'pjaxPreProc', postproc: 'pjaxify' });
        }
    }
};

var getSidebar = function (hack) {
    var sidebar = document.getElementById('l-sidebar'),
        widgets = ['/widget/account/'];

    sidebar.innerHTML = '';

    for (var i = 0; i < widgets.length; i++) {
        λ.xhr({ url: widgets[i] }, function (err, res) {
            if (err) {
                return;
            }
            sidebar.innerHTML += res;

            pjaxify('l-sidebar');
        });
    }
};

λ.onDocReady.addCallback(function () {
    getSidebar();
    λ.formPlaceHolders();
    initpjax();
    mobileNav();
});