import * as dbRtns from "./db_routines.js";
import * as cfg from "./config.js";
import { Router } from "express";
const user_router = Router();
// define a default route to retrieve all users
user_router.get("/", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let allDbUsers = await dbRtns.findAll(db, cfg.collection, {}, {});
    console.log(allDbUsers);
    res.status(200).send({ users: allDbUsers });
  } catch (err) {
    console.log(err.stack);
    res
      .status(500)
      .send({ msg: "get all users failed - internal server error" });
  }
});

user_router.get("/:name", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let someUser = await dbRtns.findOne(db, cfg.collection, {
      name: req.params.name,
    });
    someUser !== null
      ? res.status(200).send({ users: someUser })
      : res
          .status(404)
          .send({ msg: `user name ${req.params.name} doesn't exist` });
  } catch (err) {
    console.log(err.stack);
    res.status(500).send({ msg: `error occured` });
  }
});

user_router.post("/", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let result = await dbRtns.addOne(db, cfg.collection, req.body);
    res.status(200).send({ msg: `user data ${req.body.name} was added` });
  } catch (err) {
    console.log(err.stack);
    res.status(500).send({ msg: `user data was not added` });
  }
});

user_router.put("/", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let updateResults = await dbRtns.updateOne(
      db,
      cfg.collection,
      { name: req.body.name }, // only have addresses contain a j - criteria
      { age: req.body.age, email: req.body.email } // only return the email field - projection
    );
    let msg;
    updateResults.lastErrorObject.updatedExisting
      ? res
          .status(200)
          .send({ msg: `user data ${updateResults.value.name} was updated` })
      : res.status(404).send({ msg: `user data was not updated` });
  } catch (err) {
    console.log(err.stack);
    res.status(500).send({ msg: `error` });
  }
});

user_router.delete("/:name", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let results = await dbRtns.deleteOne(db, cfg.collection, {
      name: req.params.name,
    });
    results.deletedCount === 1
      ? res.status(200).send({ msg: `1 user was deleted` })
      : res.status(404).send({ msg: `user not deleted or doesn't exist` });
  } catch (err) {
    console.log(err.stack);
    res.status(500).send({ msg: `error occured` });
  }
});

export default user_router;
