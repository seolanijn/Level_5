import { MongoClient } from "mongodb";
import got from "got";
import * as cfg from "./config.js";
let db;
const getDBInstance = async () => {
  if (db) {
    console.log("using established connection");
    return db;
  }
  try {
    const client = new MongoClient(cfg.atlas, {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });
    console.log("establishing new connection to Atlas");
    const conn = await client.connect();
    db = conn.db(cfg.db);
  } catch (err) {
    console.log(err);
  }
  return db;
};

const createNameAndCodeArray = async (arr) => {
  try {
    if (arr === null) {
      console.log("Error occured");
    } else {
      let result = arr.map((item) => ({
        country_code: item["alpha-2"],
        country_name: item["name"],
      }));
      return result;
    }
  } catch (err) {
    console.log(err);
  }
};

const updateOne = (db, coll, criteria, projection) =>
  db
    .collection(coll)
    .findOneAndUpdate(criteria, { $set: projection }, { rawResult: true });
const deleteOne = (db, coll, criteria) =>
  db.collection(coll).deleteOne(criteria);
const getJSONFromWWWPromise = (url) => got(url).json();
const addOne = (db, coll, doc) => db.collection(coll).insertOne(doc);
const count = (db, coll) => db.collection(coll).countDocuments();
const deleteAll = (db, coll) => db.collection(coll).deleteMany({});
const addMany = (db, coll, docs) => db.collection(coll).insertMany(docs);
const findOne = (db, coll, criteria) => db.collection(coll).findOne(criteria);
const findAll = (db, coll, criteria, projection) =>
  db.collection(coll).find(criteria).project(projection).toArray();

export {
  getDBInstance,
  getJSONFromWWWPromise,
  addOne,
  count,
  deleteAll,
  addMany,
  findOne,
  findAll,
  createNameAndCodeArray,
  updateOne,
  deleteOne,
};
