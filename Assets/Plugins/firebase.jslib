mergeInto(LibraryManager.library, {

  GetJSON: function (path, objectName, callback, fallback) {
    window.alert("init");
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        window.alert(unityInstance);
        window.get(window.ref(window.database, parsedPath)).then(function(snapshot) {
          window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val()));
        });
    } catch (error) {
        window.alert(unityInstance);
        window.unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

  SetJSON: function (path, objectName, callback, fallback) {
    window.alert("init");
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        console.log(window.unityInstance);
        window.set(window.ref(database, parsedPath), {username: 'Ivan2', password:123456}).then(function() {
          window.unityInstance.SendMessage(parsedObjectName, parsedCallback, parsedFallback);
        });
    } catch (error) {
        window.unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

});