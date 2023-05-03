import { getJSONFromWWWPromise } from "./utilities.js";
import * as cfg from "./config.js";
(async () => {
  try {
    let alertJson = await getJSONFromWWWPromise(cfg.gocAlerts);
    console.log(`Retrieved Alert JSON from remote web site.`);

    let countryJson = await getJSONFromWWWPromise(cfg.rawdata);
    console.log(`Retrieved Country JSON from remote web site.`);

    console.log(
      `There are ${Object.keys(alertJson.data).length} alerts and ${
        Object.keys(countryJson).length
      } countries`
    );
  } catch (error) {
    console.log(error);
    //=> 'Internal server error ...'
  }
})(); // IIFE
