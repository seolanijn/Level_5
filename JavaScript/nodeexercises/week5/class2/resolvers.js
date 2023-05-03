import * as dbRtns from "./db_routines.js";
import * as cfg from "./config.js";
const resolvers = {
  users: async () => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findAll(db, cfg.collection, {}, {});
  },
  userbyname: async (args) => {
    let db = await dbRtns.getDBInstance();
    return await dbRtns.findOne(db, cfg.collection, { name: args.name });
  },
  adduser: async (args) => {
    let db = await dbRtns.getDBInstance();
    let user = { name: args.name, age: args.age, email: args.email };
    console.log(user);
    let results = await dbRtns.addOne(db, cfg.collection, user);
    return results.acknowledged ? user : null;
  },
};
export { resolvers };
