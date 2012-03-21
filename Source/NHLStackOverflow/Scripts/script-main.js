var pjaxify = function (id) {
    id = id || 'content';

    var links = document.getElementById(id).getElementsByTagName('a');

    for (var i = 0; i < links.length; i++) {
        if (!(/login|user/).test(links[i].href)) {
            λ.pjax.set(links[i], { container: 'content', preproc: 'pjaxPreProc', postproc: 'pjaxify' });
        }
    }
};


var initpjax = function () {
    if ((/login|user/).test(location.href))
        return;

    λ.pjax.registerProc('pjaxify', pjaxify);
    λ.pjax.registerProc('pjaxPreProc', pjaxPreProc);

    var links = document.getElementsByTagName('a');

    for (var i = 0; i < links.length; i++) {
        if (!(/login|user/).test(links[i].href)) {
            λ.pjax.set(links[i], { container: 'content', preproc: 'pjaxPreProc', postproc: 'pjaxify' });
        }
    }
};

var getSidebar = function (hack) {
    var sidebar = document.getElementById('l-sidebar'),
        widgets = ['/widget/user/', '/widget/tags/'];

    sidebar.innerHTML = '';    
    λ.xhr({ url: '/widget/user/' }, function (err, res) {
        if (err) {
            return;
        }
        sidebar.innerHTML += res;

        λ.xhr({ url: '/widget/tags/' }, function (err, res) {
            if (err) {
                return;
            }
            sidebar.innerHTML += res;

            pjaxify('l-sidebar');
        });
    });
};

λ.onDocReady.addCallback(function () {
    getSidebar();
    λ.formPlaceHolders();
    initpjax();
    mobileNav();
});