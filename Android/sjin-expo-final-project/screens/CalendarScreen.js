import React, { useState } from "react";
import {
  View,
  Text,
  Alert,
  StyleSheet,
  Image,
  TouchableOpacity,
  ScrollView,
  ActivityIndicator,
} from "react-native";
import { Calendar } from "react-native-calendars";
import * as SMS from "expo-sms";
import * as Permissions from "expo-permissions";
import { db, collection, getDocs } from "../FirebaseConfig";

const CalendarScreen = (props) => {
  // Location
  [currentLocation, setCurrentLocation] = useState();
  [isFetching, setIsFetching] = useState();
  const [latitude, setLatitude] = useState();
  const [longitude, setLongitude] = useState();

  // Calendat
  const [selectedDay, setSelectedDay] = useState();
  const [isSelected, setIsSelected] = useState(false);

  // Camera
  const [selectedImage, setSelectedImage] = useState();

  const [contact, setContact] = useState();
  const [memo, setMemo] = useState();

  const [dataId, setDataId] = useState("-1");

  const getData = async (day) => {
    const querySnapshot = await getDocs(collection(db, "diary_app"));

    let isFound = false;
    setDataId("-1");

    querySnapshot.forEach((doc) => {
      if (doc.data().date === day) {
        setDataId(doc.id);
        setSelectedDay(day);
        setSelectedImage(doc.data().imageUri);
        setLatitude(doc.data().latitude);
        setLongitude(doc.data().longitude);
        setContact(doc.data().phoneNum);
        setMemo(doc.data().memo);
        isFound = true;
      }
    });
    if (!isFound) {
      resetData();
    }
  };

  const resetData = () => {
    setSelectedImage("null");
    setLatitude("-");
    setLongitude("-");
    setContact("");
    setMemo("");
  };

  sendMessageWithSMS = async () => {
    const isAvailable = await SMS.isAvailableAsync();
    if (isAvailable) {
      const { result } = await SMS.sendSMSAsync(
        [contact],
        "Hello from seolan diary application"
      );
      console.log(result);
    } else {
      console.log("SMS is not available on this device");
    }
  };

  hasLocationPermissions = async () => {
    let { status } = await Permissions.askAsync(Permissions.LOCATION);
    if (status !== "granted") {
      Alert.alert(
        "Location services are not enabled! You need to enable them to use this app!"
      );
      return false;
    }
    return true;
  };
  return (
    <ScrollView style={styles.form}>
      <View style={styles.calendarContainer}>
        <Calendar
          onDayPress={(day) => {
            if (day.dateString !== selectedDay) {
              setSelectedDay(day.dateString);
              getData(day.dateString);
            }
            setIsSelected(true);
          }}
          markedDates={{
            [selectedDay]: {
              selected: true,
              disableTouchEvent: true,
              //color: "orange",
            },
          }}
          style={styles.calendar}
        />
      </View>

      {isSelected ? (
        <>
          <Text style={styles.date}>{selectedDay}</Text>
          <View style={styles.contentContainer}>
            <View style={styles.leftContent}>
              <View style={styles.defaultImage}>
                <Image
                  style={styles.image}
                  source={
                    selectedImage === "null"
                      ? require("../defaultImg.png")
                      : { uri: selectedImage }
                  }
                />
              </View>
            </View>
            <View style={styles.rightContent}>
              {isFetching ? (
                <ActivityIndicator size="small" color="#0000ff" />
              ) : (
                <>
                  <Text style={{ fontSize: 13, fontWeight: "bold" }}>
                    Image Location
                  </Text>
                  <Text style={{ fontSize: 12, marginTop: 3 }}>
                    Latitude: {latitude}
                  </Text>
                  <Text style={{ fontSize: 12 }}>Longitude: {longitude}</Text>
                </>
              )}
              <Text style={{ fontSize: 13, fontWeight: "bold", marginTop: 15 }}>
                Contact
              </Text>

              <TouchableOpacity
                style={styles.callButton}
                onPress={sendMessageWithSMS}
              >
                <Text style={{ fontSize: 14, color: "grey" }}>{contact}</Text>
              </TouchableOpacity>
            </View>
          </View>
          <Text
            style={{
              marginTop: 5,
              marginBottom: 0,
              fontSize: 13,
              fontWeight: "bold",
            }}
          >
            MEMO
          </Text>
          <Text
            style={{
              marginTop: 2,
              marginBottom: 3,
              backgroundColor: "white",
              borderRadius: 10,
              paddingLeft: 7,
            }}
          >
            {memo}
          </Text>
          <View
            style={{
              alignItems: "center",
              justifyContent: "center",
              marginTop: 10,
            }}
          >
            <TouchableOpacity
              style={styles.editButton}
              onPress={() =>
                props.navigation.navigate("EditScreen", {
                  selectedImage: selectedImage,
                  selectedDay: selectedDay,
                  latitude: latitude,
                  longitude: longitude,
                  contact: contact,
                  memo: memo,
                  dataId: dataId,
                  getData: getData,
                })
              }
            >
              <Text
                style={{
                  color: "white",
                  textAlign: "center",
                  fontWeight: "bold",
                }}
              >
                Edit
              </Text>
            </TouchableOpacity>
          </View>
        </>
      ) : null}
    </ScrollView>
  );
};

export default CalendarScreen;

const styles = StyleSheet.create({
  form: { padding: 30, paddingTop: 30, backgroundColor: "#f5eef5" },
  calendarContainer: {
    flex: 1,
    backgroundColor: "#fffff8",
    borderRadius: 10,
  },
  calendar: {
    borderRadius: 10,
  },
  date: {
    margin: 10,
    fontSize: 20,
    fontWeight: "bold",
  },
  defaultImage: {
    width: 200,
    height: 150,
    borderRadius: 10,
    backgroundColor: "#ccc",
    justifyContent: "center",
    alignItems: "center",
    borderWidth: 2,
    borderColor: "#f4d0f4",
  },
  image: {
    width: 200,
    height: 150,
    borderRadius: 10,
  },
  callButton: {
    alignItems: "center",
    justifyContent: "center",
    width: 115,
    height: 20,
    backgroundColor: "white",
    borderRadius: 10,
    marginTop: 9,
  },
  contentContainer: {
    flex: 1,
    flexDirection: "row", // Render components horizontally
    justifyContent: "space-between", // Distribute components evenly
    alignItems: "center",
  },
  leftContent: {
    width: "65%",
    alignItems: "center",
    justifyContent: "center",
  },
  rightContent: {
    width: "35%",
    height: "100%",
  },
  editButton: {
    alignItems: "center",
    justifyContent: "center",
    width: 135,
    height: 40,
    backgroundColor: "#f4d7f5",
    borderRadius: 10,
  },
});
