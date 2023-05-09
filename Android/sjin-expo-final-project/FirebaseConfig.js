// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import {
  getFirestore,
  collection,
  addDoc,
  getDocs,
  doc,
  updateDoc,
  deleteDoc,
} from "firebase/firestore";

import { getAuth, signInWithPhoneNumber } from "firebase/auth";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyA2oqqXDuiAIACcqc8x0azTUBkcOVJm7kY",
  authDomain: "info3141-finalproject-bf760.firebaseapp.com",
  databaseURL:
    "https://info3141-finalproject-bf760-default-rtdb.firebaseio.com",
  projectId: "info3141-finalproject-bf760",
  storageBucket: "info3141-finalproject-bf760.appspot.com",
  messagingSenderId: "185618230151",
  appId: "1:185618230151:web:6e5095461f0e9a32eec122",
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

const auth = getAuth(app);

const db = getFirestore(app);

export {
  firebaseConfig,
  app,
  db,
  getFirestore,
  collection,
  addDoc,
  getDocs,
  doc,
  updateDoc,
  deleteDoc,
  auth,
  signInWithPhoneNumber,
};
