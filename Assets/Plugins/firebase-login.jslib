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
      window.alert("success");
      window.userEmail = parsedEmail;
      window.user = userCredential.user;
      console.log(user)
      unityInstance.SendMessage(parsedObjectName, parsedCallback, "TBD");
    })
    .catch((error) => {
      const errorCode = error.code;
      const errorMessage = error.message;
      window.alert(errorMessage);
    });
  },
});