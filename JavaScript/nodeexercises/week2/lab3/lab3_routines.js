import got from "got";
const provinces = [
  { code: "NS", name: "Nova Scotia" },
  { code: "NL", name: "Newfoundland" },
  { code: "NB", name: "New Brunswick" },
  { code: "PE", name: "Prince Edward Island" },
  { code: "QC", name: "Quebec" },
  { code: "ON", name: "Ontario" },
  { code: "MB", name: "Manitoba" },
  { code: "SK", name: "Saskatchewan" },
  { code: "AB", name: "Alberta" },
  { code: "BC", name: "British Columbia" },
  { code: "NT", name: "North West Territories" },
  { code: "NU", name: "Nunavut" },
  { code: "YT", name: "Yukon Territory" },
];
const FISCALYEAR = "2022-2023";
// Create a currency formatter.
const currencyFormatter = (numberToFormat) =>
  new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    minimumFractionDigits: 0,
  }).format(numberToFormat);

const fullNameAndProvincePromise = (fname, lname, provinceCode) => {
  return new Promise((resolve, reject) => {
    if (fname === null || lname === null || provinceCode === null) {
      // Reject the Promise with an error
      reject("Error occured");
    } else {
      provinces.forEach((item) => {
        if (item.code === provinceCode) {
          // Resolve (or fulfill) the Promise with data
          let data = {
            firstname: `${fname}`,
            lastname: `${lname}`,
            province: `${item.name}`,
          };
          resolve(data);
        }
      });
    }
  });
};

const transferPaymentsFromWebPromise = () => {
  let srcAddr =
    "http://www.infrastructure.gc.ca/alt-format/opendata/transfer-program-programmes-de-transfert-bil.json";
  return new Promise((resolve, reject) => {
    got(srcAddr, { responseType: "json" })
      .then((response) => {
        //let body = response;
        resolve(response.body);
      })
      .catch((err) => {
        console.log(`Error ==> ${err}`);
        reject(err);
      });
  });
};

const transferPaymentForProvincePromise = (gocData, provCode) => {
  //let t = provCode.toLowerCase();

  return new Promise((resolve, reject) => {
    let result = gocData.ccbf[provCode.toLowerCase()][FISCALYEAR];
    if (result === null) {
      reject("Error occured");
    } else {
      resolve(currencyFormatter(result));
    }
  });
};

export {
  provinces,
  FISCALYEAR,
  currencyFormatter,
  fullNameAndProvincePromise,
  transferPaymentsFromWebPromise,
  transferPaymentForProvincePromise,
};
