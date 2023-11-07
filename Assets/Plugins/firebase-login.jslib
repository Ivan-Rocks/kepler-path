mergeInto(LibraryManager.library, {
  FirebaseLogin: function (path, objectName, callback, fallback) {
    var parsedPath = UTF8ToString(path);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    const pathRef = ref(database, parsedPath);
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          console.log("Path exists, data: ", snapshot.val());
        }  else {
          console.log("Path does not exist");
        }
      })
    .catch((error) => {
      console.error("Error reading data: ", error);
    });
  },
});