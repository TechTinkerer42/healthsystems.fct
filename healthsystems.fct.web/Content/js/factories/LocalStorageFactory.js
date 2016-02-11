  app.factory('LocalStorageFactory', function LocalStorageFactory($window) {
    'use strict';
    var store = $window.localStorage;

    return {
      getKey: getKey,
      setKey: setKey
    };

    function getKey(key) {
      return store.getItem(key);
    }

    function setKey(key, value) {
      if (key && value) {
        store.setItem(key, value);
      } else {
        store.removeItem(key);
      }
    }

  });