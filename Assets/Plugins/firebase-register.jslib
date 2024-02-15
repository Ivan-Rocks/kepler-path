mergeInto(LibraryManager.library, {
  FirebaseRegister: function (email, password, confirmedPassword, objectName, callback, fallback) {
    var parsedEmail = UTF8ToString(email);
    var parsedPassword = UTF8ToString(password);
    var parsedconfirmedPassword = UTF8ToString(confirmedPassword);
    var parsedObjectName = UTF8ToString(objectName);
    var parsedCallback = UTF8ToString(callback);
    var parsedFallback = UTF8ToString(fallback);
    if (parsedPassword != parsedconfirmedPassword) {
      window.alert("Passwords do not match")
    } else {
      createUserWithEmailAndPassword(auth, parsedEmail, parsedPassword)
        .then((userCredential) => {
          window.user = userCredential.user;
          console.log(user);
          const userDocRef = doc(firestore, 'Users', user.uid);
          setDoc(userDocRef, { email: parsedEmail});
        }).then(() => {
          window.instanceID = 1;
          window.instancePath = "Users/" + (user.uid).toString() + "/Instance" + (instanceID).toString() + "/";
          console.log(database);
          console.log(instancePath);
          set(ref(database, instancePath), {Actions: "", Simulation: "", Player:""});
        }).then(() => {
          window.alert("New User Created");
          unityInstance.SendMessage(parsedObjectName, parsedCallback, "TBD");
        })
        .catch((error) => {
          const errorCode = error.code;
          const errorMessage = error.message;
          window.alert(errorMessage);
        });
    }
  },
});