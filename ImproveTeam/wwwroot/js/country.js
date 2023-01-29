var countryModule = (function($, dialogModule) {

    var selectors = {
        countriesGridContainer: "#countriesGridContainer",
        editCountryPopupContainer: "#editCountryPopupContainer",
        regionsGridContainer: "#regionsGridContainer",
        citiesGridContainer: "#citiesGridContainer"
    }

    function init() {
        $(document).ready(function() {
            initCountriesGrid();
            initEditCountryPopupContainer();
        });
    }

    function showEditCountryPopup(countryId) {
        var popup = $(selectors.editCountryPopupContainer);
        var url = popup.data("url").replace("__countryId__", countryId);

        dialogModule.showDialog(popup, url);
    }

    function initRegionsGrid() {
        var regionsGridContainer = $(selectors.regionsGridContainer);

        var loadUrl = regionsGridContainer.data("load-url");
        var insertUrl = regionsGridContainer.data("insert-url");
        var updateUrl = regionsGridContainer.data("update-url");
        var removeUrl = regionsGridContainer.data("remove-url");

        $(selectors.regionsGridContainer).dxDataGrid({
            dataSource: getRegionsGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl),
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
                    caption: "Регион"
                }
            ],
            onSelectionChanged: function (e) {
                $(selectors.citiesGridContainer).dxDataGrid("refresh");
            }
        });
    }

    function initCitiesGrid() {
        var citiesGridContainer = $(selectors.citiesGridContainer);

        var loadUrl = citiesGridContainer.data("load-url");
        var insertUrl = citiesGridContainer.data("insert-url");
        var updateUrl = citiesGridContainer.data("update-url");
        var removeUrl = citiesGridContainer.data("remove-url");

        citiesGridContainer.dxDataGrid({
            dataSource: getCitiesGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl),
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
                    caption: "Город"
                }
            ],
        });
    }

    function initEditCountryPopupContainer() {
        $(selectors.editCountryPopupContainer).dxPopup({
            title: "Редактировать страну",
            visible: false,
            hideOnOutsideClick: true,
            width: 1300,
            height: "auto",
            onHiding: function() {
                $(selectors.countriesGridContainer).dxDataGrid("refresh");
            },
        });
    }

    function initCountriesGrid() {
        var url = $(selectors.countriesGridContainer).data("url");

        $(selectors.countriesGridContainer).dxDataGrid({
            dataSource: getDefaultGridDataSource(url),
            columns: getCountriesGridColumns(),
            allowColumnReordering: true,
            allowColumnResizing: true,
            showBorders: true,
            searchPanel: {
                visible: true
            },
            selection: {
                mode: 'single'
            },
            loadPanel: {
                enabled: true,
            },
            paging: {
                enabled: true,
            }
        });
    }

    function getCitiesGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl) {
        return new DevExpress.data.CustomStore({
            key: "id",
            load: function() {
                var regionsGrid = $(selectors.regionsGridContainer).dxDataGrid("instance");
                var selectedRegionId = regionsGrid.getSelectedRowKeys()[0];

                if (!selectedRegionId) {
                    return [];
                }

                const deferred = $.Deferred();

                $.ajax({
                    type: 'GET',
                    url: loadUrl.replace("__regionId__", selectedRegionId),
                    success: function(result) {
                        deferred.resolve(result, {});
                    },
                    error: function() {
                        deferred.reject('Data Loading Error');
                    }
                });

                return deferred.promise();
            },
            insert: function(values) {
                var deferred = $.Deferred();

                var regionsGrid = $(selectors.regionsGridContainer).dxDataGrid("instance");
                var selectedRegionId = regionsGrid.getSelectedRowKeys()[0];

                if (!selectedRegionId) {
                    return deferred.reject("Выберите регион");
                }

                $.ajax({
                    url: insertUrl.replace("__regionId__", selectedRegionId),
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
                    .replace("__cityId__", key)
                    .replace("__cityName__", values.name);

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

                $.ajax({
                    url: removeUrl.replace("__cityId__", key),
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

    function getRegionsGridDataSource(loadUrl, insertUrl, updateUrl, removeUrl) {
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
                    error: function() {
                        deferred.reject('Data Loading Error');
                    }
                });

                return deferred.promise();
            },
            insert: function(values) {
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
            update: function(key, values) {
                var deferred = $.Deferred();

                var url = updateUrl
                    .replace("__regionId__", key)
                    .replace("__regionName__", values.name);

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
            remove: function(key) {
                var deferred = $.Deferred();

                var url = removeUrl.replace("__regionId__", key);

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


    function getDefaultGridDataSource(loadUrl) {
        return new DevExpress.data.CustomStore({
            key: 'id',
            load: function() {
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
            }
        });
    }

    function getCountriesGridColumns() {
        return [
            {
                dataField: "name",
                caption: "Страна",
                width: "400px"
            },
            {
                dataField: "code",
                caption: "Код страны",
                width: "200px"
            },
            {
                caption: "Регионы",
                cellTemplate(container, options) {
                    var model = options.data;
                   
                    var regions = [];
                    model.regions.forEach(region => {
                        regions.push(getRegionInfo(region));
                    });

                    container.html(`<div class='white-space-pre-wrap'>${regions.join(", ")}</div>`);
                }
            },
            {
                width: "200px",
                cellTemplate(container, options) {
                    var countryId = options.data.id;

                    var template = `
                        <div style='display: flex; justify-content: space-around;'>
                            <button class='btn btn-sm btn-info margin-right-5' onclick='countryModule.showEditCountryPopup(${countryId})'>
                                <i class='bx bx-edit' style='cursor: pointer;'></i>
                            </button>
                        </div>`;

                    container.html(template);
                }
            }
        ];
    }

    function getRegionInfo(region) {
        var regionInfo = `<strong>${region.name}</strong>`;

        var regionCityNames = [];

        region.cities.forEach(city => {
            regionCityNames.push(city.name);
        });

        if (regionCityNames.length) {
            regionInfo += " (" + regionCityNames.join(", ") + ")";
        }

        return regionInfo;
    }

    return {
        init: init,
        showEditCountryPopup: showEditCountryPopup,
        initRegionsGrid: initRegionsGrid,
        initCitiesGrid: initCitiesGrid
    };

})(jQuery, dialogModule);