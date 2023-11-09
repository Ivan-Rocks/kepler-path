mergeInto(LibraryManager.library, {
  FirebaseLogActionData: function (message) {
    var parsedMessage = UTF8ToString(message);
    const pathRef = ref(database, instancePath+"Actions");
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          count = snapshot.size+1;
          path = instancePath+"Actions/";
          push(ref(database, path), { id:count, message:parsedMessage});
        }
      })
    .catch((error) => {
      console.error("Error sending data: ", error);
    });
  },
});