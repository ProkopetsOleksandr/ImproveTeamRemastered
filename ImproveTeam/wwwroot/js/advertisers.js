var advertiserModule = (function ($) {

    const noBreakSpace = '\u00A0';

    var selectors = {
        advertisersGridContainer: "#advertisersGridContainer"
    };

    function init() {
        $(document).ready(function () {
            initAdvertisersGrid();
        });
    }

    function initAdvertisersGrid() {
        var advertisersGridContainer = $(selectors.advertisersGridContainer);

        var loadUrl = advertisersGridContainer.data("load-url");
        var insertUrl = advertisersGridContainer.data("insert-url");
        var updateUrl = advertisersGridContainer.data("update-url");
        var removeUrl = advertisersGridContainer.data("remove-url");

        advertisersGridContainer.dxDataGrid({
            dataSource: getAdvertisersGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl),
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
                },
                {
                    dataField: 'productIds',
                    caption: 'Продукты',
                    allowSorting: false,
                    editCellTemplate: tagBoxEditorTemplate,
                    lookup: {
                        dataSource: productsList,
                        valueExpr: 'id',
                        displayExpr: 'name',
                    },
                    cellTemplate(container, options) {
                        var text = "";

                        if (options.value) {
                            options.value.forEach(productId => {
                                var product = productsList.find(product => product['id'] === productId);
                                if (product) {
                                    text += text === "" ? product.name : ", " + product.name;
                                }
                            });
                        }

                        container.text(text || noBreakSpace).attr('title', text);
                    },
                    calculateFilterExpression(filterValue, selectedFilterOperation, target) {
                        if (target === 'search' && typeof (filterValue) === 'string') {
                            return [this.dataField, 'contains', filterValue];
                        }
                        return function (data) {
                            return (data.AssignedEmployee || []).indexOf(filterValue) !== -1;
                        };
                    },
                }
            ],
        });
    }

    function tagBoxEditorTemplate(cellElement, cellInfo) {
        return $('<div>').dxTagBox({
            dataSource: productsList,
            value: cellInfo.value,
            valueExpr: 'id',
            displayExpr: 'name',
            showSelectionControls: true,
            maxDisplayedTags: 5,
            showMultiTagOnly: false,
            applyValueMode: 'useButtons',
            searchEnabled: true,
            onValueChanged(e) {
                cellInfo.setValue(e.value);
            },
            onSelectionChanged() {
                cellInfo.component.updateDimensions();
            },
        });
    }

    function getAdvertisersGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl) {
        return new DevExpress.data.CustomStore({
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
            update: function (data, values) {
                var deferred = $.Deferred();

                console.log(data);

                $.ajax({
                    url: updateUrl,
                    method: "PUT",
                    data: {
                        id: data.id,
                        name: values.hasOwnProperty('name') ? values.name : data.name,
                        productIds: values.hasOwnProperty('productIds') ? values.productIds : data.productIds
                    }
                })
                .done(deferred.resolve)
                .fail(function (e) {
                    deferred.reject("Update failed");
                });

                return deferred.promise();
            },
            remove: function (data) {
                var deferred = $.Deferred();

                var url = removeUrl.replace("__advertiserId__", data.id);

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