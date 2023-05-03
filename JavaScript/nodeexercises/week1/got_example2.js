// Load the got module
import got from "got";
// Lets try to make a HTTP GET request to GOC's website and get some transfer info in JSON.
const dumpJson = async () => {
 const srcAddr =
 "http://www.infrastructure.gc.ca/alt-format/opendata/transfer-program-programmes-de-transfert-bil.json";
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
 let ab = response.body.ccbf.ab[FISCALYEAR];
 let bc = response.body.ccbf.bc[FISCALYEAR];

 let difAbBc = ab-bc;
 let difBcAb = bc-ab;

 // format to currency
 ab = formatter.format(ab);
 bc = formatter.format(bc);

 difAbBc = formatter.format(difAbBc);
 difBcAb = formatter.format(difBcAb);

 // dump to the console using template literal
 console.log(`Alberta's transfer amount for 2022-2023 was ${ab}`);
 console.log(`B.C.'s transfer amount for 2022-2023 was ${bc}`);

 if (ab > bc){
console.log(`Alberta received ${difAbBc} more than B.C. for ${FISCALYEAR}`)
 }
 else {
    console.log(`B.C. received ${difBcAb} more than Alberta for ${FISCALYEAR}`)
 }
 } catch (error) {
 console.log(error);
 //=> 'Internal server error ...'
 }
};
dumpJson();