import { Router } from "express";
import * as cfg from "./config.js";
import * as dbRtns from "./db_routines.js";

const router = Router();

const findCountryByCode = async (code) => {
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

    results = await dbRtns.findOne(db, cfg.coll, {
      code: code,
    });
    if (results == null) {
      return `The code ${code} is not a known country alpha-2 code`;
    } else {
      return `The code ${code} belongs to the country of ${results.name}`;
    }
  } catch (err) {
    console.log(err);
  }
};

// define a default route
router.get("/", (req, res) => {
  res
    .status(200)
    .send({ msg: `this would be a response from the default route` });
});
// define a get route with a name parameter
router.get("/:name", async (req, res) => {
  let countryCode = req.params.name;
  console.log(countryCode);
  let countryName = await findCountryByCode(countryCode);
  res.status(200).send(countryName);
});
export default router;
