// Load the got module
import got from "got";
// Lets try to make a HTTP GET request to GOC's website and get some transfer info in JSON.
const dumpJson = async () => {
 const srcAddr =
 "https://www.7timer.info/bin/astro.php?lon=113.2&lat=23.1&ac=0&unit=metric&output=json&tzshift=0";
 const FISCALYEAR = "2022-2023";
 // Create a currency formatter.
 const formatter = new Intl.NumberFormat("en-US", {
 style: "currency",
 currency: "USD",
 minimumFractionDigits: 0,
 });
 try {
 const response = await got(srcAddr, { responseType: "json" });
 // strip out the Ontario amount
 let dataseries = response.body.dataseries;
 let direction = dataseries[0]["wind10m"]["direction"];
 let speed = dataseries[0]["wind10m"]["speed"];
 console.log(`Wind direction is ${direction} and Wind speed is ${speed}m/s`);

 
 } catch (error) {
 console.log(error);
 //=> 'Internal server error ...'
 }
};
dumpJson();