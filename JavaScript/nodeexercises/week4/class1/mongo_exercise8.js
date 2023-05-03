import * as dbRtns from "./db_routines.js";
const rawJSON = `[{"name":"Jane Doe", "age":22, "email": "jd@abc.com"},
 {"name":"John Smith", "age":24, "email": "js@abc.com"},
 {"name":"Seolan Jin", "age":22, "email": "sj@abc.com"} ]`;
const bulkLoadAndFindUsers = async () => {
  let someUsers = JSON.parse(rawJSON);
  try {
    const db = await dbRtns.getDBInstance();
    // clean out collection before adding new users
    /* let results = await dbRtns.deleteAll(db, "users");
    console.log(
      `deleted ${results.deletedCount} documents from users collection`
    );
    results = await dbRtns.addMany(db, "users", someUsers);
    console.log(
      `added ${results.insertedCount} documents to the user collection`
    ); */
    console.log(db);
    let someUser = await dbRtns.findOne(db, "users", { name: "Seolan Jin" });
    let allDbUsers = await dbRtns.findAll(db, "users", {}, {}); // empty criteria and projection
    console.log(allDbUsers);
    allDbUsers.forEach((user) =>
      console.log(`user ${user.name} is in the collection`)
    );
    process.exit(0);
  } catch (err) {
    console.log(err);
    process.exit(1);
  }
};
bulkLoadAndFindUsers();
