mergeInto(LibraryManager.library, {
  FirebaseLogin: function (path, objectName, callback, fallback) {
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        window.get(window.ref(window.database, parsedPath)).then(function(snapshot) {
          window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val()));
        });
    } catch (error) {
        window.alert(unityInstance);
        window.unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },
});