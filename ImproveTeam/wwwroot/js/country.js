var countryModule = (function($, dialogModule) {

    var selectors = {
        countriesGridContainer: "#countriesGridContainer",
        editCountryPopupContainer: "#editCountryPopupContainer"
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

    function initEditCountryPopupContainer() {
        $(selectors.editCountryPopupContainer).dxPopup({
            title: "Редактировать страну",
            visible: false,
            hideOnOutsideClick: true,
            width: 600,
            height: "auto"
        });
    }

    function initCountriesGrid() {
        $(selectors.countriesGridContainer).dxDataGrid({
            dataSource: getCountriesGridDataSource(),
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

    function getCountriesGridDataSource() {
        var url = $(selectors.countriesGridContainer).data("url");

        return new DevExpress.data.CustomStore({
            key: 'id',
            load: function () {
                const deferred = $.Deferred();

                $.ajax({
                    type: 'GET',
                    url: url,
                    success: function (result) {
                        deferred.resolve(result, {});
                    },
                    error: function() {
                        deferred.reject('Data Loading Error');
                    }
                });

                return deferred.promise();
            },
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
        showEditCountryPopup: showEditCountryPopup
    };

})(jQuery, dialogModule);