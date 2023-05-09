import React from "react";
import { Platform } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";

import AuthenticationScreen from "../screens/AuthenticationScreen";
import CalendarScreen from "../screens/CalendarScreen";
import EditScreen from "../screens/EditScreen";

const Stack = createStackNavigator();

function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator
        initialRouteName="AuthenticationScreen"
        screenOptions={{
          headerMode: "screen",
          headerTintColor: Platform.OS === "android" ? "#fffff8" : "blue",
          headerStyle: {
            backgroundColor: Platform.OS === "android" ? "#f4d7f5" : "",
          },
        }}
      >
        <Stack.Screen
          name="AuthenticationScreen"
          component={AuthenticationScreen}
          options={{
            title: "Seolan Diary App",
          }}
        />
        <Stack.Screen
          name="CalendarScreen"
          component={CalendarScreen}
          options={{
            title: "Seolan Diary App",
            headerLeft: null, // remove the back button
          }}
        />
        <Stack.Screen
          name="EditScreen"
          component={EditScreen}
          options={{
            title: "Edit Diary",
            headerLeft: null, // remove the back button
          }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}

export default AppNavigator;
