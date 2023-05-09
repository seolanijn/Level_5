import React, { useState } from "react";
import {
  View,
  TextInput,
  Text,
  Alert,
  StyleSheet,
  Image,
  TouchableOpacity,
  ActivityIndicator,
} from "react-native";
import ImageSelector from "../components/ImageSelector";
import * as Location from "expo-location";
import * as Permissions from "expo-permissions";
import {
  db,
  collection,
  addDoc,
  getDocs,
  updateDoc,
  deleteDoc,
  doc,
} from "../FirebaseConfig";

const EditScreen = (props) => {
  // Location
  [isFetching, setIsFetching] = useState();
  [latitude, setLatitude] = useState(props.route.params.latitude);
  [longitude, setLongitude] = useState(props.route.params.longitude);

  // Calendar
  const [selectedDay, setSelectedDay] = useState(
    props.route.params.selectedDay
  );

  // Camera
  const [selectedImage, setSelectedImage] = useState(
    props.route.params.selectedImage
  );

  const [memo, setMemo] = useState(props.route.params.memo);
  const [contact, setContact] = useState(props.route.params.contact);

  const [dataId, setDataId] = useState(props.route.params.dataId);

  const addData = async () => {
    try {
      const docRef = await addDoc(collection(db, "diary_app"), {
        date: selectedDay,
        imageUri: selectedImage,
        latitude: latitude,
        longitude: longitude,
        phoneNum: contact,
        memo: memo,
      });
      console.log("Document added with ID: ", docRef.id);
    } catch (e) {
      console.error("Error adding document: ", e);
    }
  };

  const updateData = async () => {
    try {
      const currentRef = doc(db, "diary_app", dataId);

      await updateDoc(currentRef, {
        imageUri: selectedImage,
        latitude: latitude,
        longitude: longitude,
        phoneNum: contact,
        memo: memo,
      });
      console.log("Document updated with ID: ", currentRef.id);
    } catch (e) {
      console.error("Error updating document: ", e);
    }
  };

  const imageSelectedHandler = async (imagePath) => {
    setSelectedImage(imagePath);
    setIsFetching(true);

    if (await hasLocationPermissions()) {
      let location = await Location.getCurrentPositionAsync({
        accuracy: Location.Accuracy.Highest,
      });
      setLatitude(location.coords.latitude);
      setLongitude(location.coords.longitude);
    }

    setIsFetching(false);
  };

  const resetImage = () => {
    Alert.alert(
      "Warning",
      "Are you sure you want to reset this image?",
      [
        {
          text: "Yes",
          onPress: () => {
            setSelectedImage("null");
            setLatitude("N/A");
            setLongitude("N/A");
          },
        },
        { text: "No", style: "cancel" },
      ],
      { cancelable: false }
    );
  };
  return (
    <View style={{}}>
      <Text style={styles.date}>{selectedDay}</Text>
      <View style={styles.imageContainer}>
        {selectedImage === "null" && (
          <ImageSelector onImageSelected={imageSelectedHandler} />
        )}
        {selectedImage !== "null" && (
          <>
            <TouchableOpacity onPress={resetImage}>
              <View style={styles.defaultImage}>
                <Image style={styles.image} source={{ uri: selectedImage }} />
              </View>
            </TouchableOpacity>
          </>
        )}
      </View>
      <View style={styles.contentContainer}>
        {isFetching ? (
          <ActivityIndicator size="small" color="#0000ff" />
        ) : (
          <>
            <Text style={styles.headText}>Image Location</Text>
            <Text style={styles.bodyText}>
              Latitude: {latitude} Longitude: {longitude}
            </Text>
          </>
        )}
        <Text style={styles.headText}>Contact</Text>
        <TextInput
          style={styles.bodyInput}
          onChangeText={(text) => {
            setContact(text);
          }}
          value={contact}
          placeholder="enter phone number"
        />
        <Text style={styles.headText}>MEMO</Text>
        <TextInput
          style={styles.bodyInput}
          onChangeText={(text) => {
            setMemo(text);
          }}
          value={memo}
          placeholder="enter memo"
        />
        <TouchableOpacity
          style={styles.saveButton}
          onPress={() => {
            /* props.route.params.setData(
              selectedImage,
              latitude,
              longitude,
              contact,
              memo
            ); */
            if (dataId < 0) {
              addData();
            } else {
              updateData();
            }
            props.route.params.getData(selectedDay);
            props.navigation.goBack();
          }}
        >
          <Text style={{ color: "white" }}>Save</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  form: { padding: 30, paddingTop: 30, backgroundColor: "#f5eef5" },
  date: {
    margin: 10,
    fontSize: 30,
    fontWeight: "bold",
    textAlign: "center",
  },
  imageContainer: {
    justifyContent: "center",
    alignItems: "center",
  },
  defaultImage: {
    width: 320,
    height: 240,
    borderRadius: 10,
    backgroundColor: "#ccc",
    justifyContent: "center",
    alignItems: "center",
    borderWidth: 2,
    borderColor: "#f4d0f4",
  },
  image: {
    width: 320,
    height: 240,
    borderRadius: 10,
  },
  contentContainer: {
    justifyContent: "center",
    alignItems: "center",
    margin: 10,
  },
  saveButton: {
    alignItems: "center",
    justifyContent: "center",
    width: 135,
    height: 40,
    backgroundColor: "#f4d7f5",
    borderRadius: 10,
    marginTop: 30,
  },
  headText: {
    color: "grey",
    fontSize: 18,
    fontWeight: "bold",
    marginBottom: 5,
    marginTop: 30,
  },
  bodyText: {
    fontSize: 15,
  },
  bodyInput: {
    fontSize: 15,
    height: 40,
    width: 300,
    borderColor: "gray",
    borderWidth: 1,
    padding: 10,
    borderRadius: 10,
  },
});

export default EditScreen;
