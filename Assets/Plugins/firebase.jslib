mergeInto(LibraryManager.library, {

  GetJSON: function (path, objectName, callback, fallback) {
    console.log("can access function")
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName)
    var parsedCallback = UTF8ToString(callback)
    var parsedFallback = UTF8ToString(fallback)
    console.log(parsedPath, parsedObjectName, parsedCallback, parsed)
    window.alert("Firebase init works!");
    try {
        console.log("successfull");
        firebase.database().ref(parsedPath).once('value').then(function(snapshot) {
        window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot.val));
    });
    } catch (error) {
      console.log("failed");
        window.unityInstance.SendMessage(parsedObjectName, parsedFallback, error.message);
    }
  },

});