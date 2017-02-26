declare var $: any;

(function(global) {

    let counter = 0, timeout;
    let preloader = <HTMLElement>document.querySelector('.preloader');
    let progressBar = <HTMLElement>document.querySelector('.preloader-progress-bar');
    let body = <HTMLElement>document.querySelector('body');

    // disables scrollbar
    body.style.overflow = 'hidden';

    timeout = setTimeout(startCounter, 20);

    // main.ts call this function once the app is boostrapped
    global.appBootstrap = () => {
        setTimeout(endCounter, 1000);
    };

    function startCounter() {
        let remaining = 100 - counter;
        counter = counter + (0.015 * Math.pow(1 - Math.sqrt(remaining), 2));

        progressBar.style.width = Math.round(counter)+'%';

        timeout = setTimeout(startCounter, 20);
    }

    function endCounter() {

        clearTimeout(timeout);

        progressBar.style.width = '100%';

        setTimeout(function() {
            // animate preloader hiding
            removePreloader();
            // retore scrollbar
            body.style.overflow = '';
        }, 300);
    }

    function removePreloader() {
        preloader.addEventListener('transitionend', () => {
            preloader.className = 'preloader-hidden';
        });
        preloader.className += ' preloader-hidden-add preloader-hidden-add-active';
    };

})((<any>window));