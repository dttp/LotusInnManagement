﻿/*--------------------- CORE MODULES ---------------------*/
angular.module('lotusinn.app.core',
	[
		'ipCookie'
	]);
/*--------------------- HOUSE MODULES ---------------------*/
angular.module('lotusinn.app.houses.list',
    [
        'ipCookie',
        'ui.bootstrap',
    ]);

angular.module('lotusinn.app.houses.addedit',
    [
        'ipCookie',
    ]);

angular.module('lotusinn.app.houses.detail',
    [
        'ipCookie',
    ]);

angular.module('lotusinn.app.houses',
[
    'lotusinn.app.houses.list',
    'lotusinn.app.houses.addedit',
    'lotusinn.app.houses.detail'
]);
/*--------------------- WAREHOUSE MODULES ---------------------*/
angular.module('lotusinn.app.warehouse.list',
[
    'lotusinn.app.services'
]);
angular.module('lotusinn.app.warehouse.detail',
[
    'lotusinn.app.services'
]);
angular.module('lotusinn.app.warehouse.itemaddedit',
[
    'lotusinn.app.services'
]);
angular.module('lotusinn.app.warehouses',
[
    'lotusinn.app.warehouse.list',
    'lotusinn.app.warehouse.detail',
    'lotusinn.app.warehouse.itemaddedit',
]);

/*--------------------- ROOMTYPES MODULES ---------------------*/
angular.module('lotusinn.app.roomTypes.list',
    [
        'ipCookie',
        'ui.bootstrap',
    ]);

angular.module('lotusinn.app.roomTypes.addedit',
    [
        'lotusinn.app.services',
    ]);

angular.module('lotusinn.app.roomTypes',
[
    'lotusinn.app.roomTypes.list',
    'lotusinn.app.roomTypes.addedit'
]);

/*--------------------- ROOM MODULES ---------------------*/
angular.module('lotusinn.app.rooms.list',
    [
        'ipCookie',
        'ui.bootstrap',
    ]);

angular.module('lotusinn.app.rooms.addedit',
    [
        'ipCookie',
    ]);

angular.module('lotusinn.app.rooms',
[
    'lotusinn.app.rooms.list',
    'lotusinn.app.rooms.addedit'
]);

/*--------------------- USER MODULES ---------------------*/
angular.module('lotusinn.app.users.list',
[
    'ipCookie',
    'ui.bootstrap',
]);

angular.module('lotusinn.app.users.addedit',
[
    'ipCookie',
]);

angular.module('lotusinn.app.users.detail',
[
    'ipCookie',
    'ui.bootstrap',
]);

angular.module('lotusinn.app.users.myprofile',
[
    'ipCookie',
]);

angular.module('lotusinn.app.users',
[
    'lotusinn.app.users.list',
    'lotusinn.app.users.addedit',
    'lotusinn.app.users.detail',
    'lotusinn.app.users.myprofile'
]);

/*--------------------- ORDERS MODULES ---------------------*/
angular.module('lotusinn.app.orders.addedit',
[
    'lotusinn.app.services',
    'ui.bootstrap',
    'fk.eternicode-datepicker',
    'icheck.directives'
]);

angular.module('lotusinn.app.orders.checkout',
[
    'lotusinn.app.services',
    'ui.bootstrap',
    'icheck.directives'
]);

angular.module('lotusinn.app.orders.receipt',
[
    'lotusinn.app.services',
]);

angular.module('lotusinn.app.orders',
[
    'lotusinn.app.orders.addedit',
    'lotusinn.app.orders.checkout',
    'lotusinn.app.orders.receipt'
]);

/*--------------------- CUSTOMERS MODULES ---------------------*/
angular.module('lotusinn.app.customers.list',
[
    'lotusinn.app.services',
    'ui.bootstrap',
    'bw.paging'
]);

angular.module('lotusinn.app.customers.edit',
[        
    'lotusinn.app.services'
]);

angular.module('lotusinn.app.customers',
[
    'lotusinn.app.customers.list',
    'lotusinn.app.customers.edit'
]);


/*--------------------- DASHBOARD MODULES ---------------------*/
angular.module('lotusinn.app.dashboard',
[
    'ipCookie',
    'ui.bootstrap',
    'fk.eternicode-datepicker',
    'ui.bootstrap.contextMenu',
    'toastr'
]);

angular.module('lotusinn.app.dashboard').config(function (toastrConfig) {
    angular.extend(toastrConfig, {
        autoDismiss: false,
        containerId: 'toast-container',
        maxOpened: 0,
        newestOnTop: true,
        positionClass: 'toast-top-center',
        preventDuplicates: false,
        preventOpenDuplicates: false,
        target: 'body',
        closeButton: true,
        timeOut: 0
    });
});

/*--------------------- EQUIPMENT MODULES ---------------------*/
angular.module('lotusinn.app.equipment.list',
[
    'lotusinn.app.services',
    'ui.bootstrap',
    'icheck.directives'
]);

angular.module('lotusinn.app.equipment.addedit',
[
    'lotusinn.app.services'
]);

angular.module('lotusinn.app.equipments',
[
    'lotusinn.app.equipment.list',
    'lotusinn.app.equipment.addedit'
]);

/*--------------------- MONEY SOURCE MODULES ---------------------*/
angular.module('lotusinn.app.moneysource.list', [
    'lotusinn.app.services',
    'lotusinnModalService',
]);
angular.module('lotusinn.app.moneysource.addedit', [
    'lotusinn.app.services',
    'ui.bootstrap'
]);
angular.module('lotusinn.app.moneysource.detail', [
    'lotusinn.app.services',
    'ui.bootstrap'
]);

angular.module('lotusinn.app.moneysource.permission', [
    'lotusinn.app.services',
    'ui.bootstrap'
]);

angular.module('lotusinn.app.moneysource', [
    'lotusinn.app.moneysource.list',
    'lotusinn.app.moneysource.addedit',
    'lotusinn.app.moneysource.detail',
    'lotusinn.app.moneysource.permission'
]);
/*--------------------- ROLE MODULES ---------------------*/
angular.module('lotusinn.app.role.list', [
]);

angular.module('lotusinn.app.role.detail', []);

angular.module('lotusinn.app.role', [
    'lotusinn.app.role.list',
    'lotusinn.app.role.detail'
]);

/*--------------------- REPORT MODULES ---------------------*/
angular.module('lotusinn.app.report.list', [
    
    'lotusinn.app.services',
    'ui.bootstrap',
    'chart.js'
]);
angular.module('lotusinn.app.report', [
    'lotusinn.app.report.list'
]);

/*--------------------- MAIN APP ---------------------*/

angular.module('lotusinn.app',
[
    'ngAlert',
    'tp',
    'lotusinn.app.core',
    'lotusinn.app.houses',
    'lotusinn.app.users',
    'lotusinn.app.roomTypes',
    'lotusinn.app.rooms',
    'lotusinn.app.orders',
    'lotusinn.app.customers',
    'angular-loading-bar',
    'lotusinn.app.dashboard',
    'lotusinn.app.equipments',
    'lotusinn.app.report',
    'lotusinn.app.warehouses',
    'lotusinn.app.moneysource',
    'lotusinn.app.role'
]);

angular.module('lotusinn.app')
    .config([
        'cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
            cfpLoadingBarProvider.spinnerTemplate = '<div class="blockUI blockOverlay" style="z-index: 1000; border: none; margin: 0px; padding: 0px; width: 100%; height: 100%; top: 0px; left: 0px; opacity: 0.05; cursor: wait; position: fixed; background-color: rgb(85, 85, 85);"></div><div class="blockUI blockMsg blockPage" style="z-index: 1011; position: fixed; padding: 0px; margin: 0px; width: 30%; top: 40%; left: 35%; text-align: center; color: rgb(0, 0, 0); border: 0px; cursor: wait;"><div class="loading-message loading-message-boxed"><img src="data:image/gif;base64,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3Pbl3fbl3/fm4Pfn4fbo4/jq5Pjr5vns6Pnu6vrw7Pvy7/z39f37+v7+/f7+/v7+/v7+/v///////////////////////////////////////////////////////////////////////////////////////wj+AOkJHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMBeSK0aMWLSX3ap1QxhNVaufrYSx7BZIjtFAOwv6BPrz2sqiRo8aZAqUmEpqUbNSK0j1p9WUWLMa3UqwaytjV8WOLUiMqipyT8UGMkiO2NJTTldS2xN1D9mYB8lRGwwXsOHDh68hQ5YXMcFgTIOZ7IYNG0RjXb+G7JZoiucufxee6npqZGfPnwuLJj0StevQCSFTLR3ymmvUaBki65o75G3PsBO2BTqM5GnUXcRBjHazJGfkyQxaq1lMdctr2K0L/KWruy5f4Rw5Cyzm3btQ8cHKd/8lnh4w9d/bD4MPrH02+Nba07vG/Xsx/QAGKOCABBZo4IEIJqjgggw26OCDCwUEACH5BAkEAPYALAAAAABAAEAAhwAAAAEBAQICAgMDAwQEBAUFBQYGBgcHBwgICAkJCQoKCgsLCwwMDA0NDQ4ODg8PDxAQEBERERISEhMTExQUFBUVFRYWFhcXFxgYGBkZGRoaGhsbGxwcHB0dHR4eHh8fHyAgICEhISIiIiMjIyQkJCUlJSYmJicnJygoKCkpKSoqKisrKywsLC0tLS4uLi8vLzAwMDExMTIyMjMzMzQ0NDU1NTY2Njc3Nzg9PDlDQTpIRjpNSztSUDtXVDxbWDxgXD1jXz1nYj1qZT1taD1waz1zbT11bz14cT16cz17dDx9djx/eDyBejuDfDuEfTuFfjuFfzuGfzuGfzqHgDqHgDuHgDuGgDuGgTuGgTyFgjyFgzyEhD2CiD9/jEB9kEF7lEJ5l0R2nEV0oEZyo0dxpkdvqUhuq0ltrUlsrkprsEprsEpqsUpqskpqskpqsktqsktqsktqsktqsktqskxssU5vsFByr1F1rVN4rFV8qlh/qVqDp1yFpV6HpGCJomKLoWSMn2eQnWmRnGuSmm2RmW+RmHGSl3OSlnaTlXqTk36UlYCUl4KUmISTl4aSlY2Ti5OUhpuXfaCadqWccKifa62fY7KfW7ehU7qiTruiSr2kR76kRb+iRMCgRMGfRMKdRMSaRMWXRceURcmRRsqNRcyJRc2GRc6BRM9+RNB6QtF5Q9F5RNJ7RtJ8R9J/SdGAStGBStCDS8+ES86FS8yHTMqKTcmSVMyeYs+nb9GvedO1g9W7i9a/ktjDmNnHntrKpNvNqtzOrt7QtNzRutvTwNzUxeDWyuXYzuvaz+3b0e7c0u7e0+/f1PDf1fHg1fHh1vHh1/Li1/Lj2PLj2fPl2vTm3PXo3vbq4Pfs4vju5fjw5/ny6vn07Pr27vr38Pr38Pr38fr38vr38/n38/r49fr59/r5+fr6+vv6+vv6+vv7+/z7/P38/P79/v///////////////////////////////////////////////////////////////////////wj+AO0JHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMGM6HFcMEaJi5hAmU6YsXMtxdt4IfWMnJ0FqsFQpZUWMJaOhQ4sVrKW0KiufKvlAFYqIYLiqYJOt1Lq168BkYKv6Wvl0KyOvaZWKVXlsq51xBXGlvcryGCM+fBjhLUiNqlJYc2UepJYsseLHkCNbPIZo0CCpEMNlO0lsimfPgxwSY6W2JJfPn68xpBZ3rUhtqD9jVugrLq6RsGNPmZ0wV1xVJE/HhsZwV1xYJDujFtRQWFxdJY8Jmk6snENdYGthZSnul6ZNm3RIaSMMzJdjlr3Aq78lTrI99fCZSa4GX71ryOLqgwfm/pL+au4h8516vbgnkDa+JCifgQw26OCDEEYo4YQUVmjhhRhmqOGGDAUEACH5BAkEAPUALAAAAABAAEAAhwAAAAEBAQICAgMDAwQEBAUFBQYGBgcHBwgICAkJCQoKCgsLCwwMDA0NDQ4ODg8PDxAQEBERERISEhMTExQUFBUVFRYWFhcXFxgYGBkZGRoaGhsbGxwcHB0dHR4eHh8fHyAgICEhISIiIiMjIyQkJCUlJSYmJicnJygoKCkpKSoqKisrKywsLC0tLS4uLi8vLzAwMDExMTIyMjMzMzQ0NDU1NTY2Njc3Nzg4ODk5OTo6Ojs7Ozw8PD09PT4+Pj8/P0BAQEFBQUJCQkNDQ0REREVFRUZGRkdHR0lJTUtMU01PWU9RX1BTZFJVaVNXblVZc1Zbd1dce1hdf1lfhVphilpijltjkVlnlFxkl1xlmVxlm1xmnFxmnl1mn11moF1moF1moV1moV1moV1moV1moV1moV1moV1nol1noV1noV1noV1noV1noV1noWFqm2ZulmtykHB2iXd6gn59e4SAdYqDb5WIZJ2MW6WRVKqVUK6YTLCZSrGaSbKbR7WYRrmURb2QQ8OKQceGQMyBP85/PtF9PdJ8PdR7PNV6PNV6PNZ6O9Z6O9V6PNR6PdN6PtJ7QNB8Qs58Rst9Sch/TsOBVL2EXbqGY7aIabKLca2OeaiSg6KWj5+ZlZ2dnZ6enp+fn6CgoKGhoaKioqOjo6SkpKWlpaampqenp6ioqKmpqaqqqqurq6ysrK2tra6urq+vr7CwsLGxsbKysrOzs7S0tLW1tba2tre3t7i4uLm5ubq6uru7u7y8vL29vb6+vr/AwL/Dw8DFxcHHx8HIycLKysbP0MnT1MzW2c7a3NDc3tLe4NPf4tXh49ji5dzk5uDm6Ofn5+vo5+/p5/Dq5/Dq6PDr6e/s6+/u7vDv7/Hw8PPx8fXz8/b09fb19vf2+Pj4+fn6/Pv7/Pz8/f39/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v///////////////////////////////////////////////////////////////////////////////////wj+AOsJHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMAV2axZz4bM4Z3J26lbTYLc3OYN2QjjtJbOgSAtOu7So6aWiK2UhDcqMYKWmWC+xPDY1J0+B0rCKlbbyWlc5BMOKbUp25TCkcZ6lXcu25bVjsph9Hfgt0tpIPQ0SExuJWGCD04jxIgYV5TNmzLwdLvjszJXLV5BNHkgKM2bJmz1j1jyZmejLwBhO00Rp0SXDJU9nXvittdjUJDuLzrZw8N+SlT2TVsiL7qKTkCM3LE735TRkxqCBm7t2kkthfbJn30aQ6diW0bQ9a8c9kBitTLwarzQmXvvmeuzb93kfv/37bfKNva8XXnux6fsJxF2ABBZo4IEIJqjgggw26OCDEEYoYYQBAQAh+QQJBAD0ACwAAAAAQABAAIcAAAABAQECAgIDAwMEBAQFBQUGBgYHBwcICAgJ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lFl1l1lzmllynFlynVlznVlznVpxnF1vm2FtmmVrmWhomGxml29jlXNhlHddknxakIBVjohQjI9LipdDhqI8g6s1gLQvfrwtfb8rfcEpfMMofMQnfMUmfMYme8Yle8Yle8Yle8Yle8YmfMYofMUqfMUrfcQufcMxfsI1f8A5gL4+gbxFg7lNhLVThrJbiK9jiqxpjKp4jqSFkJ6RkpiclJOnl4+xmYq8nIW/noPCn4HHoXvLo3XQpW/Vp2naqmLdql7gqljiqVLkqE3lp0rmpkbnpkPopUHopD/opD3opD7opD7opT/opUHopkLopkTop0fnqEnnqUzmqlHlq1TkrVnjrl/isGTis23it3XjvIDkwIrkxJTlx5nlyZ7ly6Llzaflzqzl0LHm1Lnn2L/n28Pl3crk387l4M3o4cvo4svp48zq5M3q5c/t59Du6NHv6NLw6dPx6tTz69X07Nf27dn57tz78N/88uP89Of89uz9+PD9+fP++/f9/fv+/v7+/v7+/v7///////////////////////////////////////////////////////////////////////////////////////////8I/gDpCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLks6qOHFSRdlLhdZm6nTi7CZCUTtnfvJ58FNQmkQNGg06NClBZUebtnRGtZxBTzurjGtJbZKhr456FmSGDBmzrS0rfV3riBzEa9dMrp0rliExWHhhDRvpbO7aTQ2j5c1rU6Tfr3UVAhuM99dItXMdWWX4izEsxyK7sk2ssDJjzCOpUYWozDIxpwUX5wWG2mC3aNG6tZ5Nm+A4arjR0qbGJo1vNtRqv/FN/I3BccXy/pKtkhrx58EJ3h1sS/dJ5899Rx9oGVZhlNizQG8X2P20yuHPjRcsv3Ib+jRvthnkZZl5e2ryD16rNThYbXrjHEMMMXH9Z+CBCCao4IIMNujggxBGKOGEFFaIWkAAIfkECQQA9AAsAAAAAEAAQACHAAAAAQEBAgICAwMDBAQEBQUFBgYGBwcHCAgICQkJCgoKCwsLDAwMDQ0NDg4ODw8PEBAQEREREhISExMTFBQUFRUVFhYWFxcXGBgYGRkZGhoaGxsbHBwcHR0dHh4eHx8fICAgISEhIiIiIyMjJCQkJSUlJiYmJycnKCgoKSkpKioqKysrLCwsLS0tLi4uLy8vMDAwMTExMjIyMzMzNDQ0NTU1NjY2Nzc3ODg4OTk5Ojo6Ozs7PDw8PT09P0M/QklBRE9DRlVESFpGSl9HTGRJTWhKT2xLUHBMUXRNUndNVHxOVYBPVYRPVodQV4lQV4tQV41QWI5RWI9RWJBRWJFRWJJRWJJRWJJRWJNRWZNRWZJRWZJRWZJRWZFRWZFRV49WVI5cUoxiT4ppTIhwSYZ4RoSAQoGJO36YN3yhNHupMXqvLnm1LHm6Kni+J3jDJXfGJHfJI3fMInfPIXfRIHfSIHjTIHjTIXjTInnTI3nTJHnSKHzTLH7TL4DTMoHTNIHSNYDQNn/NOH7KOnvFPXi+RHe3TXWvVXSoXnKgaXCWdW2LfWyEiGh6k2VxnGNopWBgrV1YrlxXsFtVsVpUsVpTsllSslhRs1hRs1hQs1lQs1lQtFpQtFtQtV1Qtl5Qt2BQuWNQvWpOwXFNxnhLy39I0IdG1IxF15BD2ZRC3ZhA4J0+4qA95KM85qU76Kc66ak66qs666w566w666w76qw86q0+6a1A6K1C3a5M0q9Vx7BfvbBnt7FtsrFxr7J2qbN7q7SAqrWGqreNq7iSsbmat72kvcCvxMa8yMnBzM7I1dLP3dbW3tfX39nZ4dra4tzc493d5N3d59/f6uPj7+jo8+zs9u/v+PHx+fLy+vPz+vTz+/Xz+/X0+/b2+/f3/Pj4/Pn5/fr6/fz8/v39/v7+/v7+/v7+/v7+/v7+////////////////////////////////////////////////////////////////////////////////////////////CP4A6QkcSLCgwYMIEypcyLChw4cQI0qcSLGixYsYM2rcyLGjx48gQ4ocSbKkyZMoU6pcybJlxWrGihmr5pLhMUw4cR6rqfBTTpyftPE8uOxnzmVDDRY1iglpUoLSmDZlqeyPVWPdDI4yKoqlMTpgwf4xWK1UzlE0V/IJGxbaU4LW2Ibd+VZgXLl06Nalt1butIfScMWKhSttyK9sxzqcNrhxLGsjq1rV21Cw41hO91p2jGuvwM2NO3sGdjmWM8/0toEWjTqcs2XLpKF2SW3Yr1/B3KLmlqW3b917i/n2HQz1r+G9ex2shqs5sG0rjyNXXpCx41vhVApHXrwg6MGnUzvyRg58YOlYwFZSI3Z7GLWDtkorQ/3dNOpql3FlR21NWXNn+80m4IAEFmjggQgmqOCCDDbo4IMQRlhQQAAh+QQJBADyACwAAAAAQABAAIcAAAABAQECAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMuJiU4KihCLSpMMSxVNC5dNjBmOTJtOzN0PjV7QDaGQzePRTmXRzqeSTukSjyoTDyuTT2yTj22Tz64Tz66UD67UD69UT6+UT6/UT++UT+/U0C9Uj+6VEC3VkCzWEGuW0KnXkOgYkSaZUaTaEeIb0l9dUtyek1pf09ghFBYiFJUi1JRjFNPjlNNj1RMj1RLkFRKkVRJkVVJkVZIkVZIkVdHkFhHkFpGj1xHkF5FjmFEjWRBjG0+inY6iIA3hYszg5cvgKMqfbAnfLokesIieckgeM8ed9Uddtkbdd0adeAadOAadOEbdOAcdN8edN0gdNoidNgkddYnddMqdc8udcsxdsg2dsM7d75Cd7hJeLJReqpXe6ZcfKJifZ5ofplvgJR2gpB+g4uGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmfm5Olno6roImwo4S2pYC8o3rConXGonHKo27PpWjTqGLYrFzcslXgs1His03mtEjotUXqtULstj/ttj3utjvvtjrvtjnvtjnwtzrwtzzwuD7wuUDwuULwukTwu0fwvErvvU3vvU/vvVHuvlPtvlbtv1nsv1znwGvkwXfgw4jcxZbax6Day6jXzLDV0LfT1MDR2cnR29DR3dfQ39/U4uXX5Ovb5u3f6PDj6vLn7PPr7vTt7/T39vb++/n+/fv+/vz+/v3+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7///////////////////////////////////////////////////////////////////////////////////////////////////////////8I/gDlCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXFZKJ48BC1zOXCZTJz8mBmM2FMnTyI9UQ4CuhMls2SWkP4U6colc/ytJlap9lBUEaNqfQztWsdaQat+dDpY2nKrmithjVGjJgxsyiboe2qdmjBuVPr2h3IFW0duHsFPuvbpmrgg8+STlwGSpSoYoAPJwNa9vDAokC1WpZnlEexzZyNft6MWadmy5PJRvYIjRlPiYwdQxYJbVCV21yUgZZH6LZvLqvt+h6u2zKz4b5PH0Z+u3jBa8aGDTN2bWXv4VymhbUlq7usXdVTQEL78jvZQWHevStH6ZpZcHnB0ncPBhqYfFn0N8eXPwy0tfvvDZXMfsIEuNuBCCao4IIMNujggxBGKOGEFFaIUEAAOwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" align=""><span>&nbsp;&nbsp;LOADING...</span></div></div>';
        }
    ]);

angular.module('lotusinn.app').run(function ($rootScope, alertSvc, $xhttp, ipCookie, $q) {

    $rootScope.alertSvc = alertSvc;
    $rootScope.hasError = function (field, errorType) {
        switch (errorType) {
            case 'required':
                return field.$dirty && field.$error.required;
            case 'pattern':
                return field.$dirty && field.$error.pattern;
            case 'invalid':
                return field.$dirty && field.$invalid;
            default:
                return false;
        }
    }

    $rootScope.userPermissions = [];

    $rootScope.initialized = false;

    $rootScope.initPermissions = function () {
        var defer = $q.defer();
        setTimeout(function() {
            if ($rootScope.initialized) {
                defer.resolve();
            } else {
                try {
                    var currentUser = JSON.parse(Base64.decode(ipCookie("AuthId"))).User;
                    $xhttp.get('/api/users/getpermissions?userid=' + currentUser.Id).then(function (response) {
                        $rootScope.userPermissions = response.data;
                        $rootScope.initialized = true;
                        defer.resolve();
                    });
                } catch (e) {
                    location.href = '/login';
                    defer.reject();
                }
            }
        }, 100);
                
        return defer.promise;
    }

    $rootScope.checkAccessPermission = function (permissions, objectType) {
        var hasPermission = false;
        _.forEach(permissions, function(p) {
            if ($rootScope.hasPermission(p, objectType)) {
                hasPermission = true;
            }
        });

        if (!hasPermission)
            location.href = '/error/forbidden';
    }

    $rootScope.hasPermission = function(permission, objectType) {
        var perm = _.find($rootScope.userPermissions.Permissions, { ObjectType: objectType });
        if (perm) {
            if (permission === 'None') return perm.Permission === 0;
            if (permission === 'Read') return (perm.Permission & 1) === 1;
            if (permission === 'Create') return (perm.Permission & 2) === 2;
            if (permission === 'Edit') return (perm.Permission & 4) === 4;
            if (permission === 'Delete') return (perm.Permission & 8) === 8;
        }
        return false;
    };

});