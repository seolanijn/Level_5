import yargs from "yargs";
import { hideBin } from "yargs/helpers";
import {
  fileStatsFromFSPromise,
  getJSONFromWWWPromise,
  writeFileFromFSPromise,
  readFileFromFSPromise,
} from "./iso_country_routines.js";
import * as cfg from "./config.js";
import { promises as fsp } from "fs";

// Note: hideBin is a shorthand for process.argv.slice(2)
// - bypass the first two arguments
const argv = yargs(hideBin(process.argv))
  .options({
    refresh: {
      demandOption: false,
      describe: "is a fresh copy from the web required?",
      string: true,
    },
  })
  .help()
  .alias("help", "h")
  .parse();

// console.log("Lab 4");

const dotEnvWrite = async () => {
  try {
    let fileStats = await fileStatsFromFSPromise(cfg.countriesobjects);
    if (!fileStats || argv.refresh === "") {
      let jsonArray = await getJSONFromWWWPromise(cfg.rawdata);
      await writeFileFromFSPromise(cfg.countriesobjects, jsonArray);
      console.log(`A new ${cfg.countriesobjects} file was written`);
    } else {
      console.log(
        `An existing ${cfg.countriesobjects} file was read from the file system`
      );
    }
    let fileContent = JSON.parse(
      await readFileFromFSPromise(cfg.countriesobjects)
    );

    let stats = await fsp.stat(cfg.countriesobjects);
    console.log(`${cfg.countriesobjects} was created on ${stats.ctime}`);

    let count = fileContent.length;
    console.log(`There are ${count} codes in ${cfg.countriesobjects}`);
  } catch (err) {
    console.log(err);
    console.log(`${cfg.countriesobjects} file not written to file system`);
  }
};
dotEnvWrite();
