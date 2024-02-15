mergeInto(LibraryManager.library, {
  FirebaseLogin: function (email, password, objectName, callback, fallback) {
    var parsedEmail = UTF8ToString(email);
    var parsedPassword = UTF8ToString(password);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    signInWithEmailAndPassword(auth, parsedEmail, parsedPassword)
    .then((userCredential) => {
      const user = userCredential.user;
      window.user = user;
      const pathRef = ref(database, "Users/" + user.uid);
      get(pathRef).then((snapshot) => {
      window.instanceID = (snapshot.size+1);
      window.instancePath = "Users/" + (user.uid).toString() + "/Instance" + (instanceID).toString() + "/";
      set(ref(database, instancePath), {Actions: "", Simulation: "", Player:""});
      })
    }).then(() => {
      window.alert("success");
      unityInstance.SendMessage(parsedObjectName, parsedCallback, "TBD");
    })
    .catch((error) => {
      const errorCode = error.code;
      const errorMessage = error.message;
      window.alert(errorMessage);
    });
  },
});