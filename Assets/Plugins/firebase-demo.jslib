mergeInto(LibraryManager.library, {

  GetJSON: function (path, objectName, callback, fallback) {
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        get(ref(database, parsedPath)).then(function(snapshot) {
          unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val()));
        });
    } catch (error) {
        window.alert(unityInstance);
        unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

  SetJSON: function (path, objectName, callback, fallback) {
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        set(ref(database, parsedPath), {username: 'Ivan2', password:123456}).then(function() {
        unityInstance.SendMessage(parsedObjectName, parsedCallback, parsedFallback);
        });
    } catch (error) {
        unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

});