  app.factory("AuthInterceptorFactory", function AuthInterceptorFactory(AuthTokenFactory) {
    "use strict";
    return {
      request: addToken
    };

    function addToken(config) {
      var token = AuthTokenFactory.getToken();
      if (token) {
          config.headers = config.headers || {};
          if (config.url.indexOf("maps.google.com") === -1) { // if we are NOT interacting with google maps api via ajax
              config.headers.Authorization = "Bearer " + token; // add Authorization header
          }
      }
      return config;
    }
  });