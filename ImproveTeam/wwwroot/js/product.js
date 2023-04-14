var productModule = (function ($) {

    var selectors = {
        productsGridContainer: "#productsGridContainer"
    };

    function init() {
        $(document).ready(function () {
            initProductsGrid();
        });
    };

    function initProductsGrid() {
        var productsGridContainer = $(selectors.productsGridContainer);

        var loadUrl = productsGridContainer.data("load-url");
        var insertUrl = productsGridContainer.data("insert-url");
        var updateUrl = productsGridContainer.data("update-url");
        var removeUrl = productsGridContainer.data("remove-url");

        productsGridContainer.dxDataGrid({
            dataSource: getProductsGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl),
            showBorders: true,
            selection: {
                mode: 'single'
            },
            loadPanel: {
                enabled: true,
            },
            paging: {
                enabled: true,
                pageSize: 12,
                pageIndex: 0
            },
            searchPanel: {
                visible: true
            },
            editing: {
                allowAdding: true,
                allowUpdating: true,
                allowDeleting: true,
                confirmDelete: true,
                mode: 'row'
            },
            columns: [
                {
                    dataField: "name",
                    caption: "Название"
                }
            ],
            onSaved(e) {
                productsList = e.component.getDataSource().items();
            },
        });
    }

    function getProductsGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl) {
        return new DevExpress.data.CustomStore({
            key: 'id',
            load: function () {
                const deferred = $.Deferred();

                $.ajax({
                    type: 'GET',
                    url: loadUrl,
                    success: function (result) {
                        deferred.resolve(result, {});
                    },
                    error: function () {
                        deferred.reject('Data Loading Error');
                    }
                });

                return deferred.promise();
            },
            insert: function (values) {
                var deferred = $.Deferred();

                $.ajax({
                    url: insertUrl,
                    method: "POST",
                    data: values
                })
                .done(deferred.resolve)
                .fail(function (e) {
                    deferred.reject("Insertion failed");
                });

                return deferred.promise();
            },
            update: function (key, values) {
                var deferred = $.Deferred();

                var url = updateUrl
                    .replace("__productId__", key)
                    .replace("__productName__", values.name);

                $.ajax({
                    url: url,
                    method: "PUT"
                })
                .done(deferred.resolve)
                .fail(function (e) {
                    deferred.reject("Update failed");
                });

                return deferred.promise();
            },
            remove: function (key) {
                var deferred = $.Deferred();

                var url = removeUrl.replace("__productId__", key);

                $.ajax({
                    url: url,
                    method: "DELETE"
                })
                .done(deferred.resolve)
                .fail(function (e) {
                    deferred.reject("Deletion failed");
                });

                return deferred.promise();
            },
        });
    }

    return {
        init: init
    };

})(jQuery);