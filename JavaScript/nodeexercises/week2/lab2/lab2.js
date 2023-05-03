import yargs from "yargs";
import { hideBin } from "yargs/helpers";
import {
  provinces,
  fullNameAndProvincePromise,
  transferPaymentsFromWebPromise,
  transferPaymentForProvincePromise,
} from "./lab2_routines.js";

const provinceList = [];
provinces.forEach((pro) => {
  provinceList.push(pro["code"]);
});

// Note: hideBin is a shorthand for process.argv.slice(2)
// - bypass the first two arguments
const argv = yargs(hideBin(process.argv))
  .options({
    firstname: {
      demandOption: true,
      alias: "fname",
      describe: "Resident's first name",
      string: true,
    },
    lastname: {
      demandOption: true,
      alias: "lname",
      describe: "Resident's last name",
      string: true,
    },
    province: {
      demandOption: true,
      alias: "prov",
      describe: "Resident's home province",
      string: true,
      choices: provinceList,
    },
  })
  .help()
  .alias("help", "h")
  .parse();
console.log("Lab 2");
fullNameAndProvincePromise(argv.firstname, argv.lastname, argv.province)
  .then((result) => {
    console.log(
      `${result.firstname}, ${result.lastname} lives in ${result.province}. `
    );
  })
  .catch((err) => {
    console.log(`Error ==> ${err}`);
    reject(err);
  });

transferPaymentsFromWebPromise()
  .then((data) => {
    let gocData = data;
    transferPaymentForProvincePromise(gocData, argv.province)
      .then((transferResult) => {
        console.log(`It received ${transferResult} in transfer payments.`);
      })
      .catch((err) => {
        console.log(`Error ==> ${err}`);
        reject(err);
      });
  })
  .catch((err) => {
    console.log(`Error ==> ${err}`);
    reject(err);
  });

// console.log(
//     `${fn}`
// );
