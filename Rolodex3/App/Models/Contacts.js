var Contact = (function () {
    function Contact(obj) {
        for (var prop in obj)
            this[prop] = obj[prop];
    }
    return Contact;
})();
