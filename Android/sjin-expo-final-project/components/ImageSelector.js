import React from "react";
import { View, Image, StyleSheet, Alert, TouchableOpacity } from "react-native";
import * as ImagePicker from "expo-image-picker";
import * as Permissions from "expo-permissions";

// The following are required for access to the camera:
// expo install expo-image-picker
// expo install expo-permissions

const ImageSelector = (props) => {
  const verifyPermissions = async () => {
    const cameraResult = await ImagePicker.requestCameraPermissionsAsync();
    const libraryResult =
      await ImagePicker.requestMediaLibraryPermissionsAsync();

    if (
      cameraResult.status !== "granted" &&
      libraryResult.status !== "granted"
    ) {
      Alert.alert(
        "Insufficient Permissions!",
        "You need to grant camera permissions to use this app.",
        [{ text: "Okay" }]
      );
      return false;
    }
    return true;
  };

  const retrieveImageHandler = async () => {
    const hasPermission = await verifyPermissions();
    if (!hasPermission) {
      return false;
    }

    const image = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 0.5,
    });

    if (!image.cancelled) {
      props.onImageSelected(image.uri);
    }
  };

  const takeImageHandler = async () => {
    const hasPermission = await verifyPermissions();
    if (!hasPermission) {
      return false;
    }

    const image = await ImagePicker.launchCameraAsync({
      allowsEditing: true,
      aspect: [16, 9],
      quality: 0.5,
    });

    if (!image.canceled) {
      props.onImageSelected(image.uri);
      console.log(image.uri);
    }
  };

  return (
    <TouchableOpacity onPress={takeImageHandler}>
      <View style={styles.defaultImage}>
        <Image
          source={require("../defaultImg.png")}
          style={styles.image}
          onPress={takeImageHandler}
        />
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
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
});

export default ImageSelector;
