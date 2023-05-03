import yargs from "yargs";
import { hideBin } from "yargs/helpers";
import * as cfg from "./config.js";
import * as dbRtns from "./db_routines.js";

// Note: hideBin is a shorthand for process.argv.slice(2)
// - bypass the first two arguments
const argv = yargs(hideBin(process.argv))
  .options({
    code: {
      demandOption: true,
      describe: "Country Code",
      string: true,
    },
  })
  .help()
  .alias("help", "h")
  .parse();

const findCountryByCode = async () => {
  let countries = await dbRtns.getJSONFromWWWPromise(cfg.url);
  try {
    let someCountries = await dbRtns.createNameAndCodeArray(countries);

    const db = await dbRtns.getDBInstance();

    let results = await dbRtns.count(db, cfg.coll);
    console.log(
      `there are now ${results} documents currently in the ${cfg.coll} collection`
    );

    results = await dbRtns.deleteAll(db, cfg.coll);
    console.log(
      `deleted ${results.deletedCount} documents from ${cfg.coll} collection`
    );

    results = await dbRtns.addMany(db, cfg.coll, someCountries);
    console.log(
      `there are now ${results.insertedCount} documents currently in the ${cfg.coll} collection`
    );

    results = await dbRtns.findOne(db, cfg.coll, { country_code: argv.code });
    if (results == null) {
      console.log(`The code ${argv.code} is not a known country alpha-3 code`);
    } else {
      console.log(
        `The code ${argv.code} belongs to the country of ${results.country_name}`
      );
    }

    process.exit(0);
  } catch (err) {
    console.log(err);
    process.exit(1);
  }
};
findCountryByCode();
