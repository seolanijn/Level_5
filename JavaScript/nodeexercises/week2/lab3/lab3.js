import yargs from "yargs";
import { hideBin } from "yargs/helpers";
import {
  provinces,
  FISCALYEAR,
  currencyFormatter,
  fullNameAndProvincePromise,
  transferPaymentsFromWebPromise,
  transferPaymentForProvincePromise,
} from "./lab3_routines.js";

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
console.log("Lab 3");

const getTransferPaymentsAsync = async (firstname, lastname, province) => {
  try {
    let result = await fullNameAndProvincePromise(
      firstname,
      lastname,
      province
    );
    console.log(
      `${result.firstname}, ${result.lastname} lives in ${result.province}. `
    );

    let data = await transferPaymentsFromWebPromise();
    let gocData = data;

    let transferResult = await transferPaymentForProvincePromise(
      gocData,
      province
    );
    console.log(`It received ${transferResult} in transfer payments.`);

    console.log(`\nTransfer Payments by Province/Territory:\n`);

    let allProvinces = await Promise.allSettled(
      provinces.map((prov) => {
        let temp = prov.code;
        let result = currencyFormatter(
          gocData.ccbf[temp.toLowerCase()][FISCALYEAR]
        );
        let sentence;
        if (temp === province) {
          sentence = `\x1b[1m${prov.name} had a transfer payment of ${result}`;
        } else {
          sentence = `\x1b[0m${prov.name} had a transfer payment of ${result}`;
        }
        return sentence;
      })
    );
    allProvinces.forEach((result) => console.log(result.value));
  } catch (err) {
    console.log(`Error ==> ${err}`);
    throw new Error(err);
  }
};

getTransferPaymentsAsync(argv.firstname, argv.lastname, argv.province);
