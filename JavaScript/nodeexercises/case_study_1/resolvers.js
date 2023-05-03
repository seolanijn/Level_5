import { addAlertsContent } from "./project1_setup.js";
import * as dbRtns from "./db_routines.js";
import * as cfg from "./config.js";
const resolvers = {
  project1_setup: async () => {
    return await addAlertsContent();
  },
  alerts: async () => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findAll(db, cfg.alertColl, {}, {});
  },
  alertsforregion: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findAll(db, cfg.alertColl, { region: args.region });
  },
  regions: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findUniqueValues(db, cfg.alertColl, "region");
  },
  subregions: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findUniqueValues(db, cfg.alertColl, "subregion");
  },
  alertsforsubregion: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findAll(db, cfg.alertColl, {
      subregion: args.subregion,
    });
  },
  addadvisory: async (args) => {
    let db = await dbRtns.getDBInstance();
    let advisory = {
      name: args.name,
      country: args.country,
      text: args.text,
      date: args.date,
    };
    let results = await dbRtns.addOne(db, cfg.advColl, advisory);
    return results.acknowledged ? advisory : null;
  },
  travelers: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findUniqueValues(db, cfg.advColl, "name");
  },
  advisoriesforname: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findAll(db, cfg.advColl, {
      name: args.name,
    });
  },
};
export { resolvers };
