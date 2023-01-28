var dialogModule = (function($) {

    function showDialog(dialog, url) {
        const popup = dialog.dxPopup("instance");

        popup.option({
            contentTemplate: () => {
                var content;

                $.ajax({
                    type: "GET",
                    url: url,
                    async: false,
                    success: function (data) {
                        content = data;
                    },
                    error: function (error) {
                    }
                });

                return content;
            }
        });

        popup.show();
    }

    return {
        showDialog: showDialog
    };

})(jQuery);