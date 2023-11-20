mergeInto(LibraryManager.library, {
  FirebaseLogActionData: function (message) {
    var parsedMessage = UTF8ToString(message);
    const pathRef = ref(database, instancePath+"Actions");
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          let count = snapshot.size+1;
          path = instancePath+"Actions/";
          update(ref(database, path), { [count] : parsedMessage});
        }
      })
    .catch((error) => {
      console.error("Error sending data: ", error);
    });
  },
  FirebaseLogSimulationData: function (message) {
    var parsedMessage = UTF8ToString(message);
    const pathRef = ref(database, instancePath+"Simulation");
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          let count = snapshot.size+1;
          path = instancePath+"Simulation/";
          update(ref(database, path), { [count] : parsedMessage});
        }
      })
    .catch((error) => {
      console.error("Error sending data: ", error);
    });
  },
  FirebaseLogPlayerData: function (message) {
    var parsedMessage = UTF8ToString(message);
    const pathRef = ref(database, instancePath+"Player");
    get(pathRef)
      .then((snapshot) => {
        if (snapshot.exists()) {
          let count = snapshot.size+1;
          path = instancePath+"Player/";
          update(ref(database, path), { [count] : parsedMessage});
        }
      })
    .catch((error) => {
      console.error("Error sending data: ", error);
    });
  },
});