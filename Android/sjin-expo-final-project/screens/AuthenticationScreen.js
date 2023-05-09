import { FirebaseRecaptchaVerifierModal } from "expo-firebase-recaptcha";
import { firebaseConfig, auth, signInWithPhoneNumber } from "../FirebaseConfig";
import React, { useState, useRef } from "react";
import {
  View,
  Text,
  Alert,
  TextInput,
  TouchableOpacity,
  StyleSheet,
} from "react-native";
import firebase from "firebase/compat/app";
//+1-555-521-5554
const AuthenticationScreen = (props) => {
  const [phoneNum, setPhoneNumber] = useState("");
  const [code, setCode] = useState("");
  const recaptchaVerifier = useRef(null);

  const sendVerification = async () => {
    const phoneNumber = `+1 ${phoneNum}`;
    const appVerifier = recaptchaVerifier.current;

    signInWithPhoneNumber(auth, phoneNumber, appVerifier).then(
      (confirmationResult) => {
        console.log("Sent");
        window.confirmationResult = confirmationResult;
      }
    );
  };
  const confirmCode = () => {
    confirmationResult
      .confirm(code)
      .then((result) => {
        console.log("Success");
        props.navigation.navigate("CalendarScreen");
      })
      .catch((error) => {
        Alert.alert("Verification code is not correct.");
      });
  };

  return (
    <View style={styles.form}>
      <FirebaseRecaptchaVerifierModal
        ref={recaptchaVerifier}
        firebaseConfig={firebaseConfig}
      />
      <View style={styles.container}>
        <Text style={styles.login}>Login</Text>
        <View style={styles.contentContainer}>
          <View style={styles.leftContent}>
            <Text style={styles.label}>Phone Number</Text>
            <TextInput
              onChangeText={setPhoneNumber}
              keyboardType="phone-pad"
              autoComplete="tel"
              style={styles.input}
            />
          </View>
          <View style={styles.rightContent}>
            <TouchableOpacity onPress={sendVerification} style={styles.button}>
              <Text>Send</Text>
            </TouchableOpacity>
          </View>
        </View>
        <View style={styles.contentContainer}>
          <View style={styles.leftContent}>
            <Text style={styles.label}>Verification Code</Text>
            <TextInput
              onChangeText={setCode}
              keyboardType="number-pad"
              style={styles.input}
            />
          </View>
          <View style={styles.rightContent}>
            <TouchableOpacity onPress={confirmCode} style={styles.button}>
              <Text>Confirm</Text>
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </View>
  );
};

export default AuthenticationScreen;

const styles = StyleSheet.create({
  form: {
    padding: 15,
    paddingTop: "50%",
    height: "100%",
  },
  container: {
    width: "100%",
    height: "60%",
    backgroundColor: "#f5eef5",
    borderRadius: 30,
    borderWidth: 3,
    borderColor: "white",
    padding: 15,
    paddingBottom: 45,
  },
  login: {
    marginTop: 20,
    fontSize: 25,
    fontWeight: "bold",
    textAlign: "center",
  },
  contentContainer: {
    //flex: 1,
    flexDirection: "row", // Render components horizontally
    justifyContent: "space-between", // Distribute components evenly
    alignItems: "center",
  },
  leftContent: {
    width: "60%",
    height: "40%",
    alignItems: "center",
    justifyContent: "center",
  },
  rightContent: {
    width: "30%",
    height: "40%",
  },
  label: { fontSize: 13, marginBottom: 3 },
  input: {
    backgroundColor: "white",
    width: "100%",
    borderRadius: 10,
    paddingLeft: 20,
    fontSize: 14,
  },
  button: {
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "white",
    width: "100%",
    height: "100%",
    borderRadius: 10,
  },
});
