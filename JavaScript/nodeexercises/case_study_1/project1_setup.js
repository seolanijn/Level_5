import * as dbRtns from "./db_routines.js";
import * as cfg from "./config.js";
let finalResults;
const addAlertsContent = async () => {
  try {
    finalResults = "";
    const db = await dbRtns.getDBInstance();

    let results = await dbRtns.deleteAll(db, cfg.alertColl);
    finalResults += `Deleted ${results.deletedCount} documents from ${cfg.alertColl} collection. `;

    let alertJSON = await dbRtns.getJSONFromWWWPromise(cfg.alerturl);
    finalResults += `Retrieved Alert JSON from remote web site. `;

    let countryJSON = await dbRtns.getJSONFromWWWPromise(cfg.isourl);
    finalResults += `Retrieved Country JSON from GitHub. `;

    let alertArray = await dbRtns.createNewArrayWithSpecificProperties(
      alertJSON,
      countryJSON
    );

    let addResults = await dbRtns.addMany(db, cfg.alertColl, alertArray);
    finalResults += `Added ${addResults.insertedCount} documents to the ${cfg.alertColl} collection. `;
  } catch (err) {
    console.log(err);
  } finally {
    console.log(finalResults);
    return { results: finalResults };
  }
};
export { addAlertsContent };
