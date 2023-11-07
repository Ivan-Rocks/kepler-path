mergeInto(LibraryManager.library, {
  FirebaseLogin: function (path, objectName, callback, fallback) {
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    const pathRef = ref(database, "Users/" + parsedPath);
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          window.alert("Username taken, please name another one");
          unityInstance.SendMessage(parsedObjectName, parsedFallback, "TBD");
        } else {
          window.alert("Welcome to HoloOrbits!");
          set(pathRef, {username: parsedPath, password:123456}).then(function() {
            unityInstance.SendMessage(parsedObjectName, parsedCallback, "TBD");
          });
        }
      })
    .catch((error) => {
      console.error("Error reading data: ", error);
    });
  },
});