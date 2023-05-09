// Import the functions you need from the SDKs you need
import firebase from "firebase/compat/app";
import "firebase/compat/auth";
import "firebase/compat/firestore";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
export const firebaseConfig = {
  apiKey: "AIzaSyA2oqqXDuiAIACcqc8x0azTUBkcOVJm7kY",
  authDomain: "info3141-finalproject-bf760.firebaseapp.com",
  databaseURL:
    "https://info3141-finalproject-bf760-default-rtdb.firebaseio.com",
  projectId: "info3141-finalproject-bf760",
  storageBucket: "info3141-finalproject-bf760.appspot.com",
  messagingSenderId: "185618230151",
  appId: "1:185618230151:web:6e5095461f0e9a32eec122",
};

if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
}
