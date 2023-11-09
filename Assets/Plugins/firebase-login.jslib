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
          window.alert("Welcome to HoloOrbits!");
          window.instanceID = (snapshot.size+1);
          window.instancePath = "Users/" + parsedPath + "/Instance" + (snapshot.size+1).toString() + "/";
          set(ref(database, instancePath), {Actions: "", Simulation: ""});
          unityInstance.SendMessage(parsedObjectName, parsedCallback, "TBD");
        } else {
          window.alert("No such Username");
        }
      })
    .catch((error) => {
      console.error("Error reading data: ", error);
    });
  },
});