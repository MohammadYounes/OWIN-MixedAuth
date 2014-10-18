function AppDataModel() {
    var self = this;
    var baseUrl = $('base').attr('href');
    // Routes
    self.userInfoUrl = baseUrl + "api/Me";
    self.siteUrl = baseUrl;

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };
}
