mergeInto(LibraryManager.library, {

  GetJSON: function (path, objectName, callback, fallback) {
    window.alert("init");
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    try {
        console.log(window.unityInstance);
        ref(window.database, parsedPath).once('value').then(function(snapshot) {
          window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val()));
        });
    } catch (error) {
        window.unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

});